using Bogus;
using FluentValidation.TestHelper;
using RentalManager.Infrastructure.Models.Commands.ClientCommands;
using RentalManager.Infrastructure.Validators.ClientValidators;

namespace RentalManager.Tests.ValidatorsTests;

public class ClientValidatorTests
{
    private readonly ClientBaseValidator _clientBaseValidator = new();

    private static ClientBaseCommand InitializeClient()
    {
        return new Faker<ClientBaseCommand>("pl")
            .RuleFor(x => x.FirstName, f => f.Name.FirstName())
            .RuleFor(x => x.LastName, f => f.Name.LastName())
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
        var result = await _clientBaseValidator.TestValidateAsync(client);

        //assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    #region NameRules

    [Test]
    public async Task ValidateName_Success_Two_Names()
    {
        // arrange
        var client = InitializeClient();
        client.FirstName = "John Jack";

        // act
        var result = await _clientBaseValidator.TestValidateAsync(client);

        //assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Test]
    public async Task ValidateName_Failure_Number_Present()
    {
        // arrange
        var client = InitializeClient();
        client.FirstName = "John1Jack";

        // act
        var result = await _clientBaseValidator.TestValidateAsync(client);

        //assert
        result.ShouldHaveAnyValidationError();
        result.ShouldHaveValidationErrorFor(x => x.FirstName);

        Assert.Multiple(() => {
            Assert.That(result.Errors, Has.Count.EqualTo(1));
            Assert.That(result.Errors[0].ErrorCode, Is.EqualTo("RegularExpressionValidator"));
        });
    }

    [Test]
    public async Task ValidateName_Failure_Symbol_Present()
    {
        // arrange
        var client = InitializeClient();
        client.FirstName = "John.Jack";

        // act
        var result = await _clientBaseValidator.TestValidateAsync(client);

        //assert
        result.ShouldHaveAnyValidationError();
        result.ShouldHaveValidationErrorFor(x => x.FirstName);

        Assert.Multiple(() => {
            Assert.That(result.Errors, Has.Count.EqualTo(1));
            Assert.That(result.Errors[0].ErrorCode, Is.EqualTo("RegularExpressionValidator"));
        });
    }

    [Test]
    public async Task ValidateName_Failure_Null()
    {
        // arrange
        var client = InitializeClient();
        client.FirstName = null!;

        // act
        var result = await _clientBaseValidator.TestValidateAsync(client);

        //assert
        result.ShouldHaveAnyValidationError();
        result.ShouldHaveValidationErrorFor(x => x.FirstName);

        Assert.Multiple(() => {
            Assert.That(result.Errors, Has.Count.EqualTo(1));
            Assert.That(result.Errors[0].ErrorCode, Is.EqualTo("NotEmptyValidator"));
        });
    }

    [Test]
    public async Task ValidateName_Failure_Too_Long()
    {
        // arrange
        var client = InitializeClient();
        client.FirstName = new string('A', 101);

        // act
        var result = await _clientBaseValidator.TestValidateAsync(client);

        // assert
        result.ShouldHaveAnyValidationError();
        result.ShouldHaveValidationErrorFor(x => x.FirstName);

        Assert.Multiple(() => {
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
        client.LastName = "Scott Brown";

        // act
        var result = await _clientBaseValidator.TestValidateAsync(client);

        //assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Test]
    public async Task ValidateSurname_Failure_Number_Present()
    {
        // arrange
        var client = InitializeClient();
        client.LastName = "Scott1Brown";

        // act
        var result = await _clientBaseValidator.TestValidateAsync(client);

        //assert
        result.ShouldHaveAnyValidationError();
        result.ShouldHaveValidationErrorFor(x => x.LastName);

        Assert.Multiple(() => {
            Assert.That(result.Errors, Has.Count.EqualTo(1));
            Assert.That(result.Errors[0].ErrorCode, Is.EqualTo("RegularExpressionValidator"));
        });
    }

    [Test]
    public async Task ValidateSurname_Failure_Symbol_Present()
    {
        // arrange
        var client = InitializeClient();
        client.LastName = "Scott.Brown";

        // act
        var result = await _clientBaseValidator.TestValidateAsync(client);

        //assert
        result.ShouldHaveAnyValidationError();
        result.ShouldHaveValidationErrorFor(x => x.LastName);

        Assert.Multiple(() => {
            Assert.That(result.Errors, Has.Count.EqualTo(1));
            Assert.That(result.Errors[0].ErrorCode, Is.EqualTo("RegularExpressionValidator"));
        });
    }

    [Test]
    public async Task ValidateSurname_Failure_Null()
    {
        // arrange
        var client = InitializeClient();
        client.LastName = null!;

        // act
        var result = await _clientBaseValidator.TestValidateAsync(client);

        //assert
        result.ShouldHaveAnyValidationError();
        result.ShouldHaveValidationErrorFor(x => x.LastName);

        Assert.Multiple(() => {
            Assert.That(result.Errors, Has.Count.EqualTo(1));
            Assert.That(result.Errors[0].ErrorCode, Is.EqualTo("NotEmptyValidator"));
        });
    }

    [Test]
    public async Task ValidateSurname_Failure_Too_Long()
    {
        // arrange
        var client = InitializeClient();
        client.LastName = new string('A', 101);

        // act
        var result = await _clientBaseValidator.TestValidateAsync(client);

        // assert
        result.ShouldHaveAnyValidationError();
        result.ShouldHaveValidationErrorFor(x => x.LastName);

        Assert.Multiple(() => {
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
        var result = await _clientBaseValidator.TestValidateAsync(client);

        //assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Test]
    public async Task ValidatePhoneNumber_Success_With_Spaces()
    {
        // arrange
        var client = InitializeClient();
        client.PhoneNumber = "123 456 789";

        // act
        var result = await _clientBaseValidator.TestValidateAsync(client);

        //assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Test]
    public async Task ValidatePhoneNumber_Success_With_Dashes()
    {
        // arrange
        var client = InitializeClient();
        client.PhoneNumber = "123-456-789";

        // act
        var result = await _clientBaseValidator.TestValidateAsync(client);

        //assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Test]
    public async Task ValidatePhoneNumber_Success_Without_Separators()
    {
        // arrange
        var client = InitializeClient();
        client.PhoneNumber = "123456789";

        // act
        var result = await _clientBaseValidator.TestValidateAsync(client);

        //assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Test]
    public async Task ValidatePhoneNumber_Success_Stationary_Format()
    {
        // arrange
        var client = InitializeClient();
        client.PhoneNumber = "12 34 567 89";

        // act
        var result = await _clientBaseValidator.TestValidateAsync(client);

        //assert
        result.ShouldNotHaveAnyValidationErrors();
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
        var result = await _clientBaseValidator.TestValidateAsync(client);

        // assert
        result.ShouldHaveAnyValidationError();
        result.ShouldHaveValidationErrorFor(x => x.Email);

        Assert.Multiple(() => {
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
        var result = await _clientBaseValidator.TestValidateAsync(client);

        // assert
        result.ShouldHaveAnyValidationError();
        result.ShouldHaveValidationErrorFor(x => x.Email);

        Assert.Multiple(() => {
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
        var result = await _clientBaseValidator.TestValidateAsync(client);

        //assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Test]
    public async Task ValidateIdCard_Failure_Invalid_Character()
    {
        // arrange
        var client = InitializeClient();
        client.IdCard = "ABC!23456";

        // act
        var result = await _clientBaseValidator.TestValidateAsync(client);

        //assert
        result.ShouldHaveAnyValidationError();
        result.ShouldHaveValidationErrorFor(x => x.IdCard);

        Assert.Multiple(() => {
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
        var result = await _clientBaseValidator.TestValidateAsync(client);

        //assert
        result.ShouldHaveAnyValidationError();
        result.ShouldHaveValidationErrorFor(x => x.IdCard);

        Assert.Multiple(() => {
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
        var result = await _clientBaseValidator.TestValidateAsync(client);

        // assert
        result.ShouldHaveAnyValidationError();
        result.ShouldHaveValidationErrorFor(x => x.City);

        Assert.Multiple(() => {
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
        var result = await _clientBaseValidator.TestValidateAsync(client);

        // assert
        result.ShouldHaveAnyValidationError();
        result.ShouldHaveValidationErrorFor(x => x.City);

        Assert.Multiple(() => {
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
        var result = await _clientBaseValidator.TestValidateAsync(client);

        // assert
        result.ShouldHaveAnyValidationError();
        result.ShouldHaveValidationErrorFor(x => x.Street);

        Assert.Multiple(() => {
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
        var result = await _clientBaseValidator.TestValidateAsync(client);

        // assert
        result.ShouldHaveAnyValidationError();
        result.ShouldHaveValidationErrorFor(x => x.Street);

        Assert.Multiple(() => {
            Assert.That(result.Errors, Has.Count.EqualTo(1));
            Assert.That(result.Errors[0].ErrorCode, Is.EqualTo("MaximumLengthValidator"));
        });
    }

    #endregion
}