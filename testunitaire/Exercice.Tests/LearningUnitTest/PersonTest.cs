namespace LearningUnitTest;

public class PersonTest
{
    [Fact]
    public void GetFullName_WithFirstAndLastName_ReturnsFormattedName()
    {
     
    }

    [Theory]
    [InlineData(17, false)]
    [InlineData(18, true)]
    [InlineData(25, true)]
    [InlineData(0, false)]
    public void IsAdult_DifferentAges_ReturnsCorrectResult(int age, bool expected)
    {
        
    }

    [Theory]
    [InlineData(-1, false)]
    [InlineData(0, true)]
    [InlineData(25, true)]
    [InlineData(150, true)]
    [InlineData(151, false)]
    public void IsValidAge_DifferentAges_ReturnsCorrectResult(int age, bool expected)
    {
    
    }
}