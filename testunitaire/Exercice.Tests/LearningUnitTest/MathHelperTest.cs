
using Learning;

namespace LearningUnitTest;

public class MathHelperTest
{
    private readonly MathHelper _helper = new();
    
    /// <summary>
    /// Vérifie qu’un nombre pair retourne true.
    /// </summary>
    [Fact]
    public void IsEven_EvenNumber_ReturnsTrue()
    {
        int number = 4;

        // Act
        bool result = _helper.IsEven(number);

        // Assert
        Assert.True(result);
    }
    
    /// <summary>
    /// Vérifie qu’un nombre impair retourne false.
    /// </summary>
    [Fact]
    public void IsEven_OddNumber_ReturnsFalse()
    {
        // Arrange
        int number = 5;

        // Act
        bool result = _helper.IsEven(number);

        // Assert
        Assert.False(result);
    }

    /// <summary>
    /// Vérifie que zéro est considéré comme pair.
    /// </summary>
    [Fact]
    public void IsEven_Zero_ReturnsTrue()
    {
        // Arrange
        int number = 0;

        // Act
        bool result = _helper.IsEven(number);

        // Assert
        Assert.True(result);
    }

    /// <summary>
    /// Vérifie qu’un nombre négatif pair retourne true.
    /// </summary>
    [Fact]
    public void IsEven_NegativeEvenNumber_ReturnsTrue()
    {
        // Arrange
        int number = -6;

        // Act
        bool result = _helper.IsEven(number);

        // Assert
        Assert.True(result);
    }

    /// <summary>
    /// Vérifie qu’un nombre négatif impair retourne false.
    /// </summary>
    [Fact]
    public void IsEven_NegativeOddNumber_ReturnsFalse()
    {
        // Arrange
        int number = -3;

        // Act
        bool result = _helper.IsEven(number);

        // Assert
        Assert.False(result);
    }
}