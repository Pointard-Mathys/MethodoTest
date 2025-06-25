using FluentAssertions;
using Moq;
using Notification.Contracts;
using Notification.Model;
using Notification.Services;

namespace Notification.UnitTest;

// Classe de tests unitaires pour la logique métier de création d'utilisateur
public class UserServiceTest
{
    [Fact]
    public void CreateUser_WhenUserDoesNotExist_ShouldCreateUserAndSendEmail()
    {
        // Arrange
        var name = "Test User";
        var email = "test@domain.com";

        // Création des mocks du repository utilisateur et du service email
        var repoMock = new Mock<IUserRepository>();
        var emailMock = new Mock<IEmailService>();

        // Simule qu'aucun utilisateur n'existe avec cet email
        repoMock.Setup(r => r.Exists(email)).Returns(false);

        // Simule que l'enregistrement d'utilisateur est autorisé (pas d'exception ni de retour)
        repoMock.Setup(r => r.Save(It.IsAny<User>()));

        // Instancie le service avec les mocks
        var service = new UserService(repoMock.Object, emailMock.Object);

        // Act : création de l’utilisateur
        var result = service.CreateUser(name, email);

        // Assert :
        // 1. L’utilisateur retourné contient les bonnes infos
        result.Name.Should().Be(name);
        result.Email.Should().Be(email);

        // 2. L'utilisateur a bien été enregistré dans le repository
        repoMock.Verify(r => r.Save(It.Is<User>(u => u.Email == email && u.Name == name)), Times.Once);

        // 3. Un email de bienvenue a été envoyé
        emailMock.Verify(e => e.SendWelcomeEmail(email, name), Times.Once);
    }

    [Fact]
    public void CreateUser_WhenUserExists_ShouldThrowException()
    {
        // Arrange : simule un utilisateur déjà existant
        var repoMock = new Mock<IUserRepository>();
        var emailMock = new Mock<IEmailService>();

        // Quel que soit l’email, Exists retourne true
        repoMock.Setup(r => r.Exists(It.IsAny<string>())).Returns(true);

        var service = new UserService(repoMock.Object, emailMock.Object);

        // Act : tentative de création d’un utilisateur déjà existant
        Action act = () => service.CreateUser("John", "john@example.com");

        // Assert : une exception doit être levée
        act.Should().Throw<InvalidOperationException>()
            .WithMessage("User already exists");
    }
    
    [Fact]
    public void GetUser_WithValidId_ShouldReturnUser()
    {
        // Arrange
        var userId = 1;
        var expectedUser = new User { Id = userId, Name = "Test", Email = "test@domain.com" };

        var repoMock = new Mock<IUserRepository>();
        repoMock.Setup(r => r.GetById(userId)).Returns(expectedUser);

        var emailMock = new Mock<IEmailService>(); // pas utilisé ici mais requis par le constructeur
        var service = new UserService(repoMock.Object, emailMock.Object);

        // Act
        var result = service.GetUser(userId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(expectedUser); // compare tous les champs
        repoMock.Verify(r => r.GetById(userId), Times.Once); // vérifie que le repo a bien été appelé
    }

    [Fact]
    public void GetUser_WithUnknownId_ShouldReturnNull()
    {
        // Arrange
        var userId = 999;

        var repoMock = new Mock<IUserRepository>();
        repoMock.Setup(r => r.GetById(userId)).Returns((User?)null);

        var emailMock = new Mock<IEmailService>();
        var service = new UserService(repoMock.Object, emailMock.Object);

        // Act
        var result = service.GetUser(userId);

        // Assert
        result.Should().BeNull();
    }

}
