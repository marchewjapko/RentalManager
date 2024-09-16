using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RentalManager.Infrastructure.ExceptionHandling.Exceptions;

public class AgreementNotFoundException : Exception, ICustomMappedException
{
    public AgreementNotFoundException(int id) : base($"Agreement with id {id} not found")
    {
    }

    public ProblemDetails GetProblemDetails(Exception exception)
    {
        return new ProblemDetails
        {
            Title = "Agreement not found",
            Detail = exception.Message,
            Status = StatusCodes.Status404NotFound
        };
    }
}