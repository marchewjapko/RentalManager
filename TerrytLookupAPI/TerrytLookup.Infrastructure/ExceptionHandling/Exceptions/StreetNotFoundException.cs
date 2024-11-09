using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TerrytLookup.Infrastructure.ExceptionHandling.Exceptions;

public class StreetNotFoundException(Guid id) : Exception($"Street with id {id} not found."), ICustomMappedException
{
    public ProblemDetails GetProblemDetails(Exception exception)
    {
        return new ProblemDetails
        {
            Title = "Street not found",
            Detail = exception.Message,
            Status = StatusCodes.Status404NotFound
        };
    }
}