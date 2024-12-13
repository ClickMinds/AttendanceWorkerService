namespace AttendanceWorkerService.src.Core.Entities
{
    /// <summary>
    /// Represents a shift schedule for employees, 
    /// defining the start and end times for a specific work shift.
    /// </summary>
    public class ShiftSchedule
    {
        /// <summary>
        /// Gets or sets the unique identifier for the shift schedule.
        /// This serves as the primary key in the database.
        /// </summary>
        public int Id { get; set; } = default!;

        /// <summary>
        /// Gets or sets the start time of the shift.
        /// </summary>
        public TimeSpan StartTime { get; set; } = default!;

        /// <summary>
        /// Gets or sets the end time of the shift.
        /// </summary>
        public TimeSpan EndTime { get; set; } = default!;
    }
}
