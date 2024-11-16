using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TerrytLookup.Infrastructure.ExceptionHandling.Exceptions;

public class VoivodeshipNotFoundException(int id) : Exception($"Voivodeship with id {id} not found."), ICustomMappedException
{
    public ProblemDetails GetProblemDetails(Exception exception)
    {
        return new ProblemDetails
        {
            Title = "Voivodeship not found",
            Detail = exception.Message,
            Status = StatusCodes.Status404NotFound
        };
    }
}