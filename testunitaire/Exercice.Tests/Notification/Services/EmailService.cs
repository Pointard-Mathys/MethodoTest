using System.Net.Mail;
using System.Threading.Tasks;
using Notification.Contracts;

namespace Notification.Services
{
    public class EmailService : IEmailService
    {
        private readonly string _from;

        public EmailService(string from)
        {
            _from = from;
        }

        // Méthode protégée virtuelle pour faciliter le mock en test
        public Task SendMailAsync(MailMessage mail)
        {
            using var smtp = new SmtpClient();
            return smtp.SendMailAsync(mail);
        }

        public virtual async Task<bool> SendEmailAsync(string to, string subject, string body)
        {
            if (!IsValidEmail(to)) return false;

            try
            {
                var mail = new MailMessage(_from, to, subject, body)
                {
                    IsBodyHtml = true
                };
                await SendMailAsync(mail);  // Appel de la méthode mockable
                return true;
            }
            catch
            {
                return false;
            }
        }

        public virtual bool IsValidEmail(string email)
        {
            try
            {
                _ = new MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public virtual void SendWelcomeEmail(string email, string name)
        {
            if (!IsValidEmail(email)) return;

            var subject = "Bienvenue !";
            var body = $"Bonjour {name}, merci pour votre inscription !";
            _ = SendEmailAsync(email, subject, body);
        }
    }
}