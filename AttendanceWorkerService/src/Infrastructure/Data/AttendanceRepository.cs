using AttendanceWorkerService.src.Core.Entities;
using AttendanceWorkerService.src.Core.Enums;
using AttendanceWorkerService.src.Core.Interfaces;
using AttendanceWorkerService.src.Infrastructure.Data.AttendanceWorkerService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AttendanceWorkerService.src.Infrastructure.Data
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly AttendanceDbContext _context;

        public AttendanceRepository(AttendanceDbContext context)
        {
            _context = context;
        }

        public async Task<List<AttendanceRecord>> GetAllAsync()
        {
            return await _context.AttendanceRecords
                .Include(a => a.Employee)
                .Include(a => a.Employee.ShiftSchedule)
                .ToListAsync();
        }

        public async Task UpdateStatusesAsync(IEnumerable<AttendanceRecord> records, AttendanceStatus status)
        {
            foreach (var record in records)
            {
                record.Status = status;
                _context.AttendanceRecords.Update(record);
            }
            await _context.SaveChangesAsync();
        }
    }
}
