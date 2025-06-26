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
    // Test unitaire : vérifie que GetUser retourne null si l'utilisateur n'existe pas dans la base
    public void GetUser_WithUnknownId_ShouldReturnNull()
    {
        // Arrange : préparation du test

        var userId = 999; // ID d'utilisateur inexistant

        // Création d'un mock du dépôt utilisateur
        var repoMock = new Mock<IUserRepository>();

        // Configuration du mock : lorsqu'on appelle GetById avec cet ID, on retourne null
        repoMock.Setup(r => r.GetById(userId)).Returns((User?)null);

        // Création d'un mock du service email (non utilisé ici mais requis par le constructeur)
        var emailMock = new Mock<IEmailService>();

        // Instanciation du UserService avec les mocks
        var service = new UserService(repoMock.Object, emailMock.Object);

        // Act : appel de la méthode à tester
        var result = service.GetUser(userId);

        // Assert : vérifie que la méthode retourne bien null
        result.Should().BeNull();
    }

}
