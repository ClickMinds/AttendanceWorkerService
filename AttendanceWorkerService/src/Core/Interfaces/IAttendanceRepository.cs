using AttendanceWorkerService.src.Core.Entities;
using AttendanceWorkerService.src.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceWorkerService.src.Core.Interfaces
{
    public interface IAttendanceRepository
    {
        Task<List<AttendanceRecord>> GetAllAsync();
        Task UpdateStatusesAsync(IEnumerable<AttendanceRecord> records, AttendanceStatus status);
    }
}
