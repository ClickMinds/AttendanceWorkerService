using AttendanceWorkerService.src.Core.Enums;
using AttendanceWorkerService.src.Core.Interfaces;

namespace AttendanceWorkerService.src.Business.Services
{
    public class AttendanceAnalysisService : IAttendanceAnalysisService
    {
        /// <summary>
        /// Provides access to the repository layer to perform database operations such as retrieving, updating, or analyzing attendance-related records.
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Service for generating flat file reports based on attendance analysis, including absent employees, incomplete attendance, and late arrivals.
        /// </summary>
        private readonly IFileGenerationService _fileGenerationService;

        /// <summary>
        /// Service for sending email notifications, including reports as file attachments, to administrators or stakeholders.
        /// </summary>
        private readonly IEmailService _emailService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AttendanceAnalysisService"/> class with the provided configuration.
        /// </summary>
        /// <param name="unitOfWork">Provides access to the repository layer to perform database operations related to attendance records, employees, and related entities.</param>
        /// <param name="fileGenerationService">Service for generating flat file reports (e.g., absent employees, incomplete attendance).</param>
        /// <param name="emailService">Service for sending email notifications with the generated reports attached.</param>
        /// <exception cref="ArgumentNullException">Thrown when any of the provided dependencies (<paramref name="unitOfWork"/>, <paramref name="fileGenerationService"/>, or <paramref name="emailService"/>) is null.</exception>
        public AttendanceAnalysisService(
            IUnitOfWork unitOfWork,
            IFileGenerationService fileGenerationService,
            IEmailService emailService)
        {
            // Ensure dependencies are not null
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _fileGenerationService = fileGenerationService ?? throw new ArgumentNullException(nameof(fileGenerationService));
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
        }

        public async Task ProcessDailyAttendanceAsync()
        {
            try
            {
                var records = await _unitOfWork.AttendanceRepository.GetAllAsync();

                var absent = records.Where(r => r.CheckInTime == null).ToList();
                var incomplete = records.Where(r => r.TotalHoursWorked < 9).ToList();
                var late = records.Where(r =>
                        r.CheckInTime.HasValue &&
                        r.CheckInTime.Value.TimeOfDay > r.Employee.ShiftSchedule.StartTime)
                    .ToList();

                await _unitOfWork.AttendanceRepository.UpdateStatusesAsync(absent, AttendanceStatus.Absent);
                await _unitOfWork.AttendanceRepository.UpdateStatusesAsync(incomplete, AttendanceStatus.Incomplete);
                await _unitOfWork.AttendanceRepository.UpdateStatusesAsync(late, AttendanceStatus.Late);

                await _fileGenerationService.GenerateAbsentReportAsync(absent);
                await _fileGenerationService.GenerateIncompleteReportAsync(incomplete);
                await _fileGenerationService.GenerateLateReportAsync(late);

                await _emailService.SendReportsAsync("Daily Attendance Reports", "Attached are the daily attendance reports.");
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
