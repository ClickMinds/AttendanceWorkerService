using AttendanceWorkerService.src.Core.Interfaces;
using AttendanceWorkerService.src.Infrastructure.Data.AttendanceWorkerService.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceWorkerService.src.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AttendanceDbContext _context;
        public IAttendanceRepository AttendanceRepository { get; }

        public UnitOfWork(AttendanceDbContext context, IAttendanceRepository attendanceRepository)
        {
            _context = context;
            AttendanceRepository = attendanceRepository;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
