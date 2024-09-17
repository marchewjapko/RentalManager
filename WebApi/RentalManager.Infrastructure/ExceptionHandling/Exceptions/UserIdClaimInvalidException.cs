using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RentalManager.Infrastructure.ExceptionHandling.Exceptions;

public class UserIdClaimInvalidException : Exception, ICustomMappedException
{
    public UserIdClaimInvalidException(string claim) : base($"User's claim {claim} is not valid.")
    {
    }

    public ProblemDetails GetProblemDetails(Exception exception)
    {
        return new ProblemDetails
        {
            Title = "Invalid ID claim",
            Detail = exception.Message,
            Status = StatusCodes.Status406NotAcceptable
        };
    }
}