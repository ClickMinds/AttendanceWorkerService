using AttendanceWorkerService.src.Core.Interfaces;
using System.Net;
using System.Net.Mail;

namespace AttendanceWorkerService.src.Business.Services
{
    public class EmailService : IEmailService
    {
        /// <summary>Holds the configuration values for the application, typically loaded from appsettings.json or other configuration providers.</summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailService"/> class with the provided configuration.
        /// </summary>
        /// <param name="configuration">The application's configuration interface used to retrieve email-related settings (e.g., SMTP host, port, credentials).</param>
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        /// <summary>
        /// Sends an email with the specified subject line and email body, including file attachments 
        /// for absent, incomplete attendance, and late employees.
        /// </summary>
        /// <param name="subjectLine">The Subject Line of the email to be sent.</param>
        /// <param name="emailBody">The body content of the email to be sent.</param>
        /// <returns>
        /// A <see cref="Task"/> that represents the asynchronous operation of sending the email.
        /// The task completes when the email has been sent successfully or an exception is thrown if an error occurs.
        /// </returns>
        /// <exception cref="InvalidOperationException">Thrown when required email configuration values (Host, Port, From, or To) are missing or null.</exception>
        /// <exception cref="FormatException">Thrown when the port configuration value is not a valid integer.</exception>
        /// <exception cref="SmtpException">Thrown if the SMTP client encounters an error during the email-sending process.</exception>
        /// <exception cref="IOException">Thrown if there are issues accessing the file attachments.</exception>
        /// <exception cref="Exception">A general exception that wraps other unexpected issues.</exception>
        public async Task SendReportsAsync(string subjectLine, string emailBody)
        {
            try
            {
                // Configure the SMTP client with the necessary settings from the configuration file
                var smtpClient = new SmtpClient
                {
                    Host = _configuration["EmailSettings:Host"] ?? throw new InvalidOperationException("EmailSettings:Host is not configured."), // SMTP host for sending the email
                    Port = int.Parse(_configuration["EmailSettings:Port"] ?? throw new InvalidOperationException("EmailSettings:Port is not configured.")), // Port used for SMTP communication
                    EnableSsl = Convert.ToBoolean(_configuration["EmailSettings:EnableSSL"]), // Enable SSL for secure communication
                    Credentials = new NetworkCredential(_configuration["EmailSettings:Username"], _configuration["EmailSettings:Password"]) // Set SMTP credentials for authentication
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_configuration["EmailSettings:From"] ?? throw new InvalidOperationException("EmailSettings:From is not configured.")), // Set the sender's email address
                    Subject = subjectLine, // Set the subject line for the email
                    Body = emailBody // Set the body of the email
                };

                // Add the recipient's email address
                mailMessage.To.Add(_configuration["EmailSettings:To"] ?? throw new InvalidOperationException("EmailSettings:To is not configured."));

                try
                {
                    // Attach required files to the email
                    foreach (var filePath in new[] { "AbsentEmployees.txt", "IncompleteAttendanceEmployees.txt", "LateComingEmployees.txt" })
                    {
                        // Open the file stream in read mode
                        var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);

                        // Reset the stream position to the beginning to ensure the entire file is read
                        fileStream.Position = 0;

                        // Create an attachment using the file stream and add it to the email
                        var attachment = new Attachment(fileStream, Path.GetFileName(filePath));
                        mailMessage.Attachments.Add(attachment);
                    }

                    // Send the email asynchronously
                    await smtpClient.SendMailAsync(mailMessage);
                }
                catch (Exception ex)
                {
                    throw; // Rethrow the exception for upstream handling
                }
                finally
                {
                    // Ensure all file streams are released by disposing of the attachments
                    foreach (var attachment in mailMessage.Attachments)
                    {
                        attachment.Dispose(); // Ensure the file stream is released
                    }
                }
            }
            catch (Exception ex)
            {
                throw; // Handle and rethrow unexpected exceptions encountered during SMTP or email creation
            }
        }
    }
}
