using Learning;

namespace LearningUnitTest;

public class NumberListTest
{
    [Fact]
    public void Add_Number_IncreasesCount()
    {

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
       
    }

    [Fact]
    public void GetMax_WithNumbers_ReturnsMaximum()
    {
       
    }

    [Fact]
    public void GetAverage_WithNumbers_ReturnsCorrectAverage()
    {
       
    }
}