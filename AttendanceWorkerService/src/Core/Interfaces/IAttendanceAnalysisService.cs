using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceWorkerService.src.Core.Interfaces
{
    public interface IAttendanceAnalysisService
    {
        /// <summary>
        /// Processes the daily attendance data to identify absent, incomplete, and late employees.
        /// </summary>
        Task ProcessDailyAttendanceAsync();
    }
}
