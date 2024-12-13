using AttendanceWorkerService.src.Core.Interfaces;

namespace AttendanceWorkerService.src.AttendanceWorkerService
{
    public class Worker : BackgroundService
    {
        // Logger for tracking information, warnings, and errors during worker execution
        private readonly ILogger<Worker> _logger;

        // Provides access to the DI container for resolving scoped services in the worker
        private readonly IServiceProvider _serviceProvider;

        // Holds the configuration values for the application, typically loaded from appsettings.json or other configuration providers
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="Worker"/> class with the provided configuration.
        /// </summary>
        /// <param name="logger">The application's logger interface used to log required information/error/warnings.</param>
        /// <param name="serviceProvider">The Service Provider of the application which can be used to fetch/execute existing application services.</param>
        /// <exception cref="ArgumentNullException">Thrown when any of the provided dependencies (<paramref name="logger"/>, or <paramref name="serviceProvider"/>) is null.</exception>
        public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider, IConfiguration configuration)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        /// <summary>
        /// Executes the background worker task that processes daily attendance records.
        /// Implements a retry mechanism for handling transient failures, retrying up to three times
        /// before logging the error and aborting the operation.
        /// </summary>
        /// <param name="stoppingToken">
        /// A <see cref="CancellationToken"/> that signals the worker task to stop processing.
        /// </param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation of the worker task.
        /// </returns>
        /// <exception cref="Exception">
        /// Thrown if the maximum retry attempts are exceeded or an unhandled exception occurs.
        /// </exception>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            const int MaxRetryAttempts = 3; // Maximum retry attempts
            var retryDelay = TimeSpan.FromSeconds(10); // Delay between retries

            try
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

                        int attempt = 0; // Retry attempt counter
                        bool success = false; // Indicates if the operation succeeded

                        while (attempt < MaxRetryAttempts && !success && !stoppingToken.IsCancellationRequested)
                        {
                            try
                            {
                                attempt++;
                                if (_logger.IsEnabled(LogLevel.Information))
                                {
                                    _logger.LogInformation("Attempt {attempt} to process daily attendance.", attempt);
                                }

                                await attendanceAnalysisService.ProcessDailyAttendanceAsync();
                                success = true; // Operation succeeded
                            }
                            catch (Exception retryEx)
                            {
                                _logger.LogError(retryEx, "Attempt {attempt} failed. Retrying in {retryDelay} seconds...", attempt, retryDelay.TotalSeconds);

                                if (attempt >= MaxRetryAttempts)
                                {
                                    _logger.LogError(retryEx, "Maximum retry attempts reached. Aborting operation.");
                                    throw; // Re-throw exception if retries are exhausted
                                }

                                await Task.Delay(retryDelay, stoppingToken); // Wait before retrying
                            }
                        }

                        await Task.Delay(TimeSpan.FromMinutes(Convert.ToInt32(_configuration["Attributes:WorkerDelayInMinutes"])), stoppingToken);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception occurred in the worker.");
                throw; // Ensure the exception is propagated for visibility
            }
        }
    }
}
