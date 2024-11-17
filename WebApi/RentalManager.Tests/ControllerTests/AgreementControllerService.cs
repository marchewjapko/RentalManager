using System.Security.Claims;
using Bogus;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RentalManager.Global.Queries;
using RentalManager.Infrastructure.Models.Commands.AgreementCommands;
using RentalManager.Infrastructure.Models.DTO;
using RentalManager.Infrastructure.Services.AgreementService;
using RentalManager.WebAPI.Controllers;

namespace RentalManager.Tests.ControllerTests;

public class AgreementControllerService
{
    [Test]
    public async Task AddAgreement_ShouldAddAgreement()
    {
        // Arrange
        var agreementService = new Mock<IAgreementService>();
        agreementService.Setup(x => x.AddAsync(It.IsAny<CreateAgreementCommand>(), It.IsAny<ClaimsPrincipal>()))
            .ReturnsAsync(new Faker<AgreementDto>().Generate());

        var controller = new AgreementController(agreementService.Object);
        
        var agreement = new Faker<CreateAgreementCommand>().Generate();

        // Act
        var result = await controller.AddAgreement(agreement) as OkObjectResult;

        // Assert
        Assert.Multiple(() => {
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.StatusCode, Is.EqualTo(200));
            Assert.That(result.Value, Is.Not.Null);
            Assert.That(result.Value, Is.TypeOf<AgreementDto>());
        });
    }
    
    [Test]
    public async Task BrowseAllAgreements_ShouldReturnAllAgreements()
    {
        // Arrange
        var agreementService = new Mock<IAgreementService>();
        agreementService.Setup(x => x.BrowseAllAsync(It.IsAny<QueryAgreements>()))
            .ReturnsAsync(new Faker<AgreementDto>().Generate(5));

        var controller = new AgreementController(agreementService.Object);

        // Act
        var result = await controller.BrowseAllAgreements(new QueryAgreements()) as OkObjectResult;

        // Assert
        Assert.Multiple(() => {
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.StatusCode, Is.EqualTo(200));
            Assert.That(result.Value, Is.Not.Null);
            Assert.That(result.Value, Is.TypeOf<List<AgreementDto>>());
        });
    }

    [Test]
    public async Task DeleteAgreement_ShouldDeletePayment()
    {
        // Arrange
        var agreementService = new Mock<IAgreementService>();

        var controller = new AgreementController(agreementService.Object);

        // Act
        var result = await controller.DeleteAgreement(1) as NoContentResult;

        // Assert
        Assert.Multiple(() => {
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.StatusCode, Is.EqualTo(204));
        });
    }
    
    [Test]
    public async Task GetAgreement_ShouldReturnAgreement()
    {
        // Arrange
        var agreementService = new Mock<IAgreementService>();
        agreementService.Setup(x => x.GetAsync(It.IsAny<int>()))
            .ReturnsAsync(new Faker<AgreementDto>().Generate());

        var controller = new AgreementController(agreementService.Object);

        // Act
        var result = await controller.GetAgreement(1) as OkObjectResult;

        // Assert
        Assert.Multiple(() => {
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.StatusCode, Is.EqualTo(200));
            Assert.That(result.Value, Is.Not.Null);
            Assert.That(result.Value, Is.TypeOf<AgreementDto>());
        });
    }
    
    [Test]
    public async Task UpdateAgreement_ShouldUpdateAgreement()
    {
        // Arrange
        var agreementService = new Mock<IAgreementService>();
        agreementService.Setup(x => x.UpdateAsync(It.IsAny<UpdateAgreementCommand>(), It.IsAny<int>(), It.IsAny<ClaimsPrincipal>()))
            .ReturnsAsync(new Faker<AgreementDto>().Generate());

        var controller = new AgreementController(agreementService.Object);

        var agreement = new Faker<UpdateAgreementCommand>().Generate();
        
        // Act
        var result = await controller.UpdateAgreement(agreement, 1) as OkObjectResult;

        // Assert
        Assert.Multiple(() => {
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.StatusCode, Is.EqualTo(200));
            Assert.That(result.Value, Is.Not.Null);
            Assert.That(result.Value, Is.TypeOf<AgreementDto>());
        });
    }
    
    [Test]
    public async Task DeactivateAgreement_ShouldDeactivateAgreement()
    {
        // Arrange
        var agreementService = new Mock<IAgreementService>();

        var controller = new AgreementController(agreementService.Object);

        // Act
        var result = await controller.DeactivateAgreement(1) as NoContentResult;

        // Assert
        Assert.Multiple(() => {
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.StatusCode, Is.EqualTo(204));
        });
    }
}