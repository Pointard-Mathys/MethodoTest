namespace meteoCode;

public class Temperature
{
    public double GetTemperature(string city)
    {
        return 20.0;
    }

    public string GetTemperatureDescription(double temperature)
    {
        return $"La température actuelle est de {temperature}°C. " + 
               (temperature < 10 ? "Il fait froid!" :
                temperature < 20 ? "La température est agréable." :
                "Il fait chaud!");
    }
}