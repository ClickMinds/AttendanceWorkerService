namespace AttendanceWorkerService.src.Core.Entities
{
    /// <summary>
    /// Represents an attendance record for an employee, 
    /// including details about check-in, check-out, total hours worked, and attendance status.
    /// </summary>
    public class AttendanceRecord
    {
        /// <summary>
        /// Gets or sets the unique identifier for the attendance record.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the employee associated with the attendance record.
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        /// Gets or sets the check-in time for the employee.
        /// Nullable to account for cases where the employee did not check in.
        /// </summary>
        public DateTime? CheckInTime { get; set; }

        /// <summary>
        /// Gets or sets the check-out time for the employee.
        /// Nullable to account for cases where the employee did not check out.
        /// </summary>
        public DateTime? CheckOutTime { get; set; }

        /// <summary>
        /// Gets or sets the total hours worked by the employee on the given day.
        /// Default value is 0 if no work was recorded.
        /// </summary>
        public decimal TotalHoursWorked { get; set; }

        /// <summary>
        /// Gets or sets the identifier for the attendance status (e.g., Present, Absent, Late).
        /// This is a foreign key to the AttendanceStatus table.
        /// </summary>
        public int AttendanceStatusId { get; set; }

        /// <summary>
        /// Gets or sets the associated employee details.
        /// Navigation property to provide access to employee information.
        /// </summary>
        public Employee Employee { get; set; } = default!; // Navigation property

        /// <summary>
        /// Gets or sets the associated attendance status details.
        /// Navigation property to provide access to attendance status information.
        /// </summary>
        public AttendanceStatus AttendanceStatus { get; set; } = default!; // Navigation property
    }
}
