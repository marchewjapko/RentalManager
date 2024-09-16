using System.Security.Claims;
using AutoMapper;
using Bogus;
using Moq;
using RentalManager.Core.Domain;
using RentalManager.Core.Repositories;
using RentalManager.Global.Queries;
using RentalManager.Infrastructure.Models.Commands.ClientCommands;
using RentalManager.Infrastructure.Models.DTO;
using RentalManager.Infrastructure.Services.ClientService;

namespace RentalManager.Tests.ServicesTests;

public class ClientServiceTests
{
    [Test]
    public async Task ShouldAdd()
    {
        // Arrange
        var client = new Faker<Client>().Generate();
        var createClient = new Faker<CreateClient>().Generate();
        var clientDto = new Faker<ClientDto>().Generate();
        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(x => x.Map<Client>(It.IsAny<CreateClient>()))
            .Returns(client);
        mapperMock.Setup(x => x.Map<ClientDto>(It.IsAny<Client>()))
            .Returns(clientDto);

        var repositoryMock = new Mock<IClientRepository>();
        repositoryMock.Setup(x => x.AddAsync(It.IsAny<Client>()))
            .ReturnsAsync(client);

        var service = new ClientService(repositoryMock.Object, mapperMock.Object);

        // Act
        var result = await service.AddAsync(createClient, GetClaimsPrincipal());

        // Assert
        Assert.Multiple(() => {
            Assert.That(result.Id, Is.EqualTo(clientDto.Id));
            Assert.That(result.FirstName, Is.EqualTo(clientDto.FirstName));
            Assert.That(result.LastName, Is.EqualTo(clientDto.LastName));
            Assert.That(result.PhoneNumber, Is.EqualTo(clientDto.PhoneNumber));
            Assert.That(result.Email, Is.EqualTo(clientDto.Email));
            Assert.That(result.IdCard, Is.EqualTo(clientDto.IdCard));
            Assert.That(result.City, Is.EqualTo(clientDto.City));
            Assert.That(result.Street, Is.EqualTo(clientDto.Street));
        });
        repositoryMock.Verify(x => x.AddAsync(It.IsAny<Client>()), Times.Once);
    }

    [Test]
    public async Task ShouldBrowseAll()
    {
        // Arrange
        var numberOfClients = 5;

        var clients = new Faker<Client>().Generate(numberOfClients);
        var clientsDto = new Faker<ClientDto>().Generate(numberOfClients);
        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(x =>
                x.Map<IEnumerable<ClientDto>>(
                    It.Is<IEnumerable<Client>>(a => a.Count() == numberOfClients)))
            .Returns(clientsDto);

        var repositoryMock = new Mock<IClientRepository>();
        repositoryMock.Setup(x => x.BrowseAllAsync(It.IsAny<QueryClients>()))
            .ReturnsAsync(clients);

        var service = new ClientService(repositoryMock.Object, mapperMock.Object);

        // Act
        var result = await service.BrowseAllAsync(new QueryClients());

        // Assert
        Assert.That(result.Count, Is.EqualTo(numberOfClients));
        repositoryMock.Verify(x => x.BrowseAllAsync(It.IsAny<QueryClients>()), Times.Once);
    }

    [Test]
    public async Task ShouldDelete()
    {
        // Arrange
        var mapperMock = new Mock<IMapper>();

        var repositoryMock = new Mock<IClientRepository>();

        var service = new ClientService(repositoryMock.Object, mapperMock.Object);

        // Act
        await service.DeleteAsync(1);

        // Assert
        repositoryMock.Verify(x => x.DeleteAsync(1), Times.Once);
    }

    [Test]
    public async Task ShouldGet()
    {
        // Arrange
        var client = new Faker<Client>().Generate();
        var clientDto = new Faker<ClientDto>().Generate();
        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(x =>
                x.Map<ClientDto>(
                    It.IsAny<Client>()))
            .Returns(clientDto);

        var repositoryMock = new Mock<IClientRepository>();
        repositoryMock.Setup(x => x.GetAsync(It.IsAny<int>()))
            .ReturnsAsync(client);

        var service = new ClientService(repositoryMock.Object, mapperMock.Object);

        // Act
        var result = await service.GetAsync(1);

        // Assert
        Assert.That(result.Id, Is.EqualTo(client.Id));
        repositoryMock.Verify(x => x.GetAsync(1), Times.Once);
    }

    [Test]
    public async Task ShouldUpdate()
    {
        // Arrange
        var client = new Faker<Client>().Generate();
        var updateClient = new Faker<UpdateClient>().Generate();
        var clientDto = new Faker<ClientDto>().Generate();
        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(x => x.Map<Client>(It.IsAny<UpdateClient>()))
            .Returns(client);
        mapperMock.Setup(x => x.Map<ClientDto>(It.Is<Client>(a => a == client)))
            .Returns(clientDto);

        var repositoryMock = new Mock<IClientRepository>();
        repositoryMock.Setup(x => x.UpdateAsync(It.IsAny<Client>(), It.IsAny<int>()))
            .ReturnsAsync(client);

        var service = new ClientService(repositoryMock.Object, mapperMock.Object);

        // Act
        var result = await service.UpdateAsync(updateClient, 1);

        // Assert
        Assert.Multiple(() => {
            Assert.That(result.Id, Is.EqualTo(clientDto.Id));
            Assert.That(result.FirstName, Is.EqualTo(clientDto.FirstName));
            Assert.That(result.LastName, Is.EqualTo(clientDto.LastName));
            Assert.That(result.PhoneNumber, Is.EqualTo(clientDto.PhoneNumber));
            Assert.That(result.Email, Is.EqualTo(clientDto.Email));
            Assert.That(result.IdCard, Is.EqualTo(clientDto.IdCard));
            Assert.That(result.City, Is.EqualTo(clientDto.City));
            Assert.That(result.Street, Is.EqualTo(clientDto.Street));
        });
        repositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Client>(), It.IsAny<int>()), Times.Once);
    }

    [Test]
    public async Task ShouldDeactivate()
    {
        // Arrange
        var mapperMock = new Mock<IMapper>();

        var repositoryMock = new Mock<IClientRepository>();

        var service = new ClientService(repositoryMock.Object, mapperMock.Object);

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