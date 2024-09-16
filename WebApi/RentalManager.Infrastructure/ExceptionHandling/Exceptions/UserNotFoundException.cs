using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RentalManager.Infrastructure.ExceptionHandling.Exceptions;

public class UserNotFoundException : Exception, ICustomMappedException
{
    public UserNotFoundException(int id) : base($"User with id {id} not found")
    {
    }

    public ProblemDetails GetProblemDetails(Exception exception)
    {
        return new ProblemDetails
        {
            Title = "User not found",
            Detail = exception.Message,
            Status = StatusCodes.Status404NotFound
        };
    }
}