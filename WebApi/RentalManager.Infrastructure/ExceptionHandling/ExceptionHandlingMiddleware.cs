using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RentalManager.Infrastructure.ExceptionHandling;

public class ExceptionHandlingMiddleware(RequestDelegate next)
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

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
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
        await context.Response.WriteAsJsonAsync(problemDetail);
    }
}