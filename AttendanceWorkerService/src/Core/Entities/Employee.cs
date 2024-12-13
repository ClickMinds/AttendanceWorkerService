namespace AttendanceWorkerService.src.Core.Entities
{
    /// <summary>
    /// Represents an employee in the organization, 
    /// including details such as their name, reporting manager, and shift schedule.
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// Gets or sets the unique identifier for the employee.
        /// This serves as the primary key in the database.
        /// </summary>
        public int Id { get; set; } = default!;

        /// <summary>
        /// Gets or sets the name of the employee.
        /// </summary>
        public string Name { get; set; } = default!;

        /// <summary>
        /// Gets or sets the name of the employee's reporting manager.
        /// </summary>
        public string ReportingManager { get; set; } = default!;

        /// <summary>
        /// Gets or sets the identifier of the shift schedule assigned to the employee.
        /// This is a foreign key referencing the <see cref="ShiftSchedule"/> table.
        /// </summary>
        public int ShiftScheduleId { get; set; }

        /// <summary>
        /// Gets or sets the shift schedule details associated with the employee.
        /// Navigation property to provide access to shift schedule information.
        /// </summary>
        public ShiftSchedule ShiftSchedule { get; set; } = default!;
    }
}
