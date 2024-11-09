using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TerrytLookup.Infrastructure.ExceptionHandling.Exceptions;

public class TownNotFoundException(Guid id) : Exception($"Town with id {id} not found."), ICustomMappedException
{
    public ProblemDetails GetProblemDetails(Exception exception)
    {
        return new ProblemDetails
        {
            Title = "Town not found",
            Detail = exception.Message,
            Status = StatusCodes.Status404NotFound
        };
    }
}