using System.Security.Claims;
using AutoMapper;
using Bogus;
using Moq;
using RentalManager.Core.Domain;
using RentalManager.Core.Repositories;
using RentalManager.Global.Queries;
using RentalManager.Infrastructure.Models.Commands.EquipmentCommands;
using RentalManager.Infrastructure.Models.DTO;
using RentalManager.Infrastructure.Services.EquipmentService;

namespace RentalManager.Tests.ServicesTests;

public class EquipmentServiceTests
{
    [Test]
    public async Task ShouldAdd()
    {
        // Arrange
        var equipment = new Faker<Equipment>().Generate();
        var createEquipment = new Faker<CreateEquipment>().Generate();
        var equipmentDto = new Faker<EquipmentDto>().Generate();

        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(x => x.Map<Equipment>(It.IsAny<CreateEquipment>()))
            .Returns(equipment);
        mapperMock.Setup(x => x.Map<EquipmentDto>(It.IsAny<Equipment>()))
            .Returns(equipmentDto);

        var repositoryMock = new Mock<IEquipmentRepository>();
        repositoryMock.Setup(x => x.AddAsync(It.IsAny<Equipment>()))
            .ReturnsAsync(equipment);

        var service = new EquipmentService(repositoryMock.Object, mapperMock.Object);

        // Act
        var result = await service.AddAsync(createEquipment, GetClaimsPrincipal());

        // Assert
        Assert.Multiple(() => {
            Assert.That(result.Id, Is.EqualTo(equipmentDto.Id));
            Assert.That(result.Name, Is.EqualTo(equipmentDto.Name));
            Assert.That(result.Price, Is.EqualTo(equipmentDto.Price));
        });
        repositoryMock.Verify(x => x.AddAsync(It.IsAny<Equipment>()), Times.Once);
    }

    [Test]
    public async Task ShouldBrowseAll()
    {
        // Arrange
        var numberOfEquipments = 5;

        var equipments = new Faker<Equipment>().Generate(numberOfEquipments);
        var equipmentDtos = new Faker<EquipmentDto>().Generate(numberOfEquipments);
        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(x =>
                x.Map<IEnumerable<EquipmentDto>>(
                    It.Is<IEnumerable<Equipment>>(a => a.Count() == numberOfEquipments)))
            .Returns(equipmentDtos);

        var repositoryMock = new Mock<IEquipmentRepository>();
        repositoryMock.Setup(x => x.BrowseAllAsync(It.IsAny<QueryEquipment>()))
            .ReturnsAsync(equipments);

        var service = new EquipmentService(repositoryMock.Object, mapperMock.Object);

        // Act
        var result = await service.BrowseAllAsync(new QueryEquipment());

        // Assert
        Assert.That(result.Count, Is.EqualTo(numberOfEquipments));
        repositoryMock.Verify(x => x.BrowseAllAsync(It.IsAny<QueryEquipment>()), Times.Once);
    }

    [Test]
    public async Task ShouldDelete()
    {
        // Arrange
        var mapperMock = new Mock<IMapper>();

        var repositoryMock = new Mock<IEquipmentRepository>();

        var service = new EquipmentService(repositoryMock.Object, mapperMock.Object);

        // Act
        await service.DeleteAsync(1);

        // Assert
        repositoryMock.Verify(x => x.DeleteAsync(1), Times.Once);
    }

    [Test]
    public async Task ShouldGet()
    {
        // Arrange
        var equipment = new Faker<Equipment>().Generate();
        var equipmentDto = new Faker<EquipmentDto>().Generate();
        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(x =>
                x.Map<EquipmentDto>(
                    It.IsAny<Equipment>()))
            .Returns(equipmentDto);

        var repositoryMock = new Mock<IEquipmentRepository>();
        repositoryMock.Setup(x => x.GetAsync(It.IsAny<int>()))
            .ReturnsAsync(equipment);

        var service = new EquipmentService(repositoryMock.Object, mapperMock.Object);

        // Act
        var result = await service.GetAsync(1);

        // Assert
        Assert.That(result.Id, Is.EqualTo(equipment.Id));
        repositoryMock.Verify(x => x.GetAsync(1), Times.Once);
    }

    [Test]
    public async Task ShouldUpdate()
    {
        // Arrange
        var equipment = new Faker<Equipment>().Generate();
        var updateEquipment = new Faker<UpdateEquipment>().Generate();
        var equipmentDto = new Faker<EquipmentDto>().Generate();
        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(x => x.Map<Equipment>(It.IsAny<UpdateEquipment>()))
            .Returns(equipment);
        mapperMock.Setup(x => x.Map<EquipmentDto>(It.Is<Equipment>(a => a == equipment)))
            .Returns(equipmentDto);

        var repositoryMock = new Mock<IEquipmentRepository>();
        repositoryMock.Setup(x => x.UpdateAsync(It.IsAny<Equipment>(), It.IsAny<int>()))
            .ReturnsAsync(equipment);

        var service = new EquipmentService(repositoryMock.Object, mapperMock.Object);

        // Act
        var result = await service.UpdateAsync(updateEquipment, 1);

        // Assert
        Assert.Multiple(() => {
            Assert.That(result.Id, Is.EqualTo(equipmentDto.Id));
            Assert.That(result.Name, Is.EqualTo(equipmentDto.Name));
            Assert.That(result.Price, Is.EqualTo(equipmentDto.Price));
        });
        repositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Equipment>(), It.IsAny<int>()),
            Times.Once);
    }

    [Test]
    public async Task ShouldDeactivate()
    {
        // Arrange
        var mapperMock = new Mock<IMapper>();

        var repositoryMock = new Mock<IEquipmentRepository>();

        var service = new EquipmentService(repositoryMock.Object, mapperMock.Object);

        // Act
        await service.Deactivate(1);

        // Assert
        repositoryMock.Verify(x => x.Deactivate(1), Times.Once);
    }

    private static ClaimsPrincipal GetClaimsPrincipal()
    {
        var claims = new List<Claim>
        {
            new("id", "1")
        };
        var identity = new ClaimsIdentity(claims);

        return new ClaimsPrincipal(identity);
    }
}