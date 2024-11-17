using System.Security.Claims;
using Bogus;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RentalManager.Global.Queries;
using RentalManager.Infrastructure.Models.Commands.ClientCommands;
using RentalManager.Infrastructure.Models.DTO;
using RentalManager.Infrastructure.Services.ClientService;
using RentalManager.WebAPI.Controllers;

namespace RentalManager.Tests.ControllerTests;

public class ClientControllerTests
{
    [Test]
    public async Task BrowseAllClients_ShouldReturnAllEquipment()
    {
        // Arrange
        var clientService = new Mock<IClientService>();
        clientService.Setup(x => x.AddAsync(It.IsAny<CreateClientCommand>(), It.IsAny<ClaimsPrincipal>()))
            .ReturnsAsync(new Faker<ClientDto>().Generate());

        var controller = new ClientController(clientService.Object);

        var command = new Faker<CreateClientCommand>().Generate();

        // Act
        var result = await controller.AddClient(command) as OkObjectResult;

        // Assert
        Assert.Multiple(() => {
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.StatusCode, Is.EqualTo(200));
            Assert.That(result.Value, Is.Not.Null);
            Assert.That(result.Value, Is.TypeOf<ClientDto>());
        });
    }

    [Test]
    public async Task BrowseAllClients_ShouldReturnAllClients()
    {
        // Arrange
        var clientService = new Mock<IClientService>();
        clientService.Setup(x => x.BrowseAllAsync(It.IsAny<QueryClients>()))
            .ReturnsAsync(new Faker<ClientDto>().Generate(5));

        var controller = new ClientController(clientService.Object);

        // Act
        var result = await controller.BrowseAllClients(new QueryClients()) as OkObjectResult;

        // Assert
        Assert.Multiple(() => {
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.StatusCode, Is.EqualTo(200));
            Assert.That(result.Value, Is.Not.Null);
            Assert.That(result.Value, Is.TypeOf<List<ClientDto>>());
        });
    }

    [Test]
    public async Task DeleteClient_ShouldDeleteClient()
    {
        // Arrange
        var clientService = new Mock<IClientService>();

        var controller = new ClientController(clientService.Object);

        // Act
        var result = await controller.DeleteClient(1) as NoContentResult;

        // Assert
        Assert.Multiple(() => {
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.StatusCode, Is.EqualTo(204));
        });
    }

    [Test]
    public async Task GetClient_ShouldReturnClient()
    {
        // Arrange
        var clientService = new Mock<IClientService>();
        clientService.Setup(x => x.GetAsync(It.IsAny<int>()))
            .ReturnsAsync(new Faker<ClientDto>().Generate());

        var controller = new ClientController(clientService.Object);

        // Act
        var result = await controller.GetClient(1) as OkObjectResult;

        // Assert
        Assert.Multiple(() => {
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.StatusCode, Is.EqualTo(200));
            Assert.That(result.Value, Is.Not.Null);
            Assert.That(result.Value, Is.TypeOf<ClientDto>());
        });
    }

    [Test]
    public async Task UpdateClient_ShouldUpdateClient()
    {
        // Arrange
        var clientService = new Mock<IClientService>();
        clientService.Setup(x => x.UpdateAsync(It.IsAny<UpdateClientCommand>(), It.IsAny<int>()))
            .ReturnsAsync(new Faker<ClientDto>().Generate());

        var controller = new ClientController(clientService.Object);

        var command = new Faker<UpdateClientCommand>().Generate();
        
        // Act
        var result = await controller.UpdateClient(command, 1) as OkObjectResult;

        // Assert
        Assert.Multiple(() => {
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.StatusCode, Is.EqualTo(200));
            Assert.That(result.Value, Is.Not.Null);
            Assert.That(result.Value, Is.TypeOf<ClientDto>());
        });
    }

    [Test]
    public async Task DeactivateClient_ShouldDeactivateClient()
    {
        // Arrange
        var clientService = new Mock<IClientService>();

        var controller = new ClientController(clientService.Object);

        // Act
        var result = await controller.DeactivateClient(1) as NoContentResult;

        // Assert
        Assert.Multiple(() => {
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.StatusCode, Is.EqualTo(204));
        });
    }
}