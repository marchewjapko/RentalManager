using Bogus;
using RentalManager.Infrastructure.Commands.ClientCommands;
using RentalManager.Infrastructure.Validators;

namespace RentalManager.Tests.ValidatorsTests;

public class ClientValidatorTests
{
    private readonly ClientBaseValidator _clientBaseValidator = new();

    private static ClientBaseCommand InitializeClient()
    {
        return new Faker<ClientBaseCommand>("pl")
            .RuleFor(x => x.Name, f => f.Name.FirstName())
            .RuleFor(x => x.Surname, f => f.Name.LastName())
            .RuleFor(x => x.PhoneNumber, f => f.Phone.PhoneNumber("###-###-###"))
            .RuleFor(x => x.Email, f => f.Internet.Email())
            .RuleFor(x => x.IdCard, () => "ABC 123456")
            .RuleFor(x => x.City, f => f.Address.City())
            .RuleFor(x => x.Street, f => f.Address.StreetAddress())
            .Generate();
    }

    [Test]
    public async Task ValidateClient_Success()
    {
        // arrange
        var client = InitializeClient();

        // act
        var result = await _clientBaseValidator.ValidateAsync(client);

        //assert
        Assert.That(result.IsValid);
    }

    #region NameRules

    [Test]
    public async Task ValidateName_Success_Two_Names()
    {
        // arrange
        var client = InitializeClient();
        client.Name = "John Jack";

        // act
        var result = await _clientBaseValidator.ValidateAsync(client);

        //assert
        Assert.That(result.IsValid);
    }

    [Test]
    public async Task ValidateName_Failure_Number_Present()
    {
        // arrange
        var client = InitializeClient();
        client.Name = "John1Jack";

        // act
        var result = await _clientBaseValidator.ValidateAsync(client);

        //assert
        Assert.Multiple(() => {
            Assert.That(!result.IsValid);
            Assert.That(result.Errors, Has.Count.EqualTo(1));
            Assert.That(result.Errors[0].ErrorCode, Is.EqualTo("RegularExpressionValidator"));
        });
    }

    [Test]
    public async Task ValidateName_Failure_Symbol_Present()
    {
        // arrange
        var client = InitializeClient();
        client.Name = "John.Jack";

        // act
        var result = await _clientBaseValidator.ValidateAsync(client);

        //assert
        Assert.Multiple(() => {
            Assert.That(!result.IsValid);
            Assert.That(result.Errors, Has.Count.EqualTo(1));
            Assert.That(result.Errors[0].ErrorCode, Is.EqualTo("RegularExpressionValidator"));
        });
    }

    [Test]
    public async Task ValidateName_Failure_Null()
    {
        // arrange
        var client = InitializeClient();
        client.Name = null!;

        // act
        var result = await _clientBaseValidator.ValidateAsync(client);

        //assert
        Assert.Multiple(() => {
            Assert.That(!result.IsValid);
            Assert.That(result.Errors, Has.Count.EqualTo(1));
            Assert.That(result.Errors[0].ErrorCode, Is.EqualTo("NotEmptyValidator"));
        });
    }

    [Test]
    public async Task ValidateName_Failure_Too_Long()
    {
        // arrange
        var client = InitializeClient();
        client.Name = new string('A', 101);

        // act
        var result = await _clientBaseValidator.ValidateAsync(client);

        // assert
        Assert.Multiple(() => {
            Assert.That(!result.IsValid);
            Assert.That(result.Errors, Has.Count.EqualTo(1));
            Assert.That(result.Errors[0].ErrorCode, Is.EqualTo("MaximumLengthValidator"));
        });
    }

    #endregion

    #region SurnameRules

    [Test]
    public async Task ValidateSurname_Success_Two_Surnames()
    {
        // arrange
        var client = InitializeClient();
        client.Surname = "Scott Brown";

        // act
        var result = await _clientBaseValidator.ValidateAsync(client);

        //assert
        Assert.That(result.IsValid);
    }

    [Test]
    public async Task ValidateSurname_Failure_Number_Present()
    {
        // arrange
        var client = InitializeClient();
        client.Surname = "Scott1Brown";

        // act
        var result = await _clientBaseValidator.ValidateAsync(client);

        //assert
        Assert.Multiple(() => {
            Assert.That(!result.IsValid);
            Assert.That(result.Errors, Has.Count.EqualTo(1));
            Assert.That(result.Errors[0].ErrorCode, Is.EqualTo("RegularExpressionValidator"));
        });
    }

    [Test]
    public async Task ValidateSurname_Failure_Symbol_Present()
    {
        // arrange
        var client = InitializeClient();
        client.Surname = "Scott.Brown";

        // act
        var result = await _clientBaseValidator.ValidateAsync(client);

        //assert
        Assert.Multiple(() => {
            Assert.That(!result.IsValid);
            Assert.That(result.Errors, Has.Count.EqualTo(1));
            Assert.That(result.Errors[0].ErrorCode, Is.EqualTo("RegularExpressionValidator"));
        });
    }

    [Test]
    public async Task ValidateSurname_Failure_Null()
    {
        // arrange
        var client = InitializeClient();
        client.Surname = null!;

        // act
        var result = await _clientBaseValidator.ValidateAsync(client);

        //assert
        Assert.Multiple(() => {
            Assert.That(!result.IsValid);
            Assert.That(result.Errors, Has.Count.EqualTo(1));
            Assert.That(result.Errors[0].ErrorCode, Is.EqualTo("NotEmptyValidator"));
        });
    }

    [Test]
    public async Task ValidateSurname_Failure_Too_Long()
    {
        // arrange
        var client = InitializeClient();
        client.Surname = new string('A', 101);

        // act
        var result = await _clientBaseValidator.ValidateAsync(client);

        // assert
        Assert.Multiple(() => {
            Assert.That(!result.IsValid);
            Assert.That(result.Errors, Has.Count.EqualTo(1));
            Assert.That(result.Errors[0].ErrorCode, Is.EqualTo("MaximumLengthValidator"));
        });
    }

    #endregion

    #region PhoneNumberRules

    [Test]
    public async Task ValidatePhoneNumber_Success_With_Direction()
    {
        // arrange
        var client = InitializeClient();
        client.PhoneNumber = "+48 123 456 789";

        // act
        var result = await _clientBaseValidator.ValidateAsync(client);

        //assert
        Assert.That(result.IsValid);
    }

    [Test]
    public async Task ValidatePhoneNumber_Success_With_Spaces()
    {
        // arrange
        var client = InitializeClient();
        client.PhoneNumber = "123 456 789";

        // act
        var result = await _clientBaseValidator.ValidateAsync(client);

        //assert
        Assert.That(result.IsValid);
    }

    [Test]
    public async Task ValidatePhoneNumber_Success_With_Dashes()
    {
        // arrange
        var client = InitializeClient();
        client.PhoneNumber = "123-456-789";

        // act
        var result = await _clientBaseValidator.ValidateAsync(client);

        //assert
        Assert.That(result.IsValid);
    }

    [Test]
    public async Task ValidatePhoneNumber_Success_Without_Separators()
    {
        // arrange
        var client = InitializeClient();
        client.PhoneNumber = "123456789";

        // act
        var result = await _clientBaseValidator.ValidateAsync(client);

        //assert
        Assert.That(result.IsValid);
    }

    [Test]
    public async Task ValidatePhoneNumber_Success_Stationary_Format()
    {
        // arrange
        var client = InitializeClient();
        client.PhoneNumber = "12 34 567 89";

        // act
        var result = await _clientBaseValidator.ValidateAsync(client);

        //assert
        Assert.That(result.IsValid);
    }

    #endregion

    #region EmailRules

    [Test]
    public async Task ValidateEmail_Failure_Invalid_Character()
    {
        // arrange
        var client = InitializeClient();
        client.Email = "email!@email.com";

        // act
        var result = await _clientBaseValidator.ValidateAsync(client);

        // assert
        Assert.Multiple(() => {
            Assert.That(!result.IsValid);
            Assert.That(result.Errors, Has.Count.EqualTo(1));
            Assert.That(result.Errors[0].ErrorCode, Is.EqualTo("RegularExpressionValidator"));
        });
    }

