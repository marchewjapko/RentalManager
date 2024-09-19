using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using RentalManager.WebAPI;
using Swashbuckle.AspNetCore.Swagger;

namespace RentalManager.Tests.RegistrationTests;

public class SwaggerRegistrationTests
{
    [Test]
    public void ShouldRegisterSwagger()
    {
        // Arrange       
        var builder = WebApplication.CreateBuilder();
        builder.Configuration["OpenIdProvider:Configuration"] = "https://example.com/openid-configuration";

        builder.Services.AddControllers()
            .AddJsonOptions(options => { options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); });

        // Act
        builder.RegisterSwagger();

        // Assert
        var serviceProvider = builder.Services.BuildServiceProvider();
        var swaggerGen = serviceProvider.GetService<ISwaggerProvider>();

        var swaggerDoc = swaggerGen.GetSwagger("v1");
        Assert.Multiple(() => {
            Assert.That(swaggerDoc, Is.Not.Null);
            Assert.That(swaggerDoc.Info.Title, Is.EqualTo("Rental Manager API"));
            Assert.That(swaggerDoc.Info.Version, Is.EqualTo("v1"));
        });
    }
}