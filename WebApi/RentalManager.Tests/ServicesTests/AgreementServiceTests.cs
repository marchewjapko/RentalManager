using System.Security.Claims;
using AutoMapper;
using Bogus;
using Moq;
using RentalManager.Core.Domain;
using RentalManager.Core.Repositories;
using RentalManager.Infrastructure.Models.Commands.ClientCommands;
using RentalManager.Infrastructure.Models.Commands.EquipmentCommands;
using RentalManager.Infrastructure.Services.AgreementService;
using RentalManager.Infrastructure.Services.UserService;

namespace RentalManager.Tests.ServicesTests;

public class AgreementServiceTests
{
    [Test]
    public async Task ShouldFillClient()
    {
        // Arrange
        var mapperMock = new Mock<IMapper>();
        var userService = new Mock<IUserService>();
        var clientRepository = new Mock<IClientRepository>();
        var equipmentRepository = new Mock<IEquipmentRepository>();
        var agreementRepository = new Mock<IAgreementRepository>();

        var clientId = new Faker().Random.Int();

        var createOrGetClient = new Faker<CreateOrGetClient>()
            .RuleFor(x => x.Id, () => clientId)
            .Generate();

        var existingClient = new Faker<Client>()
            .RuleFor(x => x.Id, () => clientId)
            .Generate();

        clientRepository.Setup(x => x.GetAsync(It.IsAny<int>()))
            .ReturnsAsync(existingClient);

        var agreement = new Faker<Agreement>().Generate();

        var service = new AgreementService(
            agreementRepository.Object,
            userService.Object,
            clientRepository.Object,
            equipmentRepository.Object,
            mapperMock.Object);

        // Act
        await service.FillClient(createOrGetClient, agreement);

        // Assert
        Assert.That(agreement.ClientId, Is.EqualTo(createOrGetClient.Id));
    }

    [Test]
    public async Task ShouldFillEquipment()
    {
        // Arrange
        var userService = new Mock<IUserService>();
        var clientRepository = new Mock<IClientRepository>();
        var agreementRepository = new Mock<IAgreementRepository>();

        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(x => x.Map<Equipment>(It.IsAny<CreateOrGetEquipment>()))
            .Returns((CreateOrGetEquipment equipment) => new Faker<Equipment>()
                .RuleFor(x => x.Id, () => equipment.Id ?? 0)
                .Generate());

        var createOrGetEquipments = new Faker<CreateOrGetEquipment>()
            .RuleFor(x => x.Id, f => f.UniqueIndex + 1)
            .Generate(3);

        createOrGetEquipments.AddRange(new Faker<CreateOrGetEquipment>()
            .RuleFor(x => x.Id, () => null)
            .Generate(2));

        var existingEquipments = new Faker<Equipment>()
            .RuleFor(x => x.Id, f => f.UniqueIndex + 1 - createOrGetEquipments.Count)
            .Generate(3);

        var equipmentRepository = new Mock<IEquipmentRepository>();
        equipmentRepository.Setup(x => x.GetAsync(It.IsAny<List<int>>()))
            .ReturnsAsync(existingEquipments);

        var agreement = new Faker<Agreement>()
            .RuleFor(x => x.Equipments, () => new List<Equipment>())
            .Generate();

        var service = new AgreementService(
            agreementRepository.Object,
            userService.Object,
            clientRepository.Object,
            equipmentRepository.Object,
            mapperMock.Object);

        // Act
        await service.FillEquipments(createOrGetEquipments, agreement);

        // Assert
        Assert.Multiple(() => {
            Assert.That(agreement.Equipments, Has.Count.EqualTo(5));
            Assert.That(agreement.Equipments.Count(x => x.Id != 0), Is.EqualTo(3));
            Assert.That(agreement.Equipments.Count(x => x.Id == 0), Is.EqualTo(2));
        });
    }

    // [Test]
    // public async Task ShouldAdd()
    // {
    //     // Arrange
    //     var agreement = new Faker<Agreement>().Generate();
    //     var createAgreement = new Faker<CreateAgreement>().Generate();
    //     var agreementDto = new Faker<AgreementDto>().Generate();
    //     var mapperMock = new Mock<IMapper>();
    //     mapperMock.Setup(x => x.Map<Agreement>(It.IsAny<CreateAgreement>()))
    //         .Returns(agreement);
    //     mapperMock.Setup(x => x.Map<AgreementDto>(It.IsAny<Agreement>()))
    //         .Returns(agreementDto);
    //
    //     var userService = new Mock<IUserService>();
    //     userService.Setup(x => x.GetAsync(It.IsAny<int>()))
    //         .ReturnsAsync(new Faker<UserDto>().Generate());
    //
    //     var clientRepository = new Mock<IClientRepository>();
    //     var equipmentRepository = new Mock<IEquipmentRepository>();
    //
    //     var agreementRepository = new Mock<IAgreementRepository>();
    //     agreementRepository.Setup(x => x.AddAsync(It.IsAny<Agreement>()))
    //         .ReturnsAsync(agreement);
    //
    //     var service = new AgreementService(
    //         agreementRepository.Object,
    //         userService.Object,
    //         clientRepository.Object,
    //         equipmentRepository.Object,
    //         mapperMock.Object);
    //
    //     // Act
    //     var result = await service.AddAsync(createAgreement, GetClaimsPrincipal());
    //
    //     //Assert
    //     Assert.Multiple(() => {
    //         Assert.That(result.Id, Is.EqualTo(agreementDto.Id));
    //         Assert.That(result.FirstName, Is.EqualTo(agreementDto.FirstName));
    //         Assert.That(result.LastName, Is.EqualTo(agreementDto.LastName));
    //         Assert.That(result.PhoneNumber, Is.EqualTo(agreementDto.PhoneNumber));
    //         Assert.That(result.Email, Is.EqualTo(agreementDto.Email));
    //         Assert.That(result.IdCard, Is.EqualTo(agreementDto.IdCard));
    //         Assert.That(result.City, Is.EqualTo(agreementDto.City));
    //         Assert.That(result.Street, Is.EqualTo(agreementDto.Street));
    //     });
    //     agreementRepository.Verify(x => x.AddAsync(It.IsAny<Agreement>()), Times.Once);
    // }

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