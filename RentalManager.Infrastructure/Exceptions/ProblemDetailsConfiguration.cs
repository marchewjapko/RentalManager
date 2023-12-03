using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace RentalManager.Infrastructure.Exceptions;

public static class ProblemDetailsConfiguration
{
    public static void ConfigureCustomProblemDetails(IServiceCollection services,
        IWebHostEnvironment environment)
    {
        services.AddProblemDetails(options =>
            options.CustomizeProblemDetails = context => {
                var exception = context.HttpContext.Features.Get<IExceptionHandlerPathFeature>()
                    ?.Error;
                context.ProblemDetails.Type = null;
                context.ProblemDetails.Extensions.Remove("traceId");

                switch (exception)
                {
                    case EmployeeNotFoundException:
                        context.ProblemDetails.Title = "Employee not found";
                        context.ProblemDetails.Detail = exception.Message;
                        context.ProblemDetails.Status = StatusCodes.Status404NotFound;
                        context.HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;

                        break;
                    
                    case AgreementNotFoundException:
                        context.ProblemDetails.Title = "Agreement not found";
                        context.ProblemDetails.Detail = exception.Message;
                        context.ProblemDetails.Status = StatusCodes.Status404NotFound;
                        context.HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;

                        break;
                    
                    case ClientNotFoundException:
                        context.ProblemDetails.Title = "Client not found";
                        context.ProblemDetails.Detail = exception.Message;
                        context.ProblemDetails.Status = StatusCodes.Status404NotFound;
                        context.HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;

                        break;
                    
                    case EquipmentNotFoundException:
                        context.ProblemDetails.Title = "Equipment not found";
                        context.ProblemDetails.Detail = exception.Message;
                        context.ProblemDetails.Status = StatusCodes.Status404NotFound;
                        context.HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;

                        break;
                    
                    case PaymentNotFoundException:
                        context.ProblemDetails.Title = "Payment not found";
                        context.ProblemDetails.Detail = exception.Message;
                        context.ProblemDetails.Status = StatusCodes.Status404NotFound;
                        context.HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;

                        break;
                }

                if (environment.IsDevelopment())
                {
                    context.ProblemDetails.Detail = exception?.ToString();
                }
            }
        );
    }
}