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
    public void Reverse_VariousInputs_ReturnsReversedString(string input, string expected)
    {
        //Arrange
        var processor = new StringProcessor();
        
        // Act
        string result = processor.Reverse(input);
        
        // Assert
        result.Should().Be(expected);
    }
    
    [Fact]
    public void Reverse_WithNullInput_ReturnsNull()
    {
        // Arrange
        var processor = new StringProcessor();

        // Act
        var result = processor.Reverse(null);

        // Assert
        result.Should().BeNull();
    }
    
    [Theory]
    [InlineData("radar", true)]
    [InlineData("hello", false)]
    [InlineData("A man a plan a canal Panama", true)]
    [InlineData("", true)]
    public void IsPalindrome_VariousInputs_ReturnsCorrectResult(string input, bool expected)
    {
        //Arrange
        var processor = new StringProcessor();
        
        // Act
        var result = processor.IsPalindrome(input);
        
        // Assert
        result.Should().Be(expected);
    }
    
    [Fact]
    public void IsPalindrome_WithNullInput_ReturnsFalse()
    {
        // Arrange
        var processor = new StringProcessor();

        // Act
        var result = processor.IsPalindrome(null);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void IsPalindrome_WithNonAlphanumericCharacters_IsIgnoredInCheck()
    {
        // Arrange
        var processor = new StringProcessor();

        // Act
        var result = processor.IsPalindrome("A!@#a");

        // Assert
        result.Should().BeTrue();
    }
    
    [Theory]
    [InlineData("Hello world", 2)]
    [InlineData("   Leading and trailing spaces   ", 4)]
    [InlineData("Multiple    spaces    between    words", 4)]
    [InlineData("", 0)]
    [InlineData("     ", 0)]
    [InlineData("Word", 1)]
    public void CountWords_VariousInputs_ReturnsCorrectCount(string input, int expectedCount)
    {
        // Arrange
        var processor = new StringProcessor();

        // Act
        int result = processor.CountWords(input);

        // Assert
        result.Should().Be(expectedCount);
    }
    
    [Fact]
    public void CountWords_WithNullInput_ReturnsZero()
    {
        //Arrange
        var processor = new StringProcessor();
        
        //Act
        var result = processor.CountWords(null);
        
        //Asset
        result.Should().Be(0);
    }

    [Fact]
    public void CountWords_WithOnlySpaces_ReturnsZero()
    {
        //Arrange
        var processor = new StringProcessor();
        
        //Act
        var result = processor.CountWords("     ");
        
        //Assert
        result.Should().Be(0);
    }

    [Theory]
    [InlineData("hello", "Hello")]
    [InlineData("Hello", "Hello")]
    [InlineData("hELLO", "HELLO")]
    [InlineData("", "")]
    [InlineData(null, null)]
    [InlineData("1hello", "1hello")]
    public void Capitalize_VariousInputs_ReturnsCorrectString(string input, string expected)
    {
        // Arrange
        var processor = new StringProcessor();

        // Act
        string result = processor.Capitalize(input);

        //Assert
        result.Should().Be(expected);
    }
    
    [Fact]
    public void Capitalize_WithNullInput_ReturnsNull()
    {
        //Arrange
        var processor = new StringProcessor();
        
        //Act
        var result = processor.Capitalize(null);
        
        //Assert
        result.Should().BeNull();
    }

    [Fact]
    public void Capitalize_WithOneCharacter_ReturnsUppercase()
    {
        //Arrange
        var processor = new StringProcessor();
        
        //Act
        var result = processor.Capitalize("x");
        
        //Assert
        result.Should().Be("X");
    }
}