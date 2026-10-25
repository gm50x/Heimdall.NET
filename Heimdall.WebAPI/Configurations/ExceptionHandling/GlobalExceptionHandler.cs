using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Heimdall.WebAPI.Configurations.ExceptionHandling;

public class GlobalExceptionHandler(
    ILogger<GlobalExceptionHandler> _logger,
    IWebHostEnvironment _env)
    : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        string traceId = Activity.Current?.Id ?? httpContext.TraceIdentifier;
        _logger.LogError(exception, "Unhandled exception occurred, TraceId: {TraceId}", traceId);

        var statusCode = StatusCodes.Status500InternalServerError;

        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = "An unexpected error occurred.",
            Detail = _env.IsDevelopment() ? exception.ToString() : null,
            Instance = httpContext.Request.Path,
        };


        problemDetails.Extensions["traceKey"] = traceId;

        httpContext.Response.StatusCode = statusCode;

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}
