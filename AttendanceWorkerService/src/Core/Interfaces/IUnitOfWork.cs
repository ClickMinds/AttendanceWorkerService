using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceWorkerService.src.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IAttendanceRepository AttendanceRepository { get; }
        Task SaveChangesAsync();
    }
}
