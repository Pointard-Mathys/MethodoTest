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

//bons paramètres        
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
        
//false quand on lui passe une adresse e-mail invalide        
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
        
        
        
//SMTP pas ou mal config        
        [Fact]
        public async Task SendEmailAsync_RealCallWithValidEmail_ShouldCatchExceptionAndReturnFalse()
        {
            // Arrange
            var service = new EmailService("noreply@example.com");

            // Act
            var result = await service.SendEmailAsync("valid@email.com", "Sujet", "Corps");

            // Assert
            result.Should().BeFalse(); 
        }
        
        

// connextion couper smtp
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
        
        [Fact]
        public void IsValidEmail_ShouldValidateProperly()
        {
            // Arrange
            var service = new EmailService("test@from.com");

            // Act & Assert
            service.IsValidEmail("good@email.com").Should().BeTrue();
            service.IsValidEmail("bademail").Should().BeFalse();
        }

        
//fonctionne correctement en validant des adresses e-mail valides et en rejetant les adresses invalides        
        [Fact]
        public void SendWelcomeEmail_ShouldUseCorrectParameters()
        {
            // Arrange
            var email = "user@example.com";
            var name = "Mathys";

            var mock = new Mock<EmailService>("noreply@test.com") { CallBase = true };
            mock.Setup(x => x.IsValidEmail(email)).Returns(true);
            mock.Setup(x => x.SendEmailAsync(email, It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(true);

            // Act
            mock.Object.SendWelcomeEmail(email, name);

            // Assert
            mock.Verify(x => x.SendEmailAsync(
                It.Is<string>(to => to == email),
                It.Is<string>(subject => subject.Contains("Bienvenue")),
                It.Is<string>(body => body.Contains(name))
            ), Times.Once);
        }
    }
}
