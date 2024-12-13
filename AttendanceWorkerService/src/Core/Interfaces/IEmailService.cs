namespace AttendanceWorkerService.src.Core.Interfaces
{
    /// <summary>
    /// Defines the contract for an email service to send notifications and reports.
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// Sends attendance reports to the admin with the specified subject and email body.
        /// </summary>
        /// <param name="subjectLine">
        /// The subject line of the email to be sent.
        /// </param>
        /// <param name="emailBody">
        /// The body content of the email, providing details or context for the reports.
        /// </param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation of sending the email.
        /// </returns>
        Task SendReportsAsync(string subjectLine, string emailBody);
    }
}
