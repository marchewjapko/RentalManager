using AutoMapper;
using Bogus;
using RentalManager.Core.Domain;
using RentalManager.Infrastructure.Models.Commands.AgreementCommands;
using RentalManager.Infrastructure.Models.Commands.PaymentCommands;
using RentalManager.Infrastructure.Models.DTO;
using RentalManager.Infrastructure.Models.Profiles;

namespace RentalManager.Tests.ProfileTests;

public class AgreementProfilesTests
{
    private static readonly MapperConfiguration
        Config = new(x => {
            x.AddProfile<AgreementProfiles>();
            x.AddProfile<ClientProfiles>();
            x.AddProfile<PaymentProfiles>();
            x.AddProfile<EquipmentProfiles>();
        });

    private static readonly IMapper Mapper = Config.CreateMapper();

    [Test]
    public void AgreementProfile_ConfigurationShouldBeValid()
    {
        // Act
        Config.AssertConfigurationIsValid();
    }

    [Test]
    public void AgreementProfile_ShouldMapBaseCommandToAgreement()
    {
        // Arrange
        var command = new Faker<AgreementBaseCommand>()
            .RuleFor(x => x.UserId, f => f.Random.Int())
            .RuleFor(x => x.IsActive, f => f.Random.Bool())
            .RuleFor(x => x.ClientId, f => f.Random.Int())
            .RuleFor(x => x.EquipmentsIds, () => new List<int>
            {
                new Faker().Random.Int(),
                new Faker().Random.Int()
            })
            .RuleFor(x => x.Comment, f => f.Lorem.Sentence())
            .RuleFor(x => x.Deposit, f => f.Random.Int())
            .RuleFor(x => x.TransportFromPrice, f => f.Random.Int())
            .RuleFor(x => x.TransportToPrice, f => f.Random.Int())
            .RuleFor(x => x.DateAdded, f => f.Date.Past())
            .Generate();

        // Act
        var result = Mapper.Map<Agreement>(command);

        // Assert
        Assert.Multiple(() => {
            Assert.That(result.UserId, Is.EqualTo(command.UserId));
            Assert.That(result.IsActive, Is.EqualTo(command.IsActive));
            Assert.That(result.ClientId, Is.EqualTo(command.ClientId));
            Assert.That(result.Equipments, Is.Not.Null);
            Assert.That(result.Equipments, Has.Count.EqualTo(0));
            Assert.That(result.Comment, Is.EqualTo(command.Comment));
            Assert.That(result.Deposit, Is.EqualTo(command.Deposit));
            Assert.That(result.TransportFromPrice, Is.EqualTo(command.TransportFromPrice));
            Assert.That(result.TransportToPrice, Is.EqualTo(command.TransportToPrice));
            Assert.That(result.DateAdded, Is.EqualTo(command.DateAdded));
            Assert.That(result.IsActive, Is.EqualTo(command.IsActive));
            Assert.That(result.CreatedTs, Is.Not.Default);
            Assert.That(result.UpdatedTs, Is.Null);
        });
    }

    [Test]
    public void AgreementProfile_ShouldMapCreateCommandToAgreement()
    {
        // Arrange
        var command = new Faker<CreateAgreement>()
            .RuleFor(x => x.UserId, f => f.Random.Int())
            .RuleFor(x => x.IsActive, f => f.Random.Bool())
            .RuleFor(x => x.ClientId, f => f.Random.Int())
            .RuleFor(x => x.EquipmentsIds, () => new List<int>
            {
                new Faker().Random.Int(),
                new Faker().Random.Int()
            })
            .RuleFor(x => x.Comment, f => f.Lorem.Sentence())
            .RuleFor(x => x.Deposit, f => f.Random.Int())
            .RuleFor(x => x.TransportFromPrice, f => f.Random.Int())
            .RuleFor(x => x.TransportToPrice, f => f.Random.Int())
            .RuleFor(x => x.DateAdded, f => f.Date.Past())
            .RuleFor(x => x.Payments, () => new List<CreatePayment>
            {
                new Faker<CreatePayment>()
                    .RuleFor(x => x.Method, f => f.Random.String()),
                new Faker<CreatePayment>()
                    .RuleFor(x => x.Method, f => f.Random.String()),
                new Faker<CreatePayment>()
                    .RuleFor(x => x.Method, f => f.Random.String())
            })
            .Generate();

        // Act
        var result = Mapper.Map<Agreement>(command);

        // Assert
        Assert.Multiple(() => {
            Assert.That(result.UserId, Is.EqualTo(command.UserId));
            Assert.That(result.IsActive, Is.EqualTo(command.IsActive));
            Assert.That(result.ClientId, Is.EqualTo(command.ClientId));
            Assert.That(result.Equipments, Is.Not.Null);
            Assert.That(result.Equipments, Has.Count.EqualTo(0));
            Assert.That(result.Comment, Is.EqualTo(command.Comment));
            Assert.That(result.Deposit, Is.EqualTo(command.Deposit));
            Assert.That(result.TransportFromPrice, Is.EqualTo(command.TransportFromPrice));
            Assert.That(result.TransportToPrice, Is.EqualTo(command.TransportToPrice));
            Assert.That(result.DateAdded, Is.EqualTo(command.DateAdded));
            Assert.That(result.Payments, Has.Count.EqualTo(3));
            Assert.That(result.Payments.First()
                .Method, Is.EqualTo(command.Payments.First()
                .Method));
            Assert.That(result.Payments.Skip(1)
                .First()
                .Method, Is.EqualTo(command.Payments.Skip(1)
                .First()
                .Method));
            Assert.That(result.Payments.Skip(2)
                .First()
                .Method, Is.EqualTo(command.Payments.Skip(2)
                .First()
                .Method));
            Assert.That(result.IsActive, Is.EqualTo(command.IsActive));
            Assert.That(result.CreatedTs, Is.Not.Default);
            Assert.That(result.UpdatedTs, Is.Null);
        });
    }

