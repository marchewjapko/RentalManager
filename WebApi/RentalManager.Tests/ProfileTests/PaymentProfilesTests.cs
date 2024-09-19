using AutoMapper;
using Bogus;
using RentalManager.Core.Domain;
using RentalManager.Infrastructure.Models.Commands.PaymentCommands;
using RentalManager.Infrastructure.Models.DTO;
using RentalManager.Infrastructure.Models.Profiles;

namespace RentalManager.Tests.ProfileTests;

public class PaymentProfilesTests
{
    private static readonly MapperConfiguration Config = new(x => x.AddProfile<PaymentProfiles>());
    private static readonly IMapper Mapper = Config.CreateMapper();

    [Test]
    public void PaymentProfile_ConfigurationShouldBeValid()
    {
        // Act
        Config.AssertConfigurationIsValid();
    }

    [Test]
    public void PaymentProfile_ShouldMapBaseCommandToPayment()
    {
        // Arrange
        var command = new Faker<BasePaymentCommand>()
            .RuleFor(x => x.Method, f => f.Random.Word())
            .RuleFor(x => x.Amount, f => f.Random.Int())
            .RuleFor(x => x.DateFrom, f => f.Date.Past())
            .RuleFor(x => x.DateTo, f => f.Date.Past())
            .Generate();

        // Act
        var result = Mapper.Map<Payment>(command);

        // Assert
        Assert.Multiple(() => {
            Assert.That(result.Method, Is.EqualTo(command.Method));
            Assert.That(result.Amount, Is.EqualTo(command.Amount));
            Assert.That(result.DateFrom, Is.EqualTo(command.DateFrom));
            Assert.That(result.DateTo, Is.EqualTo(command.DateTo));
            Assert.That(result.IsActive, Is.True);
            Assert.That(result.CreatedTs, Is.Not.Default);
            Assert.That(result.UpdatedTs, Is.Null);
        });
    }

    [Test]
    public void PaymentProfile_ShouldMapCreateCommandToPayment()
    {
        // Arrange
        var command = new Faker<CreatePaymentCommand>()
            .RuleFor(x => x.Method, f => f.Random.Word())
            .RuleFor(x => x.Amount, f => f.Random.Int())
            .RuleFor(x => x.DateFrom, f => f.Date.Past())
            .RuleFor(x => x.DateTo, f => f.Date.Past())
            .RuleFor(x => x.AgreementId, f => f.Random.Int())
            .Generate();

        // Act
        var result = Mapper.Map<Payment>(command);

        // Assert
        Assert.Multiple(() => {
            Assert.That(result.Method, Is.EqualTo(command.Method));
            Assert.That(result.Amount, Is.EqualTo(command.Amount));
            Assert.That(result.DateFrom, Is.EqualTo(command.DateFrom));
            Assert.That(result.DateTo, Is.EqualTo(command.DateTo));
            Assert.That(result.AgreementId, Is.EqualTo(command.AgreementId));
            Assert.That(result.IsActive, Is.True);
            Assert.That(result.CreatedTs, Is.Not.Default);
            Assert.That(result.UpdatedTs, Is.Null);
        });
    }

    [Test]
    public void PaymentProfile_ShouldMapUpdateCommandToPayment()
    {
        // Arrange
        var command = new Faker<UpdatePaymentCommand>()
            .RuleFor(x => x.Method, f => f.Random.Word())
            .RuleFor(x => x.Amount, f => f.Random.Int())
            .RuleFor(x => x.DateFrom, f => f.Date.Past())
            .RuleFor(x => x.DateTo, f => f.Date.Past())
            .Generate();

        // Act
        var result = Mapper.Map<Payment>(command);

        // Assert
        Assert.Multiple(() => {
            Assert.That(result.Method, Is.EqualTo(command.Method));
            Assert.That(result.Amount, Is.EqualTo(command.Amount));
            Assert.That(result.DateFrom, Is.EqualTo(command.DateFrom));
            Assert.That(result.DateTo, Is.EqualTo(command.DateTo));
            Assert.That(result.IsActive, Is.True);
            Assert.That(result.CreatedTs, Is.Not.Default);
            Assert.That(result.UpdatedTs, Is.Not.Null);
            Assert.That(result.UpdatedTs, Is.Not.Default);
        });
    }

    [Test]
    public void PaymentProfile_ShouldMapPaymentToPaymentDto()
    {
        // Arrange
        var command = new Faker<Payment>()
            .RuleFor(x => x.Method, f => f.Random.Word())
            .RuleFor(x => x.Amount, f => f.Random.Int())
            .RuleFor(x => x.DateFrom, f => f.Date.Past())
            .RuleFor(x => x.DateTo, f => f.Date.Past())
            .RuleFor(x => x.Id, f => f.Random.Int())
            .RuleFor(x => x.CreatedBy, f => f.Random.Int())
            .RuleFor(x => x.UpdatedTs, f => f.Date.Past())
            .RuleFor(x => x.CreatedTs, f => f.Date.Past())
            .RuleFor(x => x.IsActive, f => f.Random.Bool())
            .Generate();

        // Act
        var result = Mapper.Map<PaymentDto>(command);

        // Assert
        Assert.Multiple(() => {
            Assert.That(result.Method, Is.EqualTo(command.Method));
            Assert.That(result.Amount, Is.EqualTo(command.Amount));
            Assert.That(result.DateFrom, Is.EqualTo(command.DateFrom));
            Assert.That(result.DateTo, Is.EqualTo(command.DateTo));
        });
    }
}