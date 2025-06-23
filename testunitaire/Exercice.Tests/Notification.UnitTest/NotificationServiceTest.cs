using FluentAssertions;
using Moq;
using Notification.Contracts;
using Notification.Services;

namespace Notification.UnitTest;

public class NotificationServiceTest
{
    private readonly Mock<IEmailService> _emailServiceMock;
    private readonly Mock<ISmsService> _smsServiceMock;
    private readonly Mock<ILogger> _loggerMock;
    private readonly NotificationService _notificationService;
    
    public NotificationServiceTest()
    {
        _emailServiceMock = new Mock<IEmailService>();
        _smsServiceMock = new Mock<ISmsService>();
        _loggerMock = new Mock<ILogger>();
        _notificationService = new NotificationService(
            _emailServiceMock.Object, 
            _smsServiceMock.Object, 
            _loggerMock.Object
            );
    }
    
    [Fact]
    public async Task SendWelcomeEmailAsync_ValidEmail_SendsEmailAndLogsSuccess()
    {
        // Arrange
        var email = "test@example.com";
        var userName = "John Doe";
        
        _emailServiceMock.Setup(x => x.IsValidEmail(email)).Returns(true);
        _emailServiceMock.Setup(x => x.SendEmailAsync(email, "Welcome!", It.IsAny<string>()))
                        .ReturnsAsync(true);
        
        // Act
        var result = await _notificationService.SendWelcomeEmailAsync(email, userName);
        
        // Assert
        result.Should().BeTrue();
        
        _emailServiceMock.Verify(x => x.IsValidEmail(email), Times.Once);
        _emailServiceMock.Verify(x => x.SendEmailAsync(email, "Welcome!", 
                                 It.Is<string>(body => body.Contains(userName))), Times.Once);
        _loggerMock.Verify(x => x.LogInfo(It.Is<string>(msg => msg.Contains("sent successfully"))), 
                          Times.Once);
    }
    
    [Fact]
    public async Task SendWelcomeEmailAsync_InvalidEmail_LogsWarningAndReturnsFalse()
    {
        // Arrange
        var invalidEmail = "invalid-email";
        var userName = "John Doe";
        
        _emailServiceMock.Setup(x => x.IsValidEmail(invalidEmail)).Returns(false);
        
        // Act
        var result = await _notificationService.SendWelcomeEmailAsync(invalidEmail, userName);
        
        // Assert
        result.Should().BeFalse();
        
        _emailServiceMock.Verify(x => x.IsValidEmail(invalidEmail), Times.Once);
        _emailServiceMock.Verify(x => x.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), 
                                 It.IsAny<string>()), Times.Never);
        _loggerMock.Verify(x => x.LogWarning(It.Is<string>(msg => msg.Contains("Invalid email"))), 
                          Times.Once);
    }
    
    [Fact]
    public async Task SendNotificationAsync_ValidEmailAndPhone_SendsBoth()
    {
        // Arrange
        var email = "test@example.com";
        var phone = "+1234567890";
        var message = "Test notification";
        
        _emailServiceMock.Setup(x => x.IsValidEmail(email)).Returns(true);
        _emailServiceMock.Setup(x => x.SendEmailAsync(email, "Notification", message))
                        .ReturnsAsync(true);
        
        _smsServiceMock.Setup(x => x.IsValidPhoneNumber(phone)).Returns(true);
        _smsServiceMock.Setup(x => x.SendSmsAsync(phone, message)).ReturnsAsync(true);
        
        // Act
        var result = await _notificationService.SendNotificationAsync(email, phone, message);
        
        // Assert
        result.Should().BeTrue();
        
        _emailServiceMock.Verify(x => x.SendEmailAsync(email, "Notification", message), Times.Once);
        _smsServiceMock.Verify(x => x.SendSmsAsync(phone, message), Times.Once);
    }
    

}