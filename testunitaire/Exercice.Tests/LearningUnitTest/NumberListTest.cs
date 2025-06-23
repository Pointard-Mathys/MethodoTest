using FluentAssertions;
using Learning;

namespace LearningUnitTest;

public class NumberListTest
{
    [Fact]
    public void Add_Number_IncreasesCount()
    {
        // Arrange
        var list = new NumberList();
        int number = 5;
        
        // Act
        list.Add(number);
        
        // Assert
        Assert.Equal(1, list.Count());
        Assert.True(list.Contains(number));
    }

    [Fact]
    public void Remove_ExistingNumber_DecreasesCount()
    {
        // Arrange
        var list = new NumberList();
        list.Add(5);

        // Act
        bool removed = list.Remove(5);

        // Assert
        Assert.True(removed);
        Assert.Equal(0, list.Count());
        Assert.False(list.Contains(5));
    }

    [Fact]
    public void Remove_NonExistingNumber_ReturnsFalse()
    {
        // Arrange
        var list = new NumberList();
        list.Add(5);
        int number = 10;
        
        // Act
        bool removed = list.Remove(number);
        
        // Assert
        Assert.False(removed);
        Assert.Equal(1, list.Count());
        Assert.True(list.Contains(5));
    }

    [Fact]
    public void GetMax_WithNumbers_ReturnsMaximum()
    {
        // Arrange
        var list = new NumberList();
        list.Add(5);
        list.Add(42);
        list.Add(17);
        list.Add(99);
        list.Add(23);
        
        // Act
        int maximum = list.Max();
    
        // Assert
        maximum.Should().Be(99);

    }

    [Fact]
    public void GetAverage_WithNumbers_ReturnsCorrectAverage()
    {
        // Arrange
        var list = new NumberList();
        list.Add(5);
        list.Add(10);
        list.Add(15);
        list.Add(20);
        list.Add(25);
        list.Add(30);
        list.Add(35);
        
        // Act
        double average = list.Average();
    
        // Assert
        average.Should().Be(20);
    }
}