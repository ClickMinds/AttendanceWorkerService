namespace AttendanceWorkerService.src.AttendanceWorkerService.Config
{
    public class EmailSettings
    {
        public string Host { get; set; } = "localhost";
        public int Port { get; set; } = int.MaxValue;
        public string Username { get; set; } = "User";
        public string Password { get; set; } = "Password";
        public string From { get; set; } = "Fromuser";
    }
}
