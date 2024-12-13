using AttendanceWorkerService.src.Core.Entities;

namespace AttendanceWorkerService.src.Core.Interfaces
{
    /// <summary>
    /// Defines the contract for generating attendance-related reports.
    /// </summary>
    public interface IFileGenerationService
    {
        /// <summary>
        /// Generates a report of employees who were absent and writes it to a file.
        /// </summary>
        /// <param name="records">
        /// A collection of <see cref="AttendanceRecord"/> objects representing absent employees.
        /// </param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation of generating the absent report.
        /// </returns>
        Task GenerateAbsentReportAsync(IEnumerable<AttendanceRecord> records);

        /// <summary>
        /// Generates a report of employees with incomplete attendance and writes it to a file.
        /// </summary>
        /// <param name="records">
        /// A collection of <see cref="AttendanceRecord"/> objects representing employees with incomplete attendance.
        /// </param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation of generating the incomplete attendance report.
        /// </returns>
        Task GenerateIncompleteReportAsync(IEnumerable<AttendanceRecord> records);

        /// <summary>
        /// Generates a report of employees who arrived late and writes it to a file.
        /// </summary>
        /// <param name="records">
        /// A collection of <see cref="AttendanceRecord"/> objects representing late employees.
        /// </param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation of generating the late report.
        /// </returns>
        Task GenerateLateReportAsync(IEnumerable<AttendanceRecord> records);
    }
}
