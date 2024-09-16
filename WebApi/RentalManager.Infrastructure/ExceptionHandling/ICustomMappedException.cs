using Microsoft.AspNetCore.Mvc;

namespace RentalManager.Infrastructure.ExceptionHandling;

public interface ICustomMappedException
{
    public ProblemDetails GetProblemDetails(Exception exception);
}