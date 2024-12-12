using AttendanceWorkerService.src.AttendanceWorkerService.Config;
using AttendanceWorkerService.src.Business.Services;
using AttendanceWorkerService.src.Core.Interfaces;
using AttendanceWorkerService.src.Infrastructure.Data;
using AttendanceWorkerService.src.Infrastructure.Data.AttendanceWorkerService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceWorkerService.src.AttendanceWorkerService.DI
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ConfigureDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            // Database Context
            services.AddDbContext<AttendanceDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // Repositories and Unit of Work
            services.AddScoped<IAttendanceRepository, AttendanceRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Services
            services.AddScoped<IAttendanceAnalysisService, AttendanceAnalysisService>();
            services.AddScoped<IFileGenerationService, FileGenerationService>();
            services.AddScoped<IEmailService, EmailService>();

            // Configuration Bindings
            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
            services.Configure<LoggingSettings>(configuration.GetSection("LoggingSettings"));

            return services;
        }
    }
}
