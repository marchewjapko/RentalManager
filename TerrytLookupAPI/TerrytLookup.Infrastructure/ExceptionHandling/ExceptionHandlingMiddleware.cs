using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace TerrytLookup.Infrastructure.ExceptionHandling;

public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        logger.LogError(exception, exception.Message);

        context.Response.ContentType = "application/json";

        var problemDetail = new ProblemDetails
        {
            Title = "Internal Server Error",
            Detail = "Server has encountered an error",
            Status = StatusCodes.Status500InternalServerError
        };

        if (exception is ICustomMappedException e)
        {
            problemDetail = e.GetProblemDetails(exception);
        }

        context.Response.StatusCode = problemDetail.Status!.Value;

        return context.Response.WriteAsJsonAsync(problemDetail);
    }
}