using AttendanceWorkerService.src.AttendanceWorkerService;
using AttendanceWorkerService.src.Business.Services;
using AttendanceWorkerService.src.Core.Interfaces;
using AttendanceWorkerService.src.Infrastructure.Data;
using AttendanceWorkerService.src.Infrastructure.Data.AttendanceWorkerService.Infrastructure.Data;
using log4net;
using log4net.Config;
using Microsoft.EntityFrameworkCore;
using Serilog.Sinks.Elasticsearch;
using Serilog;
using System.Reflection;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        // Add DbContext
        services.AddDbContext<AttendanceDbContext>(options =>
            options.UseSqlServer(hostContext.Configuration.GetConnectionString("DefaultConnection")));

        // Add logging
        var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
        XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

        Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri("http://localhost:9200")) // Replace with your Elasticsearch URL
            {
                AutoRegisterTemplate = true, // Automatically registers the log template in Elasticsearch
                IndexFormat = "application-logs-{0:yyyy.MM.dd}" // Daily rolling index
            })
            .Enrich.WithProperty("Application", "AttendanceWorkerService") // Adds an application name to each log
            .CreateLogger();

        // Register scoped services
        services.AddScoped<IAttendanceRepository, AttendanceRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IFileGenerationService, FileGenerationService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IAttendanceAnalysisService, AttendanceAnalysisService>();

        // Register Worker as a Hosted Service. We can use other services like Quartz.net for more reliable and configurable worker/hosted service.
        services.AddHostedService<Worker>();
    })
    .UseSerilog() // Use Serilog as the logging provider
    .Build();

using (var scope = host.Services.CreateScope())
{
    //ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType);
    var logger = Log.ForContext<Program>();

    var dbContext = scope.ServiceProvider.GetRequiredService<AttendanceDbContext>();

    try
    {
        if (dbContext.Database.GetPendingMigrations().Any())
        {
            logger.Warning("Applying database migrations...");
            dbContext.Database.Migrate();
            logger.Warning("Database migrations applied successfully.");
        }
        else if (!dbContext.Database.CanConnect())
        {
            logger.Warning("Database does not exist. Creating database...");
            dbContext.Database.EnsureCreated();
            logger.Warning("Database created successfully.");
        }
        else
        {
            logger.Information("Database is up-to-date.");
        }
    }
    catch (Exception ex)
    {
        logger.Error("An error occurred while applying migrations or ensuring the database is created.", ex);
        throw; // Re-throw the exception to halt startup if migrations fail
    }
}

await host.RunAsync();