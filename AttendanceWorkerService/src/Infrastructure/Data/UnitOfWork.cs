using AttendanceWorkerService.src.Core.Interfaces;
using AttendanceWorkerService.src.Infrastructure.Data.AttendanceWorkerService.Infrastructure.Data;

namespace AttendanceWorkerService.src.Infrastructure.Data
{
    /// <summary>
    /// Implements the Unit of Work pattern to coordinate operations across repositories 
    /// and manage database transactions for the attendance system.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AttendanceDbContext _context;

        /// <summary>
        /// Gets the repository for managing attendance records.
        /// </summary>
        public IAttendanceRepository AttendanceRepository { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class with the specified context and repository.
        /// </summary>
        /// <param name="context">The database context for managing data operations.</param>
        /// <param name="attendanceRepository">The attendance repository to be used within the Unit of Work.</param>
        public UnitOfWork(AttendanceDbContext context, IAttendanceRepository attendanceRepository)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            AttendanceRepository = attendanceRepository ?? throw new ArgumentNullException(nameof(attendanceRepository));
        }

        /// <summary>
        /// Saves all pending changes to the database as a single transaction.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous save operation.</returns>
        public async Task SaveChangesAsync()
        {
            // Save all changes in the current transaction
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Disposes of the database context, releasing all managed resources.
        /// </summary>
        public void Dispose()
        {
            // Dispose the context to release resources
            _context.Dispose();
        }
    }
}
