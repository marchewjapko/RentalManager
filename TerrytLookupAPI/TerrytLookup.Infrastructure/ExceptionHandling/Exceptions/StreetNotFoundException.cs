using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TerrytLookup.Infrastructure.ExceptionHandling.Exceptions;

public class StreetNotFoundException(int townId, int nameId) : Exception($"Street with id {townId}/{nameId} not found."), ICustomMappedException
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