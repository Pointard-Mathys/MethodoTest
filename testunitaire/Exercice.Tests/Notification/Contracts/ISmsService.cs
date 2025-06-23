namespace Notification.Contracts;

public interface ISmsService
{
    /// <summary>
    /// Sends an SMS message to the specified phone number asynchronously.
    /// </summary>
    /// <param name="phoneNumber">The phone number to send the SMS message to.</param>
    /// <param name="message">The content of the SMS message.</param>
    /// <returns>A task representing the asynchronous operation, containing a boolean value indicating whether the SMS was sent successfully.</returns>
    Task<bool> SendSmsAsync(string phoneNumber, string message);

    /// <summary>
    /// Validates if the provided phone number is in a correct and acceptable format.
    /// </summary>
    /// <param name="phoneNumber">The phone number to be validated.</param>
    /// <returns>A boolean value indicating whether the phone number is valid.</returns>
    bool IsValidPhoneNumber(string phoneNumber);
}