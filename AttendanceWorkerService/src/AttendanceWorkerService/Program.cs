using AttendanceWorkerService.src.AttendanceWorkerService;
using AttendanceWorkerService.src.AttendanceWorkerService.DI;
using AttendanceWorkerService.src.Business.Services;
using AttendanceWorkerService.src.Core.Interfaces;
using AttendanceWorkerService.src.Infrastructure.Data.AttendanceWorkerService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

//var host = builder.Build();
builder.Services.AddDbContext<AttendanceDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IAttendanceAnalysisService, AttendanceAnalysisService>();
builder.Services.AddScoped<IFileGenerationService, FileGenerationService>();    
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddHostedService<Worker>();

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHostedService<Worker>();
        services.ConfigureDependencyInjection(hostContext.Configuration);
    })
    .Build();

await host.RunAsync();
