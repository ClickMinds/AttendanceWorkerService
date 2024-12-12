using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceWorkerService.src.AttendanceWorkerService.Config
{
    public class LoggingSettings
    {
        public string ElasticsearchUrl { get; set; }
        public string IndexName { get; set; }
    }
}