    [Test]
    public async Task ValidateEmail_Failure_Too_Long()
    {
        // arrange
        var client = InitializeClient();
        client.Email = new string('A', 91) + "@email.com"; // 101 characters

        // act
        var result = await _clientBaseValidator.ValidateAsync(client);

        // assert
        Assert.Multiple(() => {
            Assert.That(!result.IsValid);
            Assert.That(result.Errors, Has.Count.EqualTo(1));
            Assert.That(result.Errors[0].ErrorCode, Is.EqualTo("MaximumLengthValidator"));
        });
    }

    #endregion

    #region IdCardRules

    [Test]
    public async Task ValidateIdCard_Success_Without_Separator()
    {
        // arrange
        var client = InitializeClient();
        client.IdCard = "ABC123456";

        // act
        var result = await _clientBaseValidator.ValidateAsync(client);

        //assert
        Assert.That(result.IsValid);
    }

    [Test]
    public async Task ValidateIdCard_Failure_Invalid_Character()
    {
        // arrange
        var client = InitializeClient();
        client.IdCard = "ABC!23456";

        // act
        var result = await _clientBaseValidator.ValidateAsync(client);

        //assert
        Assert.Multiple(() => {
            Assert.That(!result.IsValid);
            Assert.That(result.Errors, Has.Count.EqualTo(1));
            Assert.That(result.Errors[0].ErrorCode, Is.EqualTo("RegularExpressionValidator"));
        });
    }

    [Test]
    public async Task ValidateIdCard_Failure_Too_Long()
    {
        // arrange
        var client = InitializeClient();
        client.IdCard = "ABC1234567";

        // act
        var result = await _clientBaseValidator.ValidateAsync(client);

        //assert
        Assert.Multiple(() => {
            Assert.That(!result.IsValid);
            Assert.That(result.Errors, Has.Count.EqualTo(1));
            Assert.That(result.Errors[0].ErrorCode, Is.EqualTo("RegularExpressionValidator"));
        });
    }

    #endregion

    #region CityRules

    [Test]
    public async Task ValidateCity_Failure_Invalid_Character()
    {
        // arrange
        var client = InitializeClient();
        client.City = "Paris!";

        // act
        var result = await _clientBaseValidator.ValidateAsync(client);

        // assert
        Assert.Multiple(() => {
            Assert.That(!result.IsValid);
            Assert.That(result.Errors, Has.Count.EqualTo(1));
            Assert.That(result.Errors[0].ErrorCode, Is.EqualTo("RegularExpressionValidator"));
        });
    }

    [Test]
    public async Task ValidateCity_Failure_Too_Long()
    {
        // arrange
        var client = InitializeClient();
        client.City = new string('A', 101);

        // act
        var result = await _clientBaseValidator.ValidateAsync(client);

        // assert
        Assert.Multiple(() => {
            Assert.That(!result.IsValid);
            Assert.That(result.Errors, Has.Count.EqualTo(1));
            Assert.That(result.Errors[0].ErrorCode, Is.EqualTo("MaximumLengthValidator"));
        });
    }

    #endregion

    #region StreetRules

    [Test]
    public async Task ValidateStreet_Failure_Invalid_Character()
    {
        // arrange
        var client = InitializeClient();
        client.Street = "al. AK 12B!";

        // act
        var result = await _clientBaseValidator.ValidateAsync(client);

        // assert
        Assert.Multiple(() => {
            Assert.That(!result.IsValid);
            Assert.That(result.Errors, Has.Count.EqualTo(1));
            Assert.That(result.Errors[0].ErrorCode, Is.EqualTo("RegularExpressionValidator"));
        });
    }

    [Test]
    public async Task ValidateStreet_Failure_Too_Long()
    {
        // arrange
        var client = InitializeClient();
        client.Street = new string('A', 101);

        // act
        var result = await _clientBaseValidator.ValidateAsync(client);

        // assert
        Assert.Multiple(() => {
            Assert.That(!result.IsValid);
            Assert.That(result.Errors, Has.Count.EqualTo(1));
            Assert.That(result.Errors[0].ErrorCode, Is.EqualTo("MaximumLengthValidator"));
        });
    }

    #endregion
}