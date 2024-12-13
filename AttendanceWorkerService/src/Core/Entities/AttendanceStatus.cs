namespace AttendanceWorkerService.src.Core.Entities
{
    /// <summary>
    /// Represents the attendance status of an employee, 
    /// such as "Present", "Absent", "Late", or "Incomplete".
    /// </summary>
    public class AttendanceStatus
    {
        /// <summary>
        /// Gets or sets the unique identifier for the attendance status.
        /// This serves as the primary key in the database.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the attendance status.
        /// Example values: "Present", "Absent", "Late", "Incomplete".
        /// </summary>
        public string StatusName { get; set; } = default!;
    }
}
