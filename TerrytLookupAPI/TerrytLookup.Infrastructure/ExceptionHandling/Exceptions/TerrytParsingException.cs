using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TerrytLookup.Infrastructure.ExceptionHandling.Exceptions;

public class TerrytParsingException(Exception innerException)
    : Exception("Unable to read file containing Terryt datasets.", innerException), ICustomMappedException
{
    public ProblemDetails GetProblemDetails(Exception exception)
    {
        return new ProblemDetails
        {
            Title = "Unable to read file",
            Detail = exception.Message,
            Status = StatusCodes.Status400BadRequest
        };
    }
}