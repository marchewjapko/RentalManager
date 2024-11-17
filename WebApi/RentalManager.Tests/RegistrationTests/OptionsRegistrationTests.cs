using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using RentalManager.Infrastructure.Options;

namespace RentalManager.Tests.RegistrationTests;

public class OptionsRegistrationTests
{
    [Test]
    public void ShouldRegisterProfileServices()
    {
        // Arrange
        IServiceCollection services = new ServiceCollection();

        var optionTypes = GetAllOptionTypes();

        // Act
        services.RegisterOptions();
        var handledGenerics = services.SelectMany(x => x.ServiceType.GenericTypeArguments).Distinct();
        
        // Assert
        Assert.That(optionTypes.All(x => handledGenerics.Contains(x)), Is.True);
    }
    
    private static List<Type> GetAllOptionTypes()
    {
        var assembly = Assembly.Load("RentalManager.Infrastructure");
        var types = assembly.GetTypes();

        return types
            .Where(t => t.Namespace != null && t.Namespace.StartsWith("RentalManager.Infrastructure.Options") && !t.IsAbstract)
            .Distinct()
            .ToList();
    }
}