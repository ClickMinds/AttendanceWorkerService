namespace AttendanceWorkerService.src.Core.Interfaces
{
    /// <summary>
    /// Defines the contract for services that analyze employee attendance data
    /// and generate actionable insights such as identifying absent, incomplete, or late employees.
    /// </summary>
    public interface IAttendanceAnalysisService
    {
        /// <summary>
        /// Processes the daily attendance data by analyzing employee attendance records.
        /// Identifies employees who are absent, have incomplete attendance, or are late,
        /// and triggers related actions such as report generation and notifications.
        /// </summary>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation of processing daily attendance.
        /// </returns>
        Task ProcessDailyAttendanceAsync();
    }
}
