using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RentalManager.Infrastructure.ExceptionHandling.Exceptions;

public class PaymentNotFoundException : Exception, ICustomMappedException
{
    public PaymentNotFoundException(int id) : base($"Payment with id {id} not found")
    {
    }

    public ProblemDetails GetProblemDetails(Exception exception)
    {
        return new ProblemDetails
        {
            Title = "Payment not found",
            Detail = exception.Message,
            Status = StatusCodes.Status404NotFound
        };
    }
}