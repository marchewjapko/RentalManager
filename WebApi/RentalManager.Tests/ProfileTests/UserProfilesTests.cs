using AutoMapper;
using Bogus;
using RentalManager.Infrastructure.Models.DTO;
using RentalManager.Infrastructure.Models.ExternalServices.IdentityService;
using RentalManager.Infrastructure.Models.Profiles;

namespace RentalManager.Tests.ProfileTests;

public class UserProfilesTests
{
    private static readonly MapperConfiguration Config = new(x => x.AddProfile<UserProfiles>());
    private static readonly IMapper Mapper = Config.CreateMapper();

    [Test]
    public void ClientProfile_ConfigurationShouldBeValid()
    {
        // Act
        Config.AssertConfigurationIsValid();
    }

    [Test]
    public void EquipmentProfile_ShouldMapBaseCommandToEquipment()
    {
        // Arrange
        var firstName = new Faker().Person.FirstName;
        var lastName = new Faker().Person.LastName;

        var groupName1 = new Faker().Random.Word();
        var groupName2 = new Faker().Random.Word();

        var command = new Faker<IdentityServiceUser>()
            .RuleFor(x => x.Pk, f => f.Random.Int())
            .RuleFor(x => x.UserName, f => f.Internet.UserName())
            .RuleFor(x => x.Groups, () => new List<IdentityServiceGroup>
            {
                new Faker<IdentityServiceGroup>()
                    .RuleFor(x => x.Name, () => groupName1),
                new Faker<IdentityServiceGroup>()
                    .RuleFor(x => x.Name, () => groupName2)
            })
            .RuleFor(x => x.Type, f => f.Random.Word())
            .RuleFor(x => x.Attributes, () => new Dictionary<string, object>
            {
                { "first_name", firstName },
                { "last_name", lastName }
            })
            .Generate();

        // Act
        var result = Mapper.Map<UserDto>(command);

        // Assert
        Assert.Multiple(() => {
            Assert.That(result.Id, Is.EqualTo(command.Pk));
            Assert.That(result.FirstName, Is.EqualTo(firstName));
            Assert.That(result.LastName, Is.EqualTo(lastName));
            Assert.That(result.UserName, Is.EqualTo(command.UserName));
            Assert.That(result.Roles, Has.Count.EqualTo(2));
            Assert.That(result.Roles.Any(x => x.Equals(groupName1)), Is.True);
            Assert.That(result.Roles.Any(x => x.Equals(groupName2)), Is.True);
        });
    }
}