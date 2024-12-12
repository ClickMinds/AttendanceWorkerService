using AttendanceWorkerService.src.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceWorkerService.src.Core.Interfaces
{
    public interface IFileGenerationService
    {
        /// <summary>
        /// Generates a report of absent employees.
        /// </summary>
        /// <param name="records">List of absent attendance records.</param>
        Task GenerateAbsentReportAsync(IEnumerable<AttendanceRecord> records);

        /// <summary>
        /// Generates a report of employees with incomplete attendance.
        /// </summary>
        /// <param name="records">List of incomplete attendance records.</param>
        Task GenerateIncompleteReportAsync(IEnumerable<AttendanceRecord> records);

        /// <summary>
        /// Generates a report of employees who arrived late.
        /// </summary>
        /// <param name="records">List of late attendance records.</param>
        Task GenerateLateReportAsync(IEnumerable<AttendanceRecord> records);
    }
}
