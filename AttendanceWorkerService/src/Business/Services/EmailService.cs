using AttendanceWorkerService.src.Core.Interfaces;
using System.Net;
using System.Net.Mail;

namespace AttendanceWorkerService.src.Business.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendReportsAsync()
        {
            var smtpClient = new SmtpClient
            {
                Host = _configuration["EmailSettings:Host"],
                Port = int.Parse(_configuration["EmailSettings:Port"]),
                Credentials = new NetworkCredential(
                    _configuration["EmailSettings:Username"],
                    _configuration["EmailSettings:Password"])
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_configuration["EmailSettings:From"]),
                Subject = "Daily Attendance Reports",
                Body = "Attached are the daily attendance reports."
            };

            mailMessage.Attachments.Add(new Attachment("AbsentEmployees.txt"));
            mailMessage.Attachments.Add(new Attachment("IncompleteAttendanceEmployees.txt"));
            mailMessage.Attachments.Add(new Attachment("LateComingEmployees.txt"));

            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}
