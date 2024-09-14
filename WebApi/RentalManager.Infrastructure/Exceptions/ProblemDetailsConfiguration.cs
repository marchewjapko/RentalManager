using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace RentalManager.Infrastructure.Exceptions;

public static class ProblemDetailsConfiguration
{
    // ReSharper disable once CognitiveComplexity
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
                    case UserNotFoundException:
                        context.ProblemDetails.Title = "User not found";
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

                    case ValidationException:
                        context.ProblemDetails.Title = "Validation error";
                        context.ProblemDetails.Detail = exception.Message;
                        context.ProblemDetails.Status = StatusCodes.Status400BadRequest;
                        context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

                        break;

                    case ConfigurationNotFoundException:
                        context.ProblemDetails.Title = "Configuration not found";
                        context.ProblemDetails.Detail = exception.Message;
                        context.ProblemDetails.Status = StatusCodes.Status404NotFound;
                        context.HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;

                        break;

                    case PasswordChangeRequiredException:
                        context.ProblemDetails.Title = "Password change is required";
                        context.ProblemDetails.Detail = exception.Message;
                        context.ProblemDetails.Status = StatusCodes.Status401Unauthorized;
                        context.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;

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