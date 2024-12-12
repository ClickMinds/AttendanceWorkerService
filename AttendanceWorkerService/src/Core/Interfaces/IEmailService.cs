namespace AttendanceWorkerService.src.Core.Interfaces
{
    public interface IEmailService
    {
        /// <summary>
        /// Sends attendance reports to the admin.
        /// </summary>
        Task SendReportsAsync();
    }
}
