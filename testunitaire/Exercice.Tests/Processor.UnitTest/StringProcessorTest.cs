using FluentAssertions;
using Processor.Services;

namespace Processor.UnitTest;

public class StringProcessorTest
{
    [Theory]
    [InlineData("hello", "olleh")]
    [InlineData("", "")]
    [InlineData("a", "a")]
    [InlineData("12345", "54321")]
    // Verifie que Reverse retourne bien la chaine inversee pour differents cas
    public void Reverse_VariousInputs_ReturnsReversedString(string input, string expected)
    {
        var processor = new StringProcessor();
        string result = processor.Reverse(input);
        result.Should().Be(expected);
    }

    [Fact]
    // Verifie que Reverse retourne null si l'entree est null
    public void Reverse_WithNullInput_ReturnsNull()
    {
        var processor = new StringProcessor();
        var result = processor.Reverse(null);
        result.Should().BeNull();
    }

    [Theory]
    [InlineData("radar", true)]
    [InlineData("hello", false)]
    [InlineData("A man a plan a canal Panama", true)]
    [InlineData("", true)]
    // Verifie que IsPalindrome detecte correctement si une chaine est un palindrome
    public void IsPalindrome_VariousInputs_ReturnsCorrectResult(string input, bool expected)
    {
        var processor = new StringProcessor();
        var result = processor.IsPalindrome(input);
        result.Should().Be(expected);
    }

    [Fact]
    // Verifie que IsPalindrome retourne false si l'entree est null
    public void IsPalindrome_WithNullInput_ReturnsFalse()
    {
        var processor = new StringProcessor();
        var result = processor.IsPalindrome(null);
        result.Should().BeFalse();
    }

    [Fact]
    // Verifie que les caracteres non alphanumeriques sont ignores dans le test de palindrome
    public void IsPalindrome_WithNonAlphanumericCharacters_IsIgnoredInCheck()
    {
        var processor = new StringProcessor();
        var result = processor.IsPalindrome("A!@#a");
        result.Should().BeTrue();
    }

    [Theory]
    [InlineData("Hello world", 2)]
    [InlineData("   Leading and trailing spaces   ", 4)]
    [InlineData("Multiple    spaces    between    words", 4)]
    [InlineData("", 0)]
    [InlineData("     ", 0)]
    [InlineData("Word", 1)]
    // Verifie que CountWords retourne le bon nombre de mots dans differents cas
    public void CountWords_VariousInputs_ReturnsCorrectCount(string input, int expectedCount)
    {
        var processor = new StringProcessor();
        int result = processor.CountWords(input);
        result.Should().Be(expectedCount);
    }

    [Fact]
    // Verifie que CountWords retourne 0 si l'entree est null
    public void CountWords_WithNullInput_ReturnsZero()
    {
        var processor = new StringProcessor();
        var result = processor.CountWords(null);
        result.Should().Be(0);
    }

    [Fact]
    // Verifie que CountWords retourne 0 si la chaine contient uniquement des espaces
    public void CountWords_WithOnlySpaces_ReturnsZero()
    {
        var processor = new StringProcessor();
        var result = processor.CountWords("     ");
        result.Should().Be(0);
    }

    [Theory]
    [InlineData("hello", "Hello")]
    [InlineData("Hello", "Hello")]
    [InlineData("hELLO", "HELLO")]
    [InlineData("", "")]
    [InlineData(null, null)]
    [InlineData("1hello", "1hello")]
    // Verifie que Capitalize met en majuscule la premiere lettre si applicable
    public void Capitalize_VariousInputs_ReturnsCorrectString(string input, string expected)
    {
        var processor = new StringProcessor();
        string result = processor.Capitalize(input);
        result.Should().Be(expected);
    }

    [Fact]
    // Verifie que Capitalize retourne null si l'entree est null
    public void Capitalize_WithNullInput_ReturnsNull()
    {
        var processor = new StringProcessor();
        var result = processor.Capitalize(null);
        result.Should().BeNull();
    }

    [Fact]
    // Verifie que Capitalize fonctionne correctement avec une seule lettre
    public void Capitalize_WithOneCharacter_ReturnsUppercase()
    {
        var processor = new StringProcessor();
        var result = processor.Capitalize("x");
        result.Should().Be("X");
    }
}