    [Test]
    public void AgreementProfile_ShouldMapUpdateCommandToAgreement()
    {
        // Arrange
        var command = new Faker<UpdateAgreement>()
            .RuleFor(x => x.UserId, f => f.Random.Int())
            .RuleFor(x => x.IsActive, f => f.Random.Bool())
            .RuleFor(x => x.ClientId, f => f.Random.Int())
            .RuleFor(x => x.EquipmentsIds, () => new List<int>
            {
                new Faker().Random.Int(),
                new Faker().Random.Int()
            })
            .RuleFor(x => x.Comment, f => f.Lorem.Sentence())
            .RuleFor(x => x.Deposit, f => f.Random.Int())
            .RuleFor(x => x.TransportFromPrice, f => f.Random.Int())
            .RuleFor(x => x.TransportToPrice, f => f.Random.Int())
            .RuleFor(x => x.DateAdded, f => f.Date.Past())
            .Generate();

        // Act
        var result = Mapper.Map<Agreement>(command);

        // Assert
        Assert.Multiple(() => {
            Assert.That(result.UserId, Is.EqualTo(command.UserId));
            Assert.That(result.IsActive, Is.EqualTo(command.IsActive));
            Assert.That(result.ClientId, Is.EqualTo(command.ClientId));
            Assert.That(result.Equipments, Is.Not.Null);
            Assert.That(result.Equipments, Has.Count.EqualTo(0));
            Assert.That(result.Comment, Is.EqualTo(command.Comment));
            Assert.That(result.Deposit, Is.EqualTo(command.Deposit));
            Assert.That(result.TransportFromPrice, Is.EqualTo(command.TransportFromPrice));
            Assert.That(result.TransportToPrice, Is.EqualTo(command.TransportToPrice));
            Assert.That(result.DateAdded, Is.EqualTo(command.DateAdded));
            Assert.That(result.IsActive, Is.EqualTo(command.IsActive));
            Assert.That(result.CreatedTs, Is.Not.Default);
            Assert.That(result.UpdatedTs, Is.Not.Null);
            Assert.That(result.UpdatedTs, Is.Not.Default);
        });
    }

    [Test]
    public void AgreementProfile_ShouldMapAgreementToAgreementDto()
    {
        // Arrange
        var command = new Faker<Agreement>()
            .RuleFor(x => x.UserId, f => f.Random.Int())
            .RuleFor(x => x.IsActive, f => f.Random.Bool())
            .RuleFor(x => x.ClientId, f => f.Random.Int())
            .RuleFor(x => x.Client,
                () => new Faker<Client>().RuleFor(x => x.FirstName, f => f.Name.FirstName()))
            .RuleFor(x => x.Equipments, () => new List<Equipment>
            {
                new Faker<Equipment>(),
                new Faker<Equipment>()
            })
            .RuleFor(x => x.Payments, () => new List<Payment>()
            {
                new Faker<Payment>(),
                new Faker<Payment>()
            })
            .RuleFor(x => x.Comment, f => f.Lorem.Sentence())
            .RuleFor(x => x.Deposit, f => f.Random.Int())
            .RuleFor(x => x.TransportFromPrice, f => f.Random.Int())
            .RuleFor(x => x.TransportToPrice, f => f.Random.Int())
            .RuleFor(x => x.DateAdded, f => f.Date.Past())
            .RuleFor(x => x.Id, f => f.Random.Int())
            .RuleFor(x => x.CreatedBy, f => f.Random.Int())
            .RuleFor(x => x.UpdatedTs, f => f.Date.Past())
            .RuleFor(x => x.CreatedTs, f => f.Date.Past())
            .Generate();

        // Act
        var result = Mapper.Map<AgreementDto>(command);

        // Assert
        Assert.Multiple(() => {
            Assert.That(result.Id, Is.EqualTo(command.Id));
            Assert.That(result.User.Id, Is.EqualTo(command.UserId));
            Assert.That(result.IsActive, Is.EqualTo(command.IsActive));
            Assert.That(result.Client.FirstName, Is.EqualTo(command.Client.FirstName));
            Assert.That(result.Equipments.Count(), Is.EqualTo(command.Equipments.Count));
            Assert.That(result.Payments.Count(), Is.EqualTo(command.Payments.Count));
            Assert.That(result.Comment, Is.EqualTo(command.Comment));
            Assert.That(result.Deposit, Is.EqualTo(command.Deposit));
            Assert.That(result.TransportFromPrice, Is.EqualTo(command.TransportFromPrice));
            Assert.That(result.TransportToPrice, Is.EqualTo(command.TransportToPrice));
            Assert.That(result.DateAdded, Is.EqualTo(command.DateAdded));
        });
    }
}