namespace AttendanceWorkerService.src.Core.Interfaces
{
    /// <summary>
    /// Defines the contract for the Unit of Work pattern, 
    /// providing a centralized approach for managing repositories and saving changes.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Gets the repository for managing attendance records.
        /// </summary>
        IAttendanceRepository AttendanceRepository { get; }

        /// <summary>
        /// Saves all pending changes to the database as a single transaction.
        /// </summary>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation of saving changes.
        /// </returns>
        Task SaveChangesAsync();
    }
}
