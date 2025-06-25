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
    }
}
