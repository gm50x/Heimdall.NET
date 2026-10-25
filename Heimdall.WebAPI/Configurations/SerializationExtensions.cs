using System.Text.Json.Serialization;
using MvcJsonOptions = Microsoft.AspNetCore.Mvc.JsonOptions;

namespace Heimdall.WebAPI.Configurations;

public static class SerializationExtensions
{
    public static void ConfigureSerialization(this IServiceCollection services)
    {
        var converter = new JsonStringEnumConverter();
        services.Configure<MvcJsonOptions>(options =>
        {
            options.JsonSerializerOptions.Converters.Add(converter);
        });

        services.ConfigureHttpJsonOptions(options =>
        {
            options.SerializerOptions.Converters.Add(converter);
        });
    }
}
