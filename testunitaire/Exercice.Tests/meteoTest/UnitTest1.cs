using meteoCode;

namespace meteoTest;

public class TemperatureTests
{
    private readonly Temperature _temperature = new();

    [Fact]
    public void GetTemperature_ReturnsExpectedValue()
    {
        // Arrange
        string city = "Paris";
        double expected = 20.0;

        // Act
        double result = _temperature.GetTemperature(city);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(5, "La température actuelle est de 5°C. Il fait froid!")]
    [InlineData(15, "La température actuelle est de 15°C. La température est agréable.")]
    [InlineData(25, "La température actuelle est de 25°C. Il fait chaud!")]
    public void GetTemperatureDescription_ReturnsCorrectDescription(double temp, string expected)
    {
        // Act
        string result = _temperature.GetTemperatureDescription(temp);

        // Assert
        Assert.Equal(expected, result);
    }
}