using Microsoft.AspNetCore.Mvc;

namespace Heimdall.WebAPI.Features.WeatherForecasting.v2.Models;

public record WeatherForecastRequest
{
    [FromQuery(Name = "unit")]
    public TemperatureUnit Unit { get; set; }
}

public record WeatherForecastResponse
{
    public required IEnumerable<WeatherForecast> Forecast { get; set; }
    public DateTimeOffset ForecastedAt { get; set; } = DateTimeOffset.UtcNow;
}