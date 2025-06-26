using FluentAssertions;
using Processor.Services;

namespace Processor.UnitTest;

public class PasswordValidatorTest
{
    // Verifie que differents mots de passe retournent un resultat valide ou invalide selon les regles.

    [Theory]
    [InlineData("Password123!", true)]  // Bon mot de passe
    [InlineData("password123!", false)] // Manque majuscule
    [InlineData("PASSWORD123!", false)] // Manque minuscule
    [InlineData("Password!", false)]    // Manque chiffre
    [InlineData("Pass12!", false)]     // Trop court (< 8)
    [InlineData("", false)]             // Vide
    [InlineData("Password 123!", false)] // Contient espace
    [InlineData("Password123", false)]   // Manque caractère spécial
    [InlineData("P@ssw0rdTooLooooooooooooooooooooooooooooooooooooooooooooooooooooooong!", false)] // Trop long (> 64)
    public void Validate_WithVariousInputs_ReturnsCorrectValidity(string password, bool expectedValid)
    {
        // Arrange
        var validator = new PasswordValidator();

        // Act
        var result = validator.Validate(password);

        // Assert
        result.IsValid.Should().Be(expectedValid);
    }

    // Verifie qu'un mot de passe sans majuscule retourne une erreur indiquant l'absence de majuscule.

    [Fact]
    public void Validate_WithNoUppercaseLetter_ReturnCorrectErrorMessage()
    {
        // Arrange
        var validator = new PasswordValidator();
        
        //Act
        var result = validator.Validate("password123");
        
        //Assert
        result.Errors.Should().Contain(e => e.ToLower().Contains("majuscule"));
    }

    // Verifie qu'un mot de passe sans minuscule retourne une erreur indiquant l'absence de minuscule.

    [Fact]
    public void Validate_WithNoLowercaseLetter_ReturnCorrectErrorMessage()
    {
        //Arrange
        var validator = new PasswordValidator();
        
        //Act
        var result = validator.Validate("PASSWORD123");
        
        //Assert
        result.Errors.Should().Contain(e => e.ToLower().Contains("minus"));
    }


    // Verifie qu'un mot de passe sans chiffre retourne une erreur indiquant l'absence de chiffre.

    [Fact]
    public void Validate_WithNoDigitLetter_ReturnCorrectErrorMessage()
    {
        //Arrange
        var validator = new PasswordValidator();
        
        //Act
        var result = validator.Validate("Password");
        
        //Assert
        result.Errors.Should().Contain(e => e.ToLower().Contains("chiffre"));
    }


    // Verifie qu'un mot de passe trop court genere une erreur sur la longueur minimale requise.

    [Fact]
    public void Validate_WithPasswordTooShort_ReturnCorrectErrorMessage()
    {
        //Arrange
        var validator = new PasswordValidator();
        
        //Act
        var result = validator.Validate("Pas1");
        
        //Assert
        result.Errors.Should().Contain(e => e.ToLower().Contains("8 caractères"));
    }


    // Verifie qu'un mot de passe vide declenche l'erreur appropriee.

    [Fact]
    public void Validate_WithEmptyPassword_ReturnCorrectErrorMessage()
    {
        //Arrange
        var validator = new PasswordValidator();
        
        //Act
        var result = validator.Validate("");
        
        //Assert
        result.Errors.Should().Contain(e => e.ToLower().Contains("vide"));
    }


    // Verifie que chaque mot de passe invalide declenche toutes les erreurs attendues (via donnees combinees).

    public static IEnumerable<object[]> PasswordWithErrors =>
        new List<object[]>
        {
            new object[] { "password", new[] { "majuscule", "chiffre", "caractère spécial" } },
            new object[] { "PASSWORD", new[] { "minuscule", "chiffre", "caractère spécial" } },
            new object[] { "", new[] { "vide" } },
            new object[] { "Password123", new[] { "caractère spécial" } },
            new object[] { "Password 123!", new[] { "espace" } },
            new object[] { "Password\t123!", new[] { "espace" } },
            new object[] { "Password\n123!", new[] { "espace" } },
            new object[] { "P@1", new[] { "8 caractères" } },
            new object[] { new string('A', 62) + "1a!", new[] { "64 caractères" } }
        };


    // Verifie qu'un mot de passe ne contenant que des caracteres speciaux renvoie les erreurs pour majuscule, minuscule et chiffre manquants.

    [Theory]
    [MemberData(nameof(PasswordWithErrors))]
    public void Validate_MotDePasseInvalide_ContientErreursAttendues(string password, string[] expectedErrorContains)
    {
        // Arrange
        var validator = new PasswordValidator();
    
        // Act
        var result = validator.Validate(password);
    
        // Assert
        foreach (var expectedError in expectedErrorContains)
        {
            result.Errors.Should().Contain(e => e.ToLower().Contains(expectedError.ToLower()));
        }
    }

    [Fact]
    public void Validate_OnlySpecialCharacters_ReturnsAllRelevantErrors()
    {
        // Arrange
        var validator = new PasswordValidator();
        var password = "!@#$%^&*()";

        // Act
        var result = validator.Validate(password);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ToLower().Contains("majuscule"));
        result.Errors.Should().Contain(e => e.ToLower().Contains("minuscule"));
        result.Errors.Should().Contain(e => e.ToLower().Contains("chiffre"));
    }
}