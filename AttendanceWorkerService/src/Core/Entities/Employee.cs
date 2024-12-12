using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceWorkerService.src.Core.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ReportingManager { get; set; }
        public int ShiftScheduleId { get; set; }
        public ShiftSchedule ShiftSchedule { get; set; }
    }
}
