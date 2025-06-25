using System.Text.RegularExpressions;
using Notification.Contracts;

namespace Notification.Services
{
    public class SmsService : ISmsService
    {
        public Task<bool> SendSmsAsync(string phoneNumber, string message)
        {
            if (!IsValidPhoneNumber(phoneNumber)) return Task.FromResult(false);
            return Task.FromResult(true);
        }

        public bool IsValidPhoneNumber(string phoneNumber)
        {
            return Regex.IsMatch(phoneNumber, @"^\+\d{10,}$");
        }
    }
}