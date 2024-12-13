using AttendanceWorkerService.src.Core.Entities;
using AttendanceWorkerService.src.Core.Interfaces;
using AttendanceWorkerService.src.Infrastructure.Data.AttendanceWorkerService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AttendanceWorkerService.src.Infrastructure.Data
{
    /// <summary>
    /// Provides methods for managing attendance records in the database.
    /// Implements <see cref="IAttendanceRepository"/>.
    /// </summary>
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly AttendanceDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="AttendanceRepository"/> class.
        /// </summary>
        /// <param name="context">
        /// The database context for accessing attendance-related data.
        /// </param>
        public AttendanceRepository(AttendanceDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all attendance records from the database, 
        /// including related employee and shift schedule details.
        /// </summary>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation, 
        /// with a list of <see cref="AttendanceRecord"/> as the result.
        /// </returns>
        public async Task<List<AttendanceRecord>> GetAllAsync()
        {
            // Fetch attendance records with related Employee and ShiftSchedule entities
            return await _context.AttendanceRecords
                .Include(a => a.Employee) // Include employee details
                .Include(a => a.Employee.ShiftSchedule) // Include shift schedule details
                .ToListAsync();
        }

        /// <summary>
        /// Updates the attendance status for a collection of attendance records 
        /// and saves the changes to the database.
        /// </summary>
        /// <param name="records">
        /// The collection of <see cref="AttendanceRecord"/> objects to update.
        /// </param>
        /// <param name="status">
        /// The new attendance status to apply to the records, specified as <see cref="Core.Enums.AttendanceStatus"/>.
        /// </param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation of updating the statuses.
        /// </returns>
        public async Task UpdateStatusesAsync(IEnumerable<AttendanceRecord> records, Core.Enums.AttendanceStatus status)
        {
            // Update each record's attendance status
            foreach (var record in records)
            {
                record.AttendanceStatusId = (int)status; // Map enum to corresponding database value
                _context.AttendanceRecords.Update(record); // Mark record as modified
            }

            // Save changes to the database
            await _context.SaveChangesAsync();
        }
    }
}
