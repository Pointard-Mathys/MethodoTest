using FluentAssertions;
using Processor.Services;

namespace Processor.UnitTest;

public class PasswordValidatorTest
{
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
        Assert.Equal(expectedValid, result.IsValid);
    }

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

    [Fact]
    public void Validate_WithNoLowercaseLetter_ReturnCorrectErrorMessage()
    {
        //Arrange
        var validator = new PasswordValidator();
        
        //Act
        var result = validator.Validate("PASSWORD123");
        
        //Assert
        Assert.Contains("minuscule", result.Errors.FirstOrDefault()?.ToLower());
    }

    [Fact]
    public void Validate_WithNoDigitLetter_ReturnCorrectErrorMessage()
    {
        //Arrange
        var validator = new PasswordValidator();
        
        //Act
        var result = validator.Validate("Password");
        
        //Assert
        Assert.Contains("chiffre", result.Errors.FirstOrDefault()?.ToLower());
    }

    [Fact]
    public void Validate_WithPasswordTooShort_ReturnCorrectErrorMessage()
    {
        //Arrange
        var validator = new PasswordValidator();
        
        //Act
        var result = validator.Validate("Pas1");
        
        //Assert
        Assert.Contains("8 caractères", result.Errors.FirstOrDefault());
    }

    [Fact]
    public void Validate_WithEmptyPassword_ReturnCorrectErrorMessage()
    {
        //Arrange
        var validator = new PasswordValidator();
        
        //Act
        var result = validator.Validate("");
        
        //Assert
        Assert.Contains("vide", result.Errors.FirstOrDefault()?.ToLower());
    }

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
            Assert.Contains(result.Errors, e => e.ToLower().Contains(expectedError.ToLower()));
        }
    }
}