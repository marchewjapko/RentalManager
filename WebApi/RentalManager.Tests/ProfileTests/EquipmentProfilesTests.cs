using AutoMapper;
using Bogus;
using RentalManager.Core.Domain;
using RentalManager.Infrastructure.Models.Commands.EquipmentCommands;
using RentalManager.Infrastructure.Models.DTO;
using RentalManager.Infrastructure.Models.Profiles;

namespace RentalManager.Tests.ProfileTests;

public class EquipmentProfilesTests
{
    private static readonly MapperConfiguration Config = new(x => x.AddProfile<EquipmentProfiles>());
    private static readonly IMapper Mapper = Config.CreateMapper();

    [Test]
    public void EquipmentProfile_ConfigurationShouldBeValid()
    {
        // Act
        Config.AssertConfigurationIsValid();
    }

    [Test]
    public void EquipmentProfile_ShouldMapBaseCommandToEquipment()
    {
        // Arrange
        var command = new Faker<EquipmentBaseCommand>()
            .RuleFor(x => x.Name, f => f.Name.FirstName())
            .RuleFor(x => x.Price, f => f.Random.Int())
            .Generate();
        
        // Act
        var result = Mapper.Map<Equipment>(command);
        
        // Assert
        Assert.Multiple(() => {
            Assert.That(result.Name, Is.EqualTo(command.Name));
            Assert.That(result.Price, Is.EqualTo(command.Price));
            Assert.That(result.IsActive, Is.True);
            Assert.That(result.CreatedTs, Is.Not.Default);
            Assert.That(result.UpdatedTs, Is.Null);
        });
        
    }

    [Test]
    public void EquipmentProfile_ShouldMapCreateCommandToEquipment()
    {
        // Arrange
        var command = new Faker<CreateEquipment>()
            .RuleFor(x => x.Name, f => f.Name.FirstName())
            .RuleFor(x => x.Price, f => f.Random.Int())
            .Generate();
        
        // Act
        var result = Mapper.Map<Equipment>(command);
        
        // Assert
        Assert.Multiple(() => {
            Assert.That(result.Name, Is.EqualTo(command.Name));
            Assert.That(result.Price, Is.EqualTo(command.Price));
            Assert.That(result.IsActive, Is.True);
            Assert.That(result.CreatedTs, Is.Not.Default);
            Assert.That(result.UpdatedTs, Is.Null);
        });
    }

    [Test]
    public void EquipmentProfile_ShouldMapUpdateCommandToEquipment()
    {
        // Arrange
        var command = new Faker<UpdateEquipment>()
            .RuleFor(x => x.Name, f => f.Name.FirstName())
            .RuleFor(x => x.Price, f => f.Random.Int())
            .Generate();
        
        // Act
        var result = Mapper.Map<Equipment>(command);
        
        // Assert
        Assert.Multiple(() => {
            Assert.That(result.Name, Is.EqualTo(command.Name));
            Assert.That(result.Price, Is.EqualTo(command.Price));
            Assert.That(result.IsActive, Is.True);
            Assert.That(result.CreatedTs, Is.Not.Default);
            Assert.That(result.UpdatedTs, Is.Not.Null);
            Assert.That(result.UpdatedTs, Is.Not.Default);
        });
    }

    [Test]
    public void EquipmentProfile_ShouldMapEquipmentToEquipmentDto()
    {
        // Arrange
        var command = new Faker<Equipment>()
            .RuleFor(x => x.Name, f => f.Name.FirstName())
            .RuleFor(x => x.Price, f => f.Random.Int())
            .RuleFor(x => x.Id, f => f.Random.Int())
            .RuleFor(x => x.CreatedBy, f => f.Random.Int())
            .RuleFor(x => x.UpdatedTs, f => f.Date.Past())
            .RuleFor(x => x.CreatedTs, f => f.Date.Past())
            .RuleFor(x => x.IsActive, f => f.Random.Bool())
            .Generate();
        
        // Act
        var result = Mapper.Map<EquipmentDto>(command);
        
        // Assert
        Assert.Multiple(() => {
            Assert.That(result.Name, Is.EqualTo(command.Name));
            Assert.That(result.Price, Is.EqualTo(command.Price));
        });
    }
}