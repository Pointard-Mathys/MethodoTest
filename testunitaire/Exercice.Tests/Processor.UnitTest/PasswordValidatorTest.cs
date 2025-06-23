using Processor.Services;

namespace Processor.UnitTest;

public class PasswordValidatorTest
{
    
    [Theory]
    [InlineData("Password123", true)]
    [InlineData("password123", false)] // Pas de majuscule
    [InlineData("PASSWORD123", false)] // Pas de minuscule
    [InlineData("Password", false)]    // Pas de chiffre
    [InlineData("Pass123", false)]     // Trop court
    [InlineData("", false)]            // Vide
    public void Validate_WithDifferentPassword_ReturnsCorrectResult(string password, bool expectedValid)
    {
        // Arrange
        
        // Act
        
        // Assert
    }
    
    [Fact]
    public void Validate_WithNoUppercaseLetter_ReturnCorrectErrorMessage ()
    {
        // Arrange
        
        // Act
        
        // Assert
    }
    
    [Fact]
    public void Validate_WithNoLowercaseLetter_ReturnCorrectErrorMessage ()
    {
        // Arrange
        
        // Act
        
        // Assert
    }
    
    [Fact]
    public void Validate_WithNoDigitLetter_ReturnCorrectErrorMessage ()
    {
        // Arrange
        
        // Act
        
        // Assert
    }
    
    [Fact]
    public void Validate_WithPasswordTooShort_ReturnCorrectErrorMessage ()
    {
        // Arrange
        
        // Act
        
        // Assert
    }
    
    [Fact]
    public void Validate_WithEmptyPassword_ReturnCorrectErrorMessage ()
    {
        // Arrange
        
        // Act
        
        // Assert
    }

    /// <summary>
    /// Validates that an invalid password returns the expected set of error messages.
    /// </summary>
    /// <param name="password">The password to be validated.</param>
    /// <param name="expectedErrorContains">An array of expected error messages or substrings that should be included in the validation errors.</param>
    // [Theory]
    // [MemberData(nameof(PasswordWithErrors))]
    public void Validate_MotDePasseInvalide_ContientErreursAttendues(
        string password, string[] expectedErrorContains)
    {
        // Arrange
        
        // Act
        
        // Assert
        foreach (var expectedError in expectedErrorContains)
        {
           
        }
    }


}