namespace AttendanceWorkerService.src.Core.Enums
{
    /// <summary>
    /// Represents the possible attendance statuses for an employee.
    /// </summary>
    public enum AttendanceStatus
    {
        /// <summary>
        /// Indicates that the employee was present for the scheduled work shift.
        /// </summary>
        Present,

        /// <summary>
        /// Indicates that the employee was absent for the scheduled work shift.
        /// </summary>
        Absent,

        /// <summary>
        /// Indicates that the employee's attendance record is incomplete
        /// due to missing check-in or check-out information.
        /// </summary>
        Incomplete,

        /// <summary>
        /// Indicates that the employee arrived late for the scheduled work shift.
        /// </summary>
        Late
    }
}
