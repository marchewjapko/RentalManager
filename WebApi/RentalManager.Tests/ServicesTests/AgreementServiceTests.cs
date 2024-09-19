using System.Security.Claims;
using AutoMapper;
using Bogus;
using Moq;
using RentalManager.Core.Domain;
using RentalManager.Core.Repositories;
using RentalManager.Global.Queries;
using RentalManager.Infrastructure.Models.Commands.AgreementCommands;
using RentalManager.Infrastructure.Models.DTO;
using RentalManager.Infrastructure.Services.AgreementService;
using RentalManager.Infrastructure.Services.UserService;

namespace RentalManager.Tests.ServicesTests;

public class AgreementServiceTests
{
    [Test]
    public async Task ShouldAdd()
    {
        // Arrange
        var agreement = new Faker<Agreement>()
            .RuleFor(x => x.Payments, () => new List<Payment> { new Faker<Payment>() })
            .RuleFor(x => x.Client, () => new Faker<Client>())
            .Generate();
        var createAgreement = new Faker<CreateAgreementCommand>();
        var agreementDto = new Faker<AgreementDto>()
            .RuleFor(x => x.Payments, () => new List<PaymentDto> { new Faker<PaymentDto>() })
            .RuleFor(x => x.Client, () => new Faker<ClientDto>())
            .Generate();

        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(x => x.Map<Agreement>(It.IsAny<CreateAgreementCommand>()))
            .Returns(agreement);
        mapperMock.Setup(x => x.Map(It.IsAny<Agreement>(),
                It.IsAny<Action<IMappingOperationOptions<object, AgreementDto>>>()))
            .Returns(agreementDto);

        var userService = new Mock<IUserService>();
        userService.Setup(x => x.GetAsync(It.IsAny<int>()))
            .ReturnsAsync(new Faker<UserDto>().Generate());

        var clientRepository = new Mock<IClientRepository>();
        var equipmentRepository = new Mock<IEquipmentRepository>();

        var agreementRepository = new Mock<IAgreementRepository>();
        agreementRepository.Setup(x => x.AddAsync(It.IsAny<Agreement>()))
            .ReturnsAsync(agreement);

        var service = new AgreementService(
            agreementRepository.Object,
            userService.Object,
            clientRepository.Object,
            equipmentRepository.Object,
            mapperMock.Object);

        // Act
        var result = await service.AddAsync(createAgreement, GetClaimsPrincipal());

        //Assert
        Assert.Multiple(() => {
            Assert.That(result.Id, Is.EqualTo(agreementDto.Id));
            Assert.That(result.IsActive, Is.EqualTo(agreementDto.IsActive));
            Assert.That(result.Client.Id, Is.EqualTo(agreementDto.Client.Id));
            Assert.That(result.Equipments, Is.EqualTo(agreementDto.Equipments));
            Assert.That(result.Payments, Is.EqualTo(agreementDto.Payments));
            Assert.That(result.Comment, Is.EqualTo(agreementDto.Comment));
            Assert.That(result.Deposit, Is.EqualTo(agreementDto.Deposit));
            Assert.That(result.TransportFromPrice, Is.EqualTo(agreementDto.TransportFromPrice));
            Assert.That(result.TransportToPrice, Is.EqualTo(agreementDto.TransportToPrice));
            Assert.That(result.DateAdded, Is.EqualTo(agreementDto.DateAdded));
        });
        agreementRepository.Verify(x => x.AddAsync(It.IsAny<Agreement>()), Times.Once);
    }

    [Test]
    public async Task ShouldBrowseAll()
    {
        // Arrange
        var agreements = new Faker<Agreement>().Generate(5);
        var agreementsDtos = new Faker<AgreementDto>()
            .RuleFor(x => x.User, () => new Faker<UserDto>().Generate())
            .Generate(5);

        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(x => x.Map<IEnumerable<AgreementDto>>(It.IsAny<IEnumerable<Agreement>>()))
            .Returns(agreementsDtos);

        var agreementRepository = new Mock<IAgreementRepository>();
        agreementRepository.Setup(x => x.BrowseAllAsync(It.IsAny<QueryAgreements>()))
            .ReturnsAsync(agreements);

        var userService = new Mock<IUserService>();
        userService.Setup(x => x.GetAsync(It.IsAny<int>()))
            .ReturnsAsync(new Faker<UserDto>().Generate());

        var clientRepository = new Mock<IClientRepository>();
        var equipmentRepository = new Mock<IEquipmentRepository>();
        var service = new AgreementService(
            agreementRepository.Object,
            userService.Object,
            clientRepository.Object,
            equipmentRepository.Object,
            mapperMock.Object);
        // Act
        var result = await service.BrowseAllAsync(new QueryAgreements());
        result = result.ToList();

        // Assert
        Assert.Multiple(() => {
            Assert.That(result.Count, Is.EqualTo(agreements.Count));
            Assert.That(result.Select(x => x.User), Is.Not.Default);
            Assert.That(result.Select(x => x.User), Is.Not.Null);
        });
    }

