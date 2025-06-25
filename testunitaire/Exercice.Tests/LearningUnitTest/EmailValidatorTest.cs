using Learning;

namespace LearningUnitTest;

public class EmailValidatorTest
{
    private readonly EmailValidator _validator = new();

    /// <summary>
    /// Vérifie qu'un email bien formé est valide.
    /// </summary>
    [Fact]
    public void IsValidEmail_ValidEmail_ReturnsTrue()
    {
        var result = _validator.IsValidEmail("test@example.com");
        Assert.True(result);
    }

    /// <summary>
    /// Vérifie qu'un email vide retourne false.
    /// </summary>
    [Fact]
    public void IsValidEmail_EmptyString_ReturnsFalse()
    {
        var result = _validator.IsValidEmail("");
        Assert.False(result);
    }

    /// <summary>
    /// Vérifie qu'un email null retourne false.
    /// </summary>
    [Fact]
    public void IsValidEmail_Null_ReturnsFalse()
    {
        var result = _validator.IsValidEmail(null);
        Assert.False(result);
    }

    /// <summary>
    /// Vérifie qu’un email sans arobase est invalide.
    /// </summary>
    [Fact]
    public void IsValidEmail_MissingAtSymbol_ReturnsFalse()
    {
        var result = _validator.IsValidEmail("testexample.com");
        Assert.False(result);
    }

    /// <summary>
    /// Vérifie qu’un email avec des espaces est invalide.
    /// </summary>
    [Fact]
    public void IsValidEmail_EmailWithSpaces_ReturnsFalse()
    {
        var result = _validator.IsValidEmail("test @example.com");
        Assert.False(result);
    }

    // === Tests pour IsValidEmailWithPattern (Regex) ===

    /// <summary>
    /// Vérifie qu’un email simple est reconnu comme valide par la regex.
    /// </summary>
    [Fact]
    public void IsValidEmailWithPattern_ValidEmail_ReturnsTrue()
    {
        var result = _validator.IsValidEmailWithPattern("user@domain.com");
        Assert.True(result);
    }

    /// <summary>
    /// Vérifie que la regex refuse un email sans domaine.
    /// </summary>
    [Fact]
    public void IsValidEmailWithPattern_MissingDomain_ReturnsFalse()
    {
        var result = _validator.IsValidEmailWithPattern("user@");
        Assert.False(result);
    }

    /// <summary>
    /// Vérifie qu’un email sans arobase est invalide selon la regex.
    /// </summary>
    [Fact]
    public void IsValidEmailWithPattern_NoAtSymbol_ReturnsFalse()
    {
        var result = _validator.IsValidEmailWithPattern("userdomain.com");
        Assert.False(result);
    }

    /// <summary>
    /// Vérifie que la regex refuse un email vide.
    /// </summary>
    [Fact]
    public void IsValidEmailWithPattern_EmptyString_ReturnsFalse()
    {
        var result = _validator.IsValidEmailWithPattern("");
        Assert.False(result);
    }

    /// <summary>
    /// Vérifie que la regex refuse un email null.
    /// </summary>
    [Fact]
    public void IsValidEmailWithPattern_Null_ReturnsFalse()
    {
        var result = _validator.IsValidEmailWithPattern(null);
        Assert.False(result);
    }
}

