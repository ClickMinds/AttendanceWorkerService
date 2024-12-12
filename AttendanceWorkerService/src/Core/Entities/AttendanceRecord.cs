using AttendanceWorkerService.src.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceWorkerService.src.Core.Entities
{
    public class AttendanceRecord
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public TimeSpan? CheckInTime { get; set; }
        public TimeSpan? CheckOutTime { get; set; }
        public double TotalHoursWorked { get; set; }
        public AttendanceStatus Status { get; set; }
        public Employee Employee { get; set; }
    }
}
