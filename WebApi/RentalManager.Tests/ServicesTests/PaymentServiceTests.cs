using System.Security.Claims;
using AutoMapper;
using Bogus;
using Moq;
using RentalManager.Core.Domain;
using RentalManager.Core.Repositories;
using RentalManager.Global.Queries;
using RentalManager.Infrastructure.Models.Commands.PaymentCommands;
using RentalManager.Infrastructure.Models.DTO;
using RentalManager.Infrastructure.Services.PaymentService;

namespace RentalManager.Tests.ServicesTests;

public class PaymentServiceTests
{
    [Test]
    public async Task ShouldAdd()
    {
        // Arrange
        var payment = new Faker<Payment>().Generate();
        var createPayment = new Faker<CreatePaymentCommand>()
            .RuleFor(x => x.AgreementId, f => f.Random.Int())
            .Generate();
        var paymentDto = new Faker<PaymentDto>().Generate();
        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(x => x.Map<Payment>(It.IsAny<CreatePaymentCommand>()))
            .Returns(payment);
        mapperMock.Setup(x => x.Map<PaymentDto>(It.Is<Payment>(a => a == payment)))
            .Returns(paymentDto);

        var repositoryMock = new Mock<IPaymentRepository>();
        repositoryMock.Setup(x => x.AddAsync(It.IsAny<Payment>()))
            .ReturnsAsync(payment);

        var service = new PaymentService(repositoryMock.Object, mapperMock.Object);

        // Act
        var result = await service.AddAsync(createPayment, GetClaimsPrincipal());

        // Assert
        Assert.Multiple(() => {
            Assert.That(result.Id, Is.EqualTo(paymentDto.Id));
            Assert.That(result.Method, Is.EqualTo(paymentDto.Method));
            Assert.That(result.Amount, Is.EqualTo(paymentDto.Amount));
            Assert.That(result.DateFrom, Is.EqualTo(paymentDto.DateFrom));
            Assert.That(result.DateTo, Is.EqualTo(paymentDto.DateTo));
        });
        repositoryMock.Verify(x => x.AddAsync(It.IsAny<Payment>()), Times.Once);
    }

    [Test]
    public async Task ShouldBrowseAll()
    {
        // Arrange
        var numberOfPayments = 5;

        var payments = new Faker<Payment>().Generate(numberOfPayments);
        var paymentDto = new Faker<PaymentDto>().Generate(numberOfPayments);
        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(x =>
                x.Map<IEnumerable<PaymentDto>>(
                    It.Is<IEnumerable<Payment>>(a => a.Count() == numberOfPayments)))
            .Returns(paymentDto);

        var repositoryMock = new Mock<IPaymentRepository>();
        repositoryMock.Setup(x => x.BrowseAllAsync(It.IsAny<QueryPayment>()))
            .ReturnsAsync(payments);

        var service = new PaymentService(repositoryMock.Object, mapperMock.Object);

        // Act
        var result = await service.BrowseAllAsync(new QueryPayment());

        // Assert
        Assert.That(result.Count, Is.EqualTo(numberOfPayments));
        repositoryMock.Verify(x => x.BrowseAllAsync(It.IsAny<QueryPayment>()), Times.Once);
    }

    [Test]
    public async Task ShouldDelete()
    {
        // Arrange
        var mapperMock = new Mock<IMapper>();

        var repositoryMock = new Mock<IPaymentRepository>();

        var service = new PaymentService(repositoryMock.Object, mapperMock.Object);

        // Act
        await service.DeleteAsync(1);

        // Assert
        repositoryMock.Verify(x => x.DeleteAsync(1), Times.Once);
    }

    [Test]
    public async Task ShouldGet()
    {
        // Arrange
        var payment = new Faker<Payment>().Generate();
        var paymentDto = new Faker<PaymentDto>().Generate();
        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(x =>
                x.Map<PaymentDto>(
                    It.IsAny<Payment>()))
            .Returns(paymentDto);

        var repositoryMock = new Mock<IPaymentRepository>();
        repositoryMock.Setup(x => x.GetAsync(It.IsAny<int>()))
            .ReturnsAsync(payment);

        var service = new PaymentService(repositoryMock.Object, mapperMock.Object);

        // Act
        var result = await service.GetAsync(1);

        // Assert
        Assert.That(result.Id, Is.EqualTo(payment.Id));
        repositoryMock.Verify(x => x.GetAsync(1), Times.Once);
    }

    [Test]
    public async Task ShouldUpdate()
    {
        // Arrange
        var payment = new Faker<Payment>().Generate();
        var updatePayment = new Faker<UpdatePaymentCommand>().Generate();
        var paymentDto = new Faker<PaymentDto>().Generate();
        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(x => x.Map<Payment>(It.IsAny<UpdatePaymentCommand>()))
            .Returns(payment);
        mapperMock.Setup(x => x.Map<PaymentDto>(It.Is<Payment>(a => a == payment)))
            .Returns(paymentDto);

        var repositoryMock = new Mock<IPaymentRepository>();
        repositoryMock.Setup(x => x.UpdateAsync(It.IsAny<Payment>(), It.IsAny<int>()))
            .ReturnsAsync(payment);

        var service = new PaymentService(repositoryMock.Object, mapperMock.Object);

        // Act
        var result = await service.UpdateAsync(updatePayment, 1);

        // Assert
        Assert.Multiple(() => {
            Assert.That(result.Id, Is.EqualTo(paymentDto.Id));
            Assert.That(result.Method, Is.EqualTo(paymentDto.Method));
            Assert.That(result.Amount, Is.EqualTo(paymentDto.Amount));
            Assert.That(result.DateFrom, Is.EqualTo(paymentDto.DateFrom));
            Assert.That(result.DateTo, Is.EqualTo(paymentDto.DateTo));
        });
        repositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Payment>(), It.IsAny<int>()), Times.Once);
    }

    [Test]
    public async Task ShouldDeactivate()
    {
        // Arrange
        var mapperMock = new Mock<IMapper>();

        var repositoryMock = new Mock<IPaymentRepository>();

        var service = new PaymentService(repositoryMock.Object, mapperMock.Object);

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