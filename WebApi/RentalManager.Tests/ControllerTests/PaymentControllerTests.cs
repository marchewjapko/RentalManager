using System.Security.Claims;
using Bogus;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RentalManager.Global.Queries;
using RentalManager.Infrastructure.Models.Commands.PaymentCommands;
using RentalManager.Infrastructure.Models.DTO;
using RentalManager.Infrastructure.Services.PaymentService;
using RentalManager.WebAPI.Controllers;

namespace RentalManager.Tests.ControllerTests;

public class PaymentControllerTests
{
    [Test]
    public async Task AddPayment_ShouldAddPayment()
    {
        // Arrange
        var paymentService = new Mock<IPaymentService>();
        paymentService.Setup(x => x.AddAsync(It.IsAny<CreatePaymentCommand>(), It.IsAny<ClaimsPrincipal>()))
            .ReturnsAsync(new Faker<PaymentDto>().Generate());

        var controller = new PaymentController(paymentService.Object);

        var command = new Faker<CreatePaymentCommand>().Generate();
        
        // Act
        var result = await controller.AddPayment(command) as OkObjectResult;

        // Assert
        Assert.Multiple(() => {
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.StatusCode, Is.EqualTo(200));
            Assert.That(result.Value, Is.Not.Null);
            Assert.That(result.Value, Is.TypeOf<PaymentDto>());
        });
    }

    [Test]
    public async Task BrowseAllPayments_ShouldReturnAllPayments()
    {
        // Arrange
        var paymentService = new Mock<IPaymentService>();
        paymentService.Setup(x => x.BrowseAllAsync(It.IsAny<QueryPayment>()))
            .ReturnsAsync(new Faker<PaymentDto>().Generate(5));

        var controller = new PaymentController(paymentService.Object);

        // Act
        var result = await controller.BrowseAllPayments(new QueryPayment()) as OkObjectResult;

        // Assert
        Assert.Multiple(() => {
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.StatusCode, Is.EqualTo(200));
            Assert.That(result.Value, Is.Not.Null);
            Assert.That(result.Value, Is.TypeOf<List<PaymentDto>>());
        });
    }

    [Test]
    public async Task DeletePayment_ShouldDeletePayment()
    {
        // Arrange
        var paymentService = new Mock<IPaymentService>();

        var controller = new PaymentController(paymentService.Object);

        // Act
        var result = await controller.DeletePayment(1) as NoContentResult;

        // Assert
        Assert.Multiple(() => {
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.StatusCode, Is.EqualTo(204));
        });
    }

    [Test]
    public async Task GetPayment_ShouldReturnPayment()
    {
        // Arrange
        var paymentService = new Mock<IPaymentService>();
        paymentService.Setup(x => x.GetAsync(It.IsAny<int>()))
            .ReturnsAsync(new Faker<PaymentDto>().Generate());

        var controller = new PaymentController(paymentService.Object);

        // Act
        var result = await controller.GetPayment(1) as OkObjectResult;

        // Assert
        Assert.Multiple(() => {
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.StatusCode, Is.EqualTo(200));
            Assert.That(result.Value, Is.Not.Null);
            Assert.That(result.Value, Is.TypeOf<PaymentDto>());
        });
    }

    [Test]
    public async Task UpdatePayment_ShouldUpdatePayment()
    {
        // Arrange
        var paymentService = new Mock<IPaymentService>();
        paymentService.Setup(x => x.UpdateAsync(It.IsAny<UpdatePaymentCommand>(), It.IsAny<int>()))
            .ReturnsAsync(new Faker<PaymentDto>().Generate());

        var controller = new PaymentController(paymentService.Object);

        var command = new Faker<UpdatePaymentCommand>().Generate();
        
        // Act
        var result = await controller.UpdatePayment(command, 1) as OkObjectResult;

        // Assert
        Assert.Multiple(() => {
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.StatusCode, Is.EqualTo(200));
            Assert.That(result.Value, Is.Not.Null);
            Assert.That(result.Value, Is.TypeOf<PaymentDto>());
        });
    }

    [Test]
    public async Task DeactivatePayment_ShouldDeactivatePayment()
    {
        // Arrange
        var paymentService = new Mock<IPaymentService>();

        var controller = new PaymentController(paymentService.Object);

        // Act
        var result = await controller.DeactivatePayment(1) as NoContentResult;

        // Assert
        Assert.Multiple(() => {
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.StatusCode, Is.EqualTo(204));
        });
    }
}