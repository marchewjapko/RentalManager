using AutoMapper;
using Bogus;
using RentalManager.Core.Domain;
using RentalManager.Infrastructure.Models.Commands.ClientCommands;
using RentalManager.Infrastructure.Models.DTO;
using RentalManager.Infrastructure.Models.Profiles;

namespace RentalManager.Tests.ProfileTests;

public class ClientProfilesTests
{
    private static readonly MapperConfiguration Config = new(x => x.AddProfile<ClientProfiles>());
    private static readonly IMapper Mapper = Config.CreateMapper();

    [Test]
    public void ClientProfile_ConfigurationShouldBeValid()
    {
        // Act
        Config.AssertConfigurationIsValid();
    }

    [Test]
    public void ClientProfile_ShouldMapBaseCommandToClient()
    {
        // Arrange
        var command = new Faker<BaseClientCommand>()
            .RuleFor(x => x.FirstName, f => f.Name.FirstName())
            .RuleFor(x => x.LastName, f => f.Name.LastName())
            .RuleFor(x => x.Email, f => f.Internet.Email())
            .RuleFor(x => x.PhoneNumber, f => f.Phone.PhoneNumber())
            .RuleFor(x => x.City, f => f.Address.City())
            .RuleFor(x => x.Street, f => f.Address.StreetAddress())
            .RuleFor(x => x.IdCard, f => f.Random.Word())
            .Generate();

        // Act
        var result = Mapper.Map<Client>(command);

        // Assert
        Assert.Multiple(() => {
            Assert.That(result.FirstName, Is.EqualTo(command.FirstName));
            Assert.That(result.LastName, Is.EqualTo(command.LastName));
            Assert.That(result.PhoneNumber, Is.EqualTo(command.PhoneNumber));
            Assert.That(result.Email, Is.EqualTo(command.Email));
            Assert.That(result.IdCard, Is.EqualTo(command.IdCard));
            Assert.That(result.City, Is.EqualTo(command.City));
            Assert.That(result.Street, Is.EqualTo(command.Street));
            Assert.That(result.IsActive, Is.True);
            Assert.That(result.CreatedTs, Is.Not.Default);
            Assert.That(result.UpdatedTs, Is.Null);
        });
    }

    [Test]
    public void ClientProfile_ShouldMapCreateCommandToClient()
    {
        // Arrange
        var command = new Faker<CreateClientCommand>()
            .RuleFor(x => x.FirstName, f => f.Name.FirstName())
            .RuleFor(x => x.LastName, f => f.Name.LastName())
            .RuleFor(x => x.Email, f => f.Internet.Email())
            .RuleFor(x => x.PhoneNumber, f => f.Phone.PhoneNumber())
            .RuleFor(x => x.City, f => f.Address.City())
            .RuleFor(x => x.Street, f => f.Address.StreetAddress())
            .RuleFor(x => x.IdCard, f => f.Random.Word())
            .Generate();

        // Act
        var result = Mapper.Map<Client>(command);

        // Assert
        Assert.Multiple(() => {
            Assert.That(result.FirstName, Is.EqualTo(command.FirstName));
            Assert.That(result.LastName, Is.EqualTo(command.LastName));
            Assert.That(result.PhoneNumber, Is.EqualTo(command.PhoneNumber));
            Assert.That(result.Email, Is.EqualTo(command.Email));
            Assert.That(result.IdCard, Is.EqualTo(command.IdCard));
            Assert.That(result.City, Is.EqualTo(command.City));
            Assert.That(result.Street, Is.EqualTo(command.Street));
            Assert.That(result.IsActive, Is.True);
            Assert.That(result.CreatedTs, Is.Not.Default);
            Assert.That(result.UpdatedTs, Is.Null);
        });
    }

    [Test]
    public void ClientProfile_ShouldMapUpdateCommandToClient()
    {
        // Arrange
        var command = new Faker<UpdateClientCommand>()
            .RuleFor(x => x.FirstName, f => f.Name.FirstName())
            .RuleFor(x => x.LastName, f => f.Name.LastName())
            .RuleFor(x => x.Email, f => f.Internet.Email())
            .RuleFor(x => x.PhoneNumber, f => f.Phone.PhoneNumber())
            .RuleFor(x => x.City, f => f.Address.City())
            .RuleFor(x => x.Street, f => f.Address.StreetAddress())
            .RuleFor(x => x.IdCard, f => f.Random.Word())
            .Generate();

        // Act
        var result = Mapper.Map<Client>(command);

        // Assert
        Assert.Multiple(() => {
            Assert.That(result.FirstName, Is.EqualTo(command.FirstName));
            Assert.That(result.LastName, Is.EqualTo(command.LastName));
            Assert.That(result.PhoneNumber, Is.EqualTo(command.PhoneNumber));
            Assert.That(result.Email, Is.EqualTo(command.Email));
            Assert.That(result.IdCard, Is.EqualTo(command.IdCard));
            Assert.That(result.City, Is.EqualTo(command.City));
            Assert.That(result.Street, Is.EqualTo(command.Street));
            Assert.That(result.IsActive, Is.True);
            Assert.That(result.CreatedTs, Is.Not.Default);
            Assert.That(result.UpdatedTs, Is.Not.Null);
            Assert.That(result.UpdatedTs, Is.Not.Default);
        });
    }

    [Test]
    public void ClientProfile_ShouldMapClientToClientDto()
    {
        // Arrange
        var command = new Faker<Client>()
            .RuleFor(x => x.FirstName, f => f.Name.FirstName())
            .RuleFor(x => x.LastName, f => f.Name.LastName())
            .RuleFor(x => x.Email, f => f.Internet.Email())
            .RuleFor(x => x.PhoneNumber, f => f.Phone.PhoneNumber())
            .RuleFor(x => x.City, f => f.Address.City())
            .RuleFor(x => x.Street, f => f.Address.StreetAddress())
            .RuleFor(x => x.IdCard, f => f.Random.Word())
            .RuleFor(x => x.Id, f => f.Random.Int())
            .RuleFor(x => x.CreatedBy, f => f.Random.Int())
            .RuleFor(x => x.UpdatedTs, f => f.Date.Past())
            .RuleFor(x => x.CreatedTs, f => f.Date.Past())
            .RuleFor(x => x.IsActive, f => f.Random.Bool())
            .Generate();

        // Act
        var result = Mapper.Map<ClientDto>(command);

        // Assert
        Assert.Multiple(() => {
            Assert.That(result.FirstName, Is.EqualTo(command.FirstName));
            Assert.That(result.LastName, Is.EqualTo(command.LastName));
            Assert.That(result.PhoneNumber, Is.EqualTo(command.PhoneNumber));
            Assert.That(result.Email, Is.EqualTo(command.Email));
            Assert.That(result.IdCard, Is.EqualTo(command.IdCard));
            Assert.That(result.City, Is.EqualTo(command.City));
            Assert.That(result.Street, Is.EqualTo(command.Street));
        });
    }
}