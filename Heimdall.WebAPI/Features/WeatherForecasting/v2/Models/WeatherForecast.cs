namespace Heimdall.WebAPI.Features.WeatherForecasting.v2.Models;

public enum TemperatureUnit
{
    Celsius,
    Fahrenheit
}

public record WeatherForecast
{
    public DateOnly Date { get; set; }

    public TemperatureUnit Unit { get; set; }
    public int Temperature { get; set; }

    public string? Summary { get; set; }

    public WeatherForecast AsFahrenheit()
    {
        if (Unit == TemperatureUnit.Fahrenheit)
        {
            return this;
        }

        return this with
        {
            Unit = TemperatureUnit.Fahrenheit,
            Temperature = ConvertToFahrenheit(Temperature)
        };

    }

    private static int ConvertToFahrenheit(int temperatureC)
        => 32 + (int)(temperatureC / 0.5556);
}
