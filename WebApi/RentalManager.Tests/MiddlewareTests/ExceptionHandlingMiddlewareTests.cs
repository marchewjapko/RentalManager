using Microsoft.AspNetCore.Http;
using RentalManager.Infrastructure.ExceptionHandling;
using RentalManager.Infrastructure.ExceptionHandling.Exceptions;

namespace RentalManager.Tests.MiddlewareTests;

public class ExceptionHandlingMiddlewareTests
{
    [Test]
    public async Task ShouldReturnMappedProblemDetail_AgreementNotFoundException()
    {
        // Arrange
        var expectedException = new AgreementNotFoundException(1);
        var httpContext = new DefaultHttpContext();
        var problem = expectedException.GetProblemDetails(expectedException);
        var exceptionHandlingMiddleware =
            new ExceptionHandlingMiddleware(_ => Task.FromException(expectedException));

        // Act
        await exceptionHandlingMiddleware.InvokeAsync(httpContext);

        // Assert
        Assert.That(httpContext.Response.StatusCode, Is.EqualTo(problem.Status));
    }

    [Test]
    public async Task ShouldReturnMappedProblemDetail_ClientNotFoundException()
    {
        // Arrange
        var expectedException = new ClientNotFoundException(1);
        var httpContext = new DefaultHttpContext();
        var problem = expectedException.GetProblemDetails(expectedException);
        var exceptionHandlingMiddleware =
            new ExceptionHandlingMiddleware(_ => Task.FromException(expectedException));

        // Act
        await exceptionHandlingMiddleware.InvokeAsync(httpContext);

        // Assert
        Assert.That(httpContext.Response.StatusCode, Is.EqualTo(problem.Status));
    }

    [Test]
    public async Task ShouldReturnMappedProblemDetail_EquipmentNotFoundException()
    {
        // Arrange
        var expectedException = new EquipmentNotFoundException(1);
        var httpContext = new DefaultHttpContext();
        var problem = expectedException.GetProblemDetails(expectedException);
        var exceptionHandlingMiddleware =
            new ExceptionHandlingMiddleware(_ => Task.FromException(expectedException));

        // Act
        await exceptionHandlingMiddleware.InvokeAsync(httpContext);

        // Assert
        Assert.That(httpContext.Response.StatusCode, Is.EqualTo(problem.Status));
    }

    [Test]
    public async Task ShouldReturnMappedProblemDetail_PaymentNotFoundException()
    {
        // Arrange
        var expectedException = new PaymentNotFoundException(1);
        var httpContext = new DefaultHttpContext();
        var problem = expectedException.GetProblemDetails(expectedException);
        var exceptionHandlingMiddleware =
            new ExceptionHandlingMiddleware(_ => Task.FromException(expectedException));

        // Act
        await exceptionHandlingMiddleware.InvokeAsync(httpContext);

        // Assert
        Assert.That(httpContext.Response.StatusCode, Is.EqualTo(problem.Status));
    }

    [Test]
    public async Task ShouldReturnMappedProblemDetail_UserNotFoundException()
    {
        // Arrange
        var expectedException = new UserNotFoundException(1);
        var httpContext = new DefaultHttpContext();
        var problem = expectedException.GetProblemDetails(expectedException);
        var exceptionHandlingMiddleware =
            new ExceptionHandlingMiddleware(_ => Task.FromException(expectedException));

        // Act
        await exceptionHandlingMiddleware.InvokeAsync(httpContext);

        // Assert
        Assert.That(httpContext.Response.StatusCode, Is.EqualTo(problem.Status));
    }

    [Test]
    public async Task ShouldReturnMappedProblemDetail_UserDoesNotHaveIdClaimException()
    {
        // Arrange
        var expectedException = new UserDoesNotHaveIdClaimException();
        var httpContext = new DefaultHttpContext();
        var problem = expectedException.GetProblemDetails(expectedException);
        var exceptionHandlingMiddleware =
            new ExceptionHandlingMiddleware(_ => Task.FromException(expectedException));

        // Act
        await exceptionHandlingMiddleware.InvokeAsync(httpContext);

        // Assert
        Assert.That(httpContext.Response.StatusCode, Is.EqualTo(problem.Status));
    }

    [Test]
    public async Task ShouldReturnMappedProblemDetail_UserIdClaimInvalidException()
    {
        // Arrange
        var expectedException = new UserIdClaimInvalidException("");
        var httpContext = new DefaultHttpContext();
        var problem = expectedException.GetProblemDetails(expectedException);
        var exceptionHandlingMiddleware =
            new ExceptionHandlingMiddleware(_ => Task.FromException(expectedException));

        // Act
        await exceptionHandlingMiddleware.InvokeAsync(httpContext);

        // Assert
        Assert.That(httpContext.Response.StatusCode, Is.EqualTo(problem.Status));
    }

    [Test]
    public async Task ShouldReturnStatus500WhenUnmappedException()
    {
        // Arrange
        var expectedException = new Exception();
        RequestDelegate mockNextMiddleware = _ => Task.FromException(expectedException);
        var httpContext = new DefaultHttpContext();

        var exceptionHandlingMiddleware = new ExceptionHandlingMiddleware(mockNextMiddleware);

        // Act
        await exceptionHandlingMiddleware.InvokeAsync(httpContext);

        // Assert
        Assert.That(httpContext.Response.StatusCode,
            Is.EqualTo(StatusCodes.Status500InternalServerError));
    }
}