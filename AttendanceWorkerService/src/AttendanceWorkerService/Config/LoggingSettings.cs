namespace AttendanceWorkerService.src.AttendanceWorkerService.Config
{
    public class LoggingSettings
    {
        public string ElasticsearchUrl { get; set; } = "localhost";
        public string IndexName { get; set; } = "NameOfTheIndex";
    }
}
