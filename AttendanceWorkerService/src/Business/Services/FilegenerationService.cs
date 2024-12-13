using AttendanceWorkerService.src.Core.Entities;
using AttendanceWorkerService.src.Core.Interfaces;

namespace AttendanceWorkerService.src.Business.Services
{
    /// <summary>
    /// Provides functionality to generate attendance reports as flat text files,
    /// including reports for absent employees, incomplete attendance, and late arrivals.
    /// </summary>
    public class FileGenerationService : IFileGenerationService
    {
        /// <summary>
        /// Generates a report for employees who are absent and writes it to "AbsentEmployees.txt".
        /// </summary>
        /// <param name="records">A collection of attendance records representing absent employees.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task GenerateAbsentReportAsync(IEnumerable<AttendanceRecord> records)
        {
            await WriteToFile("AbsentEmployees.txt", records, r =>
                $"Employee ID: {r.Employee.Id}, Name: {r.Employee.Name}, Reporting Manager: {r.Employee.ReportingManager}");
        }

        /// <summary>
        /// Generates a report for employees with incomplete attendance and writes it to "IncompleteAttendanceEmployees.txt".
        /// </summary>
        /// <param name="records">A collection of attendance records representing employees with incomplete attendance.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task GenerateIncompleteReportAsync(IEnumerable<AttendanceRecord> records)
        {
            await WriteToFile("IncompleteAttendanceEmployees.txt", records, r =>
                $"Employee ID: {r.Employee.Id}, Name: {r.Employee.Name}, Reporting Manager: {r.Employee.ReportingManager}, Check-In: {r.CheckInTime}, Check-Out: {r.CheckOutTime}");
        }

        /// <summary>
        /// Generates a report for employees who arrived late and writes it to "LateComingEmployees.txt".
        /// </summary>
        /// <param name="records">A collection of attendance records representing late employees.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task GenerateLateReportAsync(IEnumerable<AttendanceRecord> records)
        {
            await WriteToFile("LateComingEmployees.txt", records, r =>
                $"Employee ID: {r.Employee.Id}, Name: {r.Employee.Name}, Reporting Manager: {r.Employee.ReportingManager}, Check-In: {r.CheckInTime}, Scheduled Start: {r.Employee.ShiftSchedule.StartTime}");
        }

        /// <summary>
        /// Writes the provided attendance records to a file using the specified formatting function.
        /// </summary>
        /// <param name="fileName">The name of the file to write to.</param>
        /// <param name="records">A collection of attendance records to write to the file.</param>
        /// <param name="format">A function to format each attendance record as a string.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        private async Task WriteToFile(string fileName, IEnumerable<AttendanceRecord> records, Func<AttendanceRecord, string> format)
        {
            // Ensure the StreamWriter is disposed properly after writing to the file
            using var writer = new StreamWriter(fileName);

            // Write each formatted record to the file
            foreach (var record in records)
            {
                await writer.WriteLineAsync(format(record));
            }
        }
    }
}
