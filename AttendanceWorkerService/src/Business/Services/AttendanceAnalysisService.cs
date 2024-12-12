using AttendanceWorkerService.src.Core.Enums;
using AttendanceWorkerService.src.Core.Interfaces;

namespace AttendanceWorkerService.src.Business.Services
{
    public class AttendanceAnalysisService : IAttendanceAnalysisService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileGenerationService _fileGenerationService;
        private readonly IEmailService _emailService;

        public AttendanceAnalysisService(
            IUnitOfWork unitOfWork,
            IFileGenerationService fileGenerationService,
            IEmailService emailService)
        {
            _unitOfWork = unitOfWork;
            _fileGenerationService = fileGenerationService;
            _emailService = emailService;
        }

        public async Task ProcessDailyAttendanceAsync()
        {
            var records = await _unitOfWork.AttendanceRepository.GetAllAsync();

            var absent = records.Where(r => r.CheckInTime == null).ToList();
            var incomplete = records.Where(r => r.TotalHoursWorked < 9).ToList();
            var late = records.Where(r => r.CheckInTime > r.Employee.ShiftSchedule.StartTime).ToList();

            await _unitOfWork.AttendanceRepository.UpdateStatusesAsync(absent, AttendanceStatus.Absent);
            await _unitOfWork.AttendanceRepository.UpdateStatusesAsync(incomplete, AttendanceStatus.Incomplete);
            await _unitOfWork.AttendanceRepository.UpdateStatusesAsync(late, AttendanceStatus.Late);

            await _fileGenerationService.GenerateAbsentReportAsync(absent);
            await _fileGenerationService.GenerateIncompleteReportAsync(incomplete);
            await _fileGenerationService.GenerateLateReportAsync(late);

            await _emailService.SendReportsAsync();
        }
    }
}
