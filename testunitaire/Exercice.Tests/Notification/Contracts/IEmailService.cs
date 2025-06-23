namespace Notification.Contracts;

public interface IEmailService
{
    /// <summary>
    /// Sends an email asynchronously to the specified recipient.
    /// </summary>
    /// <param name="to">The email address of the recipient.</param>
    /// <param name="subject">The subject of the email.</param>
    /// <param name="body">The body content of the email.</param>
    /// <returns>Returns a task representing the asynchronous operation, with a result indicating whether the email was sent successfully.</returns>
    Task<bool> SendEmailAsync(string to, string subject, string body);

    /// <summary>
    /// Validates whether the provided email address is in a correct format.
    /// </summary>
    /// <param name="email">The email address to validate.</param>
    /// <returns>Returns true if the email is valid, otherwise false.</returns>
    bool IsValidEmail(string email);

    /// <summary>
    /// Sends a welcome email to the specified recipient.
    /// </summary>
    /// <param name="email">The email address of the recipient.</param>
    /// <param name="name">The name of the recipient.</param>
    void SendWelcomeEmail(string email, string name);
}