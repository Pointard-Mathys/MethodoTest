using Notification.Contracts;

namespace Notification.Services;

public class EmailService : IEmailService
{
    public Task<bool> SendEmailAsync(string to, string subject, string body)
    {
        throw new NotImplementedException();
    }

    public bool IsValidEmail(string email)
    {
        throw new NotImplementedException();
    }

    public void SendWelcomeEmail(string email, string name)
    {
        throw new NotImplementedException();
    }
}