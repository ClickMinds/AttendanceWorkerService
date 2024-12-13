using AttendanceWorkerService.src.Core.Entities;

namespace AttendanceWorkerService.src.Core.Interfaces
{
    /// <summary>
    /// Defines the repository interface for managing attendance records, 
    /// including retrieval and status updates.
    /// </summary>
    public interface IAttendanceRepository
    {
        /// <summary>
        /// Retrieves all attendance records from the database.
        /// </summary>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. 
        /// The task result contains a list of <see cref="AttendanceRecord"/>.
        /// </returns>
        Task<List<AttendanceRecord>> GetAllAsync();

        /// <summary>
        /// Updates the attendance status for a collection of attendance records.
        /// </summary>
        /// <param name="records">
        /// A collection of <see cref="AttendanceRecord"/> objects to update.
        /// </param>
        /// <param name="status">
        /// The new <see cref="Enums.AttendanceStatus"/> to apply to the specified records.
        /// </param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation.
        /// </returns>
        Task UpdateStatusesAsync(IEnumerable<AttendanceRecord> records, Enums.AttendanceStatus status);
    }
}
