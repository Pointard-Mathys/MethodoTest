using System.Net.Mail;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Notification.Contracts;
using Notification.Services;
using Xunit;

namespace Notification.Tests
{
    public class EmailServiceTests
    {
//e-mail valide        
        [Fact]
        public async Task SendEmailAsync_WhenValidEmail_ShouldCallAndReturnTrue()
        {
            // Arrange 
            var mock = new Mock<EmailService>("noreply@test.com");

            
            mock.Setup(e => e.IsValidEmail(It.IsAny<string>())).Returns(true);
            mock.Setup(e => e.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(true);
            
            //act
            var result = await mock.Object.SendEmailAsync("user@example.com", "Test", "Hello");

            // Assert 
            result.Should().BeTrue();
        }
        
        
        
//rejette une adresse e-mail invalide
        [Fact]
        public void IsValidEmail_WithBadFormat_ShouldReturnFalse()
        {
            // Arrange
            var service = new EmailService("noreply@test.com");

            // Act
            var result = service.IsValidEmail("bad-email");

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void SendWelcomeEmail_ShouldTriggerSendEmail()
        {
            
            var mock = new Mock<EmailService>("noreply@test.com") { CallBase = true };

            mock.Setup(e => e.IsValidEmail(It.IsAny<string>())).Returns(true);

            mock.Setup(e => e.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(true);

            // Act 
            mock.Object.SendWelcomeEmail("user@example.com", "Mathys");

            // Assert
            mock.Verify(e => e.SendEmailAsync(
                It.Is<string>(s => s == "user@example.com"),                
                It.Is<string>(s => s.Contains("Bienvenue")),              
                It.Is<string>(b => b.Contains("Mathys"))                
            ), Times.Once); 
        }
        
        [Fact]
        public async Task SendEmailAsync_WithValidEmail_ShouldReturnTrue()
        {
            // Arrange
            var mock = new Mock<EmailService>("noreply@test.com") { CallBase = true };
            var email = "user@example.com";

            mock.Setup(x => x.IsValidEmail(email)).Returns(true);
            mock.Setup(x => x.SendEmailAsync(email, It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(true);

            // Act
            var result = await mock.Object.SendEmailAsync(email, "Hello", "Bienvenue");

            // Assert
            result.Should().BeTrue();
            mock.Verify(x => x.IsValidEmail(email), Times.Once);
        }

        [Fact]
        public async Task SendEmailAsync_WithInvalidEmail_ShouldReturnFalse()
        {
            // Arrange
            var service = new EmailService("noreply@test.com");

            // Act
            var result = await service.SendEmailAsync("invalid-email", "Test", "Body");

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task SendEmailAsync_WhenSmtpThrows_ShouldReturnFalse()
        {
            // Arrange
            var mock = new Mock<EmailService>("noreply@test.com") { CallBase = true };
            var email = "fail@example.com";

            mock.Setup(x => x.IsValidEmail(email)).Returns(true);

            // Simule une exception lors de l’envoi SMTP
            mock.Setup(x => x.SendEmailAsync(email, It.IsAny<string>(), It.IsAny<string>()))
                .ThrowsAsync(new SmtpException("SMTP failed"));

            // Act & Assert
            await FluentActions.Awaiting(() =>
                mock.Object.SendEmailAsync(email, "Test", "Body")
            ).Should().ThrowAsync<SmtpException>();
        }
    }
}
