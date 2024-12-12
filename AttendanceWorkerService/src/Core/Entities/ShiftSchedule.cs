namespace AttendanceWorkerService.src.Core.Entities
{
    public class ShiftSchedule
    {
        public int Id { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