    [Test]
    public async Task ShouldDelete()
    {
        // Arrange
        var agreementRepository = new Mock<IAgreementRepository>();
        var clientRepository = new Mock<IClientRepository>();
        var equipmentRepository = new Mock<IEquipmentRepository>();
        var userService = new Mock<IUserService>();
        var mapperMock = new Mock<IMapper>();
        var service = new AgreementService(
            agreementRepository.Object,
            userService.Object,
            clientRepository.Object,
            equipmentRepository.Object,
            mapperMock.Object);

        // Act
        await service.DeleteAsync(1);

        // Assert
        agreementRepository.Verify(x => x.DeleteAsync(It.IsAny<int>()), Times.Once);
    }

    [Test]
    public async Task ShouldGet()
    {
        // Arrange
        var agreement = new Faker<Agreement>().Generate();
        var agreementDto = new Faker<AgreementDto>()
            .RuleFor(x => x.User, () => new Faker<UserDto>().Generate())
            .Generate();

        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(x => x.Map(It.IsAny<Agreement>(),
                It.IsAny<Action<IMappingOperationOptions<object, AgreementDto>>>()))
            .Returns(agreementDto);

        var agreementRepository = new Mock<IAgreementRepository>();
        agreementRepository.Setup(x => x.GetAsync(It.IsAny<int>()))
            .ReturnsAsync(agreement);

        var userService = new Mock<IUserService>();
        userService.Setup(x => x.GetAsync(It.IsAny<int>()))
            .ReturnsAsync(new Faker<UserDto>().Generate());

        var clientRepository = new Mock<IClientRepository>();
        var equipmentRepository = new Mock<IEquipmentRepository>();
        var service = new AgreementService(
            agreementRepository.Object,
            userService.Object,
            clientRepository.Object,
            equipmentRepository.Object,
            mapperMock.Object);
        // Act
        var result = await service.GetAsync(1);

        // Assert
        Assert.Multiple(() => {
            Assert.That(result, Is.Not.Null);
            Assert.That(result.User, Is.Not.Default);
            Assert.That(result.User, Is.Not.Null);
        });
    }

    [Test]
    public async Task ShouldUpdate()
    {
        // Arrange
        var agreement = new Faker<Agreement>()
            .RuleFor(x => x.Payments, () => new List<Payment> { new Faker<Payment>() })
            .RuleFor(x => x.Client, () => new Faker<Client>())
            .Generate();
        var updateAgreement = new Faker<UpdateAgreementCommand>();
        var agreementDto = new Faker<AgreementDto>()
            .RuleFor(x => x.Payments, () => new List<PaymentDto> { new Faker<PaymentDto>() })
            .RuleFor(x => x.Client, () => new Faker<ClientDto>())
            .Generate();

        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(x => x.Map<Agreement>(It.IsAny<UpdateAgreementCommand>()))
            .Returns(agreement);
        mapperMock.Setup(x => x.Map(It.IsAny<Agreement>(),
                It.IsAny<Action<IMappingOperationOptions<object, AgreementDto>>>()))
            .Returns(agreementDto);

        var userService = new Mock<IUserService>();
        userService.Setup(x => x.GetAsync(It.IsAny<int>()))
            .ReturnsAsync(new Faker<UserDto>().Generate());

        var clientRepository = new Mock<IClientRepository>();
        var equipmentRepository = new Mock<IEquipmentRepository>();

        var agreementRepository = new Mock<IAgreementRepository>();
        agreementRepository.Setup(x => x.AddAsync(It.IsAny<Agreement>()))
            .ReturnsAsync(agreement);

        var service = new AgreementService(
            agreementRepository.Object,
            userService.Object,
            clientRepository.Object,
            equipmentRepository.Object,
            mapperMock.Object);

        // Act
        var result = await service.UpdateAsync(updateAgreement, 1, GetClaimsPrincipal());

        //Assert
        Assert.Multiple(() => {
            Assert.That(result.Id, Is.EqualTo(agreementDto.Id));
            Assert.That(result.IsActive, Is.EqualTo(agreementDto.IsActive));
            Assert.That(result.Client.Id, Is.EqualTo(agreementDto.Client.Id));
            Assert.That(result.Equipments, Is.EqualTo(agreementDto.Equipments));
            Assert.That(result.Payments, Is.EqualTo(agreementDto.Payments));
            Assert.That(result.Comment, Is.EqualTo(agreementDto.Comment));
            Assert.That(result.Deposit, Is.EqualTo(agreementDto.Deposit));
            Assert.That(result.TransportFromPrice, Is.EqualTo(agreementDto.TransportFromPrice));
            Assert.That(result.TransportToPrice, Is.EqualTo(agreementDto.TransportToPrice));
            Assert.That(result.DateAdded, Is.EqualTo(agreementDto.DateAdded));
        });
        agreementRepository.Verify(x => x.UpdateAsync(It.IsAny<Agreement>(), It.IsAny<int>()),
            Times.Once);
    }

    [Test]
    public async Task ShouldDeactivate()
    {
        // Arrange
        var agreementRepository = new Mock<IAgreementRepository>();
        var clientRepository = new Mock<IClientRepository>();
        var equipmentRepository = new Mock<IEquipmentRepository>();
        var userService = new Mock<IUserService>();
        var mapperMock = new Mock<IMapper>();
        var service = new AgreementService(
            agreementRepository.Object,
            userService.Object,
            clientRepository.Object,
            equipmentRepository.Object,
            mapperMock.Object);

        // Act
        await service.Deactivate(1);

        // Assert
        agreementRepository.Verify(x => x.Deactivate(It.IsAny<int>()), Times.Once);
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