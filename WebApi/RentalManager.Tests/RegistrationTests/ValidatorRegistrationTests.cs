using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using RentalManager.Infrastructure.Services;

namespace RentalManager.Tests.RegistrationTests;

public class ValidatorRegistrationTests
{
    [Test]
    public void ShouldRegisterValidatorServices()
    {
        // Arrange
        IServiceCollection services = new ServiceCollection();

        var commandTypes = GetAllCommandTypes();

        // Act
        services.RegisterValidatorServices();
        var handledGenerics = services.SelectMany(x => x.ServiceType.GenericTypeArguments);

        // Assert
        Assert.That(commandTypes.Where(x => !x.Name.Contains("Base"))
            .All(x => handledGenerics.Contains(x)), Is.True);
    }

    private static List<Type> GetAllCommandTypes()
    {
        var assembly = Assembly.Load("RentalManager.Infrastructure");
        var types = assembly.GetTypes();

        return types
            .Where(t => t.Namespace != null && t.Namespace.StartsWith("RentalManager.Infrastructure.Models.Commands"))
            .Distinct()
            .ToList();
    }
}