using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RentalManager.Infrastructure.ExceptionHandling.Exceptions;

public class UserDoesNotHaveIdClaimException : Exception, ICustomMappedException
{
    public UserDoesNotHaveIdClaimException() : base($"Users does not have an id claim.")
    {
    }

    public ProblemDetails GetProblemDetails(Exception exception)
    {
        return new ProblemDetails
        {
            Title = "No ID claim found",
            Detail = exception.Message,
            Status = StatusCodes.Status406NotAcceptable
        };
    }
}