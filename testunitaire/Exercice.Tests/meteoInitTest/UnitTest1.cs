using Xunit;
using meteoCode;

namespace meteoInitTest
{
    public class TemperatureTests
    {
        private readonly Temperature _temperature = new();

        [Fact]
        public void GetTemperature_ShouldReturnExpectedValueForKnownCity()
        {
            // Arrange
            string city = "Paris";
            double expected = 20.0; // Cette valeur doit être en cohérence avec l’implémentation de GetTemperature

            // Act
            double result = _temperature.GetTemperature(city);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}