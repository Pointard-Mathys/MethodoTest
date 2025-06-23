namespace Notification.Contracts;

public interface INotificationService
{
    /// <summary>
    /// Sends a welcome email to the specified email address with a personalized message.
    /// </summary>
    /// <param name="email">The email address of the recipient.</param>
    /// <param name="userName">The name of the user to include in the welcome message.</param>
    /// <returns>A task representing the asynchronous operation. The task result is a boolean indicating whether the email was sent successfully.</returns>
    Task<bool> SendWelcomeEmailAsync(string email, string userName);

    /// <summary>
    /// Sends a notification to the specified recipient, which can include an email or SMS, with the provided message content.
    /// </summary>
    /// <param name="email">The email address of the recipient.</param>
    /// <param name="phoneNumber">The phone number of the recipient for sending SMS notifications.</param>
    /// <param name="message">The content of the notification message to be sent.</param>
    /// <returns>A task representing the asynchronous operation. The task result is a boolean indicating whether the notification was sent successfully.</returns>
    Task<bool> SendNotificationAsync(string email, string phoneNumber, string message);
}