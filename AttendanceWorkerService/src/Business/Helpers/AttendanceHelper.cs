namespace AttendanceWorkerService.src.Business.Helpers
{
    public static class AttendanceHelper
    {
        /// <summary>
        /// Calculates the total hours worked based on check-in and check-out times.
        /// </summary>
        /// <param name="checkInTime">Check-in time of the employee.</param>
        /// <param name="checkOutTime">Check-out time of the employee.</param>
        /// <returns>Total hours worked.</returns>
        public static double CalculateTotalHoursWorked(DateTime? checkInTime, DateTime? checkOutTime)
        {
            if (checkInTime == null || checkOutTime == null)
                return 0;

            return (checkOutTime.Value - checkInTime.Value).TotalHours;
        }

        /// <summary>
        /// Determines if the employee arrived late based on the shift start time.
        /// </summary>
        /// <param name="checkInTime">Check-in time of the employee.</param>
        /// <param name="shiftStartTime">Start time of the assigned shift.</param>
        /// <returns>True if the employee is late; otherwise, false.</returns>
        public static bool IsLate(DateTime? checkInTime, TimeSpan shiftStartTime)
        {
            if (checkInTime == null)
                return false;

            return checkInTime.Value.TimeOfDay > shiftStartTime;
        }
    }
}
