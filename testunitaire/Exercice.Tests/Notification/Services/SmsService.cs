using Notification.Contracts;

namespace Notification.Services;

public class SmsService : ISmsService
{
    public Task<bool> SendSmsAsync(string phoneNumber, string message)
    {
        throw new NotImplementedException();
    }

    public bool IsValidPhoneNumber(string phoneNumber)
    {
        throw new NotImplementedException();
    }
}