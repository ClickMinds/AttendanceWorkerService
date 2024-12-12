using AttendanceWorkerService.src.Core.Interfaces;

namespace AttendanceWorkerService.src.AttendanceWorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IAttendanceAnalysisService _attendanceAnalysisService;
        private readonly IServiceProvider _serviceProvider;

        public Worker(ILogger<Worker> logger, IAttendanceAnalysisService attendanceAnalysisService, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _attendanceAnalysisService = attendanceAnalysisService;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }

                using (var scope = _serviceProvider.CreateScope())
                {
                    var attendanceAnalysisService = scope.ServiceProvider.GetRequiredService<IAttendanceAnalysisService>();
                    await attendanceAnalysisService.ProcessDailyAttendanceAsync();
                }

                await _attendanceAnalysisService.ProcessDailyAttendanceAsync();
                await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
            }
        }
    }
}
