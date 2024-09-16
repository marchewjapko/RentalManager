using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RentalManager.Infrastructure.ExceptionHandling.Exceptions;

public class ClientNotFoundException : Exception, ICustomMappedException
{
    public ClientNotFoundException(int id) : base($"Client with id {id} not found")
    {
    }

    public ProblemDetails GetProblemDetails(Exception exception)
    {
        return new ProblemDetails
        {
            Title = "Client not found",
            Detail = exception.Message,
            Status = StatusCodes.Status404NotFound
        };
    }
}