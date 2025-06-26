using FluentAssertions;
using Moq;
using Notification.Contracts;
using Notification.Services;

namespace Notification.UnitTest;

public class NotificationServiceTest
{
    // Mocks des services utilisés par NotificationService
    private readonly Mock<IEmailService> _emailServiceMock;
    private readonly Mock<ISmsService> _smsServiceMock;
    private readonly Mock<ILogger> _loggerMock;

    // Instance du service à tester
    private readonly NotificationService _notificationService;

    // Constructeur d'initialisation des mocks et de l'objet à tester
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
        // Arrange : configuration d'un email valide et simulation d'un envoi réussi
        var email = "test@example.com";
        var userName = "John Doe";

        _emailServiceMock.Setup(x => x.IsValidEmail(email)).Returns(true);
        _emailServiceMock.Setup(x => x.SendEmailAsync(email, "Welcome!", It.IsAny<string>()))
                         .ReturnsAsync(true);

        // Act : appel de la méthode testée
        var result = await _notificationService.SendWelcomeEmailAsync(email, userName);

        // Assert : vérifie que tout s’est bien passé
        result.Should().BeTrue();

        _emailServiceMock.Verify(x => x.IsValidEmail(email), Times.Once);
        _emailServiceMock.Verify(x => x.SendEmailAsync(email, "Welcome!", 
            It.Is<string>(body => body.Contains(userName))), Times.Once);
        _loggerMock.Verify(x => x.LogInfo(It.Is<string>(msg => msg.Contains("sent successfully"))), Times.Once);
    }

    [Fact]
    public async Task SendWelcomeEmailAsync_InvalidEmail_LogsWarningAndReturnsFalse()
    {
        // Arrange : configuration d’un email invalide
        var invalidEmail = "invalid-email";
        var userName = "John Doe";

        _emailServiceMock.Setup(x => x.IsValidEmail(invalidEmail)).Returns(false);

        // Act
        var result = await _notificationService.SendWelcomeEmailAsync(invalidEmail, userName);

        // Assert : vérifie qu’aucun email n’est envoyé et qu’un avertissement est loggé
        result.Should().BeFalse();

        _emailServiceMock.Verify(x => x.IsValidEmail(invalidEmail), Times.Once);
        _emailServiceMock.Verify(x => x.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        _loggerMock.Verify(x => x.LogWarning(It.Is<string>(msg => msg.Contains("Invalid email"))), Times.Once);
    }

    [Fact]
    public async Task SendNotificationAsync_ValidEmailAndPhone_SendsBoth()
    {
        // Arrange : email et téléphone valides, simulateurs de succès pour l’envoi
        var email = "test@example.com";
        var phone = "+1234567890";
        var message = "Test notification";

        _emailServiceMock.Setup(x => x.IsValidEmail(email)).Returns(true);
        _emailServiceMock.Setup(x => x.SendEmailAsync(email, "Notification", message)).ReturnsAsync(true);

        _smsServiceMock.Setup(x => x.IsValidPhoneNumber(phone)).Returns(true);
        _smsServiceMock.Setup(x => x.SendSmsAsync(phone, message)).ReturnsAsync(true);

        // Act
        var result = await _notificationService.SendNotificationAsync(email, phone, message);

        // Assert : les deux moyens de communication doivent être appelés
        result.Should().BeTrue();

        _emailServiceMock.Verify(x => x.SendEmailAsync(email, "Notification", message), Times.Once);
        _smsServiceMock.Verify(x => x.SendSmsAsync(phone, message), Times.Once);
    }

    [Fact]
    public async Task SendNotificationAsync_EmailFailsButSmsSucceeds_ShouldReturnTrue()
    {
        // Arrange : email échoue, SMS réussit
        var email = "test@example.com";
        var phone = "+1234567890";
        var message = "Test message";

        _emailServiceMock.Setup(x => x.IsValidEmail(email)).Returns(true);
        _emailServiceMock.Setup(x => x.SendEmailAsync(email, "Notification", message)).ReturnsAsync(false); // Échec email

        _smsServiceMock.Setup(x => x.IsValidPhoneNumber(phone)).Returns(true);
        _smsServiceMock.Setup(x => x.SendSmsAsync(phone, message)).ReturnsAsync(true); // Succès SMS

        // Act
        var result = await _notificationService.SendNotificationAsync(email, phone, message);

        // Assert : même si l’email échoue, le SMS suffit pour retourner `true`
        result.Should().BeTrue();
        _emailServiceMock.Verify(x => x.SendEmailAsync(email, "Notification", message), Times.Once);
        _smsServiceMock.Verify(x => x.SendSmsAsync(phone, message), Times.Once);
    }

    [Fact]
    public async Task SendWelcomeEmailAsync_CapturesEmailParameters()
    {
        // Arrange : capturer les paramètres du corps de l’email pour vérification
        var email = "capture@example.com";
        var userName = "Test User";
        string? capturedBody = null;

        _emailServiceMock.Setup(x => x.IsValidEmail(email)).Returns(true);
        _emailServiceMock.Setup(x => x.SendEmailAsync(email, "Welcome!", It.IsAny<string>()))
            .Callback<string, string, string>((to, subject, body) =>
            {
                capturedBody = body; // Capture du corps de l’email
            })
            .ReturnsAsync(true);

        // Act
        await _notificationService.SendWelcomeEmailAsync(email, userName);

        // Assert : le corps doit contenir le nom d’utilisateur
        capturedBody.Should().Contain(userName);
    }

    [Fact]
    public async Task SendWelcomeEmailAsync_EmailFails_ShouldLogError()
    {
        // Arrange : l’envoi de l’email échoue
        var email = "fail@example.com";
        var userName = "Fail User";

        _emailServiceMock.Setup(x => x.IsValidEmail(email)).Returns(true);
        _emailServiceMock.Setup(x => x.SendEmailAsync(email, It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(false);

        // Act
        var result = await _notificationService.SendWelcomeEmailAsync(email, userName);

        // Assert : le résultat est false et une erreur est loggée
        result.Should().BeFalse();
        _loggerMock.Verify(x => x.LogError(It.Is<string>(msg => msg.Contains("Failed to send welcome email"))), Times.Once);
    }
    
    [Fact]
    public async Task SendWelcomeEmailAsync_EmailServiceThrowsException_ShouldLogErrorAndReturnFalse()
    {
        // Arrange
        var email = "exception@example.com";
        var userName = "Throwing User";

        _emailServiceMock.Setup(x => x.IsValidEmail(email)).Returns(true);
    
        // Simulation d'une exception pendant l'envoi de l'email
        _emailServiceMock
            .Setup(x => x.SendEmailAsync(email, It.IsAny<string>(), It.IsAny<string>()))
            .ThrowsAsync(new Exception("SMTP server unreachable"));

        // Act
        var result = await _notificationService.SendWelcomeEmailAsync(email, userName);

        // Assert
        result.Should().BeFalse(); // Le service doit retourner false en cas d'exception

        // Vérifie que l'erreur a bien été loggée
        _loggerMock.Verify(x => x.LogError(It.Is<string>(msg => msg.Contains("Exception") || msg.Contains("SMTP"))), Times.Once);
    }

}
