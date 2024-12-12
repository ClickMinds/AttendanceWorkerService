using AttendanceWorkerService.src.Core.Entities;
using AttendanceWorkerService.src.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceWorkerService.src.Business.Services
{
    public class FileGenerationService : IFileGenerationService
    {
        public async Task GenerateAbsentReportAsync(IEnumerable<AttendanceRecord> records)
        {
            await WriteToFile("AbsentEmployees.txt", records, r =>
                $"{r.Employee.Id},{r.Employee.Name},{r.Employee.ReportingManager}");
        }

        public async Task GenerateIncompleteReportAsync(IEnumerable<AttendanceRecord> records)
        {
            await WriteToFile("IncompleteAttendanceEmployees.txt", records, r =>
                $"{r.Employee.Id},{r.Employee.Name},{r.Employee.ReportingManager},{r.CheckInTime},{r.CheckOutTime}");
        }

        public async Task GenerateLateReportAsync(IEnumerable<AttendanceRecord> records)
        {
            await WriteToFile("LateComingEmployees.txt", records, r =>
                $"{r.Employee.Id},{r.Employee.Name},{r.Employee.ReportingManager},{r.CheckInTime},{r.Employee.ShiftSchedule.StartTime}");
        }

        private async Task WriteToFile(string fileName, IEnumerable<AttendanceRecord> records, Func<AttendanceRecord, string> format)
        {
            using var writer = new StreamWriter(fileName);
            foreach (var record in records)
            {
                await writer.WriteLineAsync(format(record));
            }
        }
    }
}
