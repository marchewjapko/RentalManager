using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using RentalManager.Infrastructure.Services;

namespace RentalManager.Tests.RegistrationTests;

public class ServiceRegistrationTests
{
    [Test]
    public void ShouldRegisterApiServices()
    {
        // Arrange
        IServiceCollection services = new ServiceCollection();

        var numberOfExpectedInterfaces = 0;
        var servicesLocations = new List<(string, string)>
        {
            ("RentalManager.Core", "RentalManager.Core.Repositories"),
            ("RentalManager.Infrastructure", "RentalManager.Infrastructure.Services")
        };
        foreach (var location in servicesLocations)
        {
            var assembly = Assembly.Load(location.Item1);
            var types = assembly.GetTypes();

            var memberNamespaces = types
                .Where(t => t.Namespace != null && t.Namespace.StartsWith(location.Item2))
                .Select(t => t.Namespace)
                .Distinct();

            numberOfExpectedInterfaces += types.Count(type => type.IsInterface && memberNamespaces.Contains(type.Namespace));
        }

        // Act
        services.RegisterApiServices();

        // Assert
        Assert.That(services, Has.Count.EqualTo(numberOfExpectedInterfaces));
    }
}