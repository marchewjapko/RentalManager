using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TerrytLookup.Infrastructure.ExceptionHandling.Exceptions;

public class DatabaseNotEmptyException() : Exception("Unable to initialize registers. The database must be empty."), ICustomMappedException
{
    public ProblemDetails GetProblemDetails(Exception exception)
    {
        return new ProblemDetails
        {
            Title = "Unable to initialize registers",
            Detail = exception.Message,
            Status = StatusCodes.Status409Conflict
        };
    }
}