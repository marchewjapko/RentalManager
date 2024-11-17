using System.Security.Claims;
using Bogus;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RentalManager.Global.Queries;
using RentalManager.Infrastructure.Models.Commands.EquipmentCommands;
using RentalManager.Infrastructure.Models.DTO;
using RentalManager.Infrastructure.Services.EquipmentService;
using RentalManager.WebAPI.Controllers;

namespace RentalManager.Tests.ControllerTests;

public class EquipmentControllerTests
{
    [Test]
    public async Task AddEquipment_ShouldAddEquipment()
    {
        // Arrange
        var equipmentService = new Mock<IEquipmentService>();
        equipmentService.Setup(x => x.AddAsync(It.IsAny<CreateEquipmentCommand>(), It.IsAny<ClaimsPrincipal>()))
            .ReturnsAsync(new Faker<EquipmentDto>().Generate());

        var controller = new EquipmentController(equipmentService.Object);

        var command = new Faker<CreateEquipmentCommand>().Generate();
        
        // Act
        var result = await controller.AddEquipment(command) as OkObjectResult;

        // Assert
        Assert.Multiple(() => {
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.StatusCode, Is.EqualTo(200));
            Assert.That(result.Value, Is.Not.Null);
            Assert.That(result.Value, Is.TypeOf<EquipmentDto>());
        });
    }

    [Test]
    public async Task BrowseAllEquipment_ShouldReturnAllEquipment()
    {
        // Arrange
        var equipmentService = new Mock<IEquipmentService>();
        equipmentService.Setup(x => x.BrowseAllAsync(It.IsAny<QueryEquipment>()))
            .ReturnsAsync(new Faker<EquipmentDto>().Generate(5));

        var controller = new EquipmentController(equipmentService.Object);

        // Act
        var result = await controller.BrowseAllEquipment(new QueryEquipment()) as OkObjectResult;

        // Assert
        Assert.Multiple(() => {
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.StatusCode, Is.EqualTo(200));
            Assert.That(result.Value, Is.Not.Null);
            Assert.That(result.Value, Is.TypeOf<List<EquipmentDto>>());
        });
    }

    [Test]
    public async Task DeleteEquipment_ShouldDeleteEquipment()
    {
        // Arrange
        var equipmentService = new Mock<IEquipmentService>();

        var controller = new EquipmentController(equipmentService.Object);

        // Act
        var result = await controller.DeleteEquipment(1) as NoContentResult;

        // Assert
        Assert.Multiple(() => {
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.StatusCode, Is.EqualTo(204));
        });
    }

    [Test]
    public async Task GetEquipment_ShouldReturnEquipment()
    {
        // Arrange
        var equipmentService = new Mock<IEquipmentService>();
        equipmentService.Setup(x => x.GetAsync(It.IsAny<int>()))
            .ReturnsAsync(new Faker<EquipmentDto>().Generate());

        var controller = new EquipmentController(equipmentService.Object);

        // Act
        var result = await controller.GetEquipment(1) as OkObjectResult;

        // Assert
        Assert.Multiple(() => {
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.StatusCode, Is.EqualTo(200));
            Assert.That(result.Value, Is.Not.Null);
            Assert.That(result.Value, Is.TypeOf<EquipmentDto>());
        });
    }

    [Test]
    public async Task UpdateEquipment_ShouldUpdateEquipment()
    {
        // Arrange
        var equipmentService = new Mock<IEquipmentService>();
        equipmentService.Setup(x => x.UpdateAsync(It.IsAny<UpdateEquipmentCommand>(), It.IsAny<int>()))
            .ReturnsAsync(new Faker<EquipmentDto>().Generate());

        var controller = new EquipmentController(equipmentService.Object);

        var command = new Faker<UpdateEquipmentCommand>().Generate();
        
        // Act
        var result = await controller.UpdateEquipment(command, 1) as OkObjectResult;

        // Assert
        Assert.Multiple(() => {
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.StatusCode, Is.EqualTo(200));
            Assert.That(result.Value, Is.Not.Null);
            Assert.That(result.Value, Is.TypeOf<EquipmentDto>());
        });
    }

    [Test]
    public async Task DeactivateEquipment_ShouldDeactivateEquipment()
    {
        // Arrange
        var equipmentService = new Mock<IEquipmentService>();

        var controller = new EquipmentController(equipmentService.Object);

        // Act
        var result = await controller.DeactivateEquipment(1) as NoContentResult;

        // Assert
        Assert.Multiple(() => {
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.StatusCode, Is.EqualTo(204));
        });
    }
}