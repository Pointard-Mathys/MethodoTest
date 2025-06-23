using FluentAssertions;
using Learning;

namespace LearningUnitTest;

public class PersonTest
{
    [Fact]
    public void GetFullName_WithFirstAndLastName_ReturnsFormattedName()
    {
        // Arrange
        var person = new Person { FirstName = "John", LastName = "Doe" };
        
        // Act
        string fullName = person.GetFullName();

        // Assert
        fullName.Should().Be("John Doe");
    }

    [Theory]
    [InlineData(17, false)]
    [InlineData(18, true)]
    [InlineData(25, true)]
    [InlineData(0, false)]
    public void IsAdult_DifferentAges_ReturnsCorrectResult(int age, bool expected)
    {
        // Arrange
        var person = new Person { Age = age };

        // Act
        bool isAdult = person.IsAdult();

        // Assert
        isAdult.Should().Be(expected);
    }

    [Theory]
    [InlineData(-1, false)]
    [InlineData(0, true)]
    [InlineData(25, true)]
    [InlineData(150, true)]
    [InlineData(151, false)]
    public void IsValidAge_DifferentAges_ReturnsCorrectResult(int age, bool expected)
    {
        // Arrange
        var person = new Person { Age = age };

        // Act
        bool isValidAge = person.IsValidAge();

        // Assert
        isValidAge.Should().Be(expected);
    }
}