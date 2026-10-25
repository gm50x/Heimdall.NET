using Asp.Versioning;
using Heimdall.WebAPI.Features.WeatherForecasting.v2.Models;
using Microsoft.AspNetCore.Mvc;

namespace Heimdall.WebAPI.Features.WeatherForecasting.v2;

[ApiController]
[ApiVersion("2")]
[Route("api/v{version:apiVersion}/[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries =
    [
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    ];

    [HttpGet(Name = "GetWeatherForecast")]
    [ProducesResponseType(typeof(WeatherForecastResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public IActionResult Get([FromQuery] WeatherForecastRequest request)
    {
        var data = GetForecast(request);
        return Ok(new WeatherForecastResponse
        {
            Forecast = data,
        });
    }

    private IEnumerable<WeatherForecast> GetForecast(WeatherForecastRequest request)
    {
        return Enumerable
                .Range(1, 5)
                .Select(index =>
                {
                    var forecast = new WeatherForecast
                    {
                        Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                        Temperature = Random.Shared.Next(-20, 55),
                        Unit = TemperatureUnit.Celsius,
                        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                    };
                    return request.Unit is TemperatureUnit.Celsius
                        ? forecast
                        : forecast.AsFahrenheit();
                });
    }
}
