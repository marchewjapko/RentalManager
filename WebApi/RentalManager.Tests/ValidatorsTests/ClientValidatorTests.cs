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
        // Arrange
        var client = InitializeClient();

        // Act
        var result = await _clientBaseValidator.TestValidateAsync(client);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    #region NameRules

    [Test]
    public async Task ValidateName_Success_Two_Names()
    {
        // Arrange
        var client = InitializeClient();
        client.FirstName = "John Jack";

        // Act
        var result = await _clientBaseValidator.TestValidateAsync(client);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Test]
    public async Task ValidateName_Failure_Number_Present()
    {
        // Arrange
        var client = InitializeClient();
        client.FirstName = "John1Jack";

        // Act
        var result = await _clientBaseValidator.TestValidateAsync(client);

        // Assert
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
        // Arrange
        var client = InitializeClient();
        client.FirstName = "John.Jack";

        // Act
        var result = await _clientBaseValidator.TestValidateAsync(client);

        // Assert
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
        // Arrange
        var client = InitializeClient();
        client.FirstName = null!;

        // Act
        var result = await _clientBaseValidator.TestValidateAsync(client);

        // Assert
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
        // Arrange
        var client = InitializeClient();
        client.FirstName = new string('A', 101);

        // Act
        var result = await _clientBaseValidator.TestValidateAsync(client);

        // Assert
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
        // Arrange
        var client = InitializeClient();
        client.LastName = "Scott Brown";

        // Act
        var result = await _clientBaseValidator.TestValidateAsync(client);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Test]
    public async Task ValidateSurname_Failure_Number_Present()
    {
        // Arrange
        var client = InitializeClient();
        client.LastName = "Scott1Brown";

        // Act
        var result = await _clientBaseValidator.TestValidateAsync(client);

        // Assert
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
        // Arrange
        var client = InitializeClient();
        client.LastName = "Scott.Brown";

        // Act
        var result = await _clientBaseValidator.TestValidateAsync(client);

        // Assert
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
        // Arrange
        var client = InitializeClient();
        client.LastName = null!;

        // Act
        var result = await _clientBaseValidator.TestValidateAsync(client);

        // Assert
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
        // Arrange
        var client = InitializeClient();
        client.LastName = new string('A', 101);

        // Act
        var result = await _clientBaseValidator.TestValidateAsync(client);

        // Assert
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
        // Arrange
        var client = InitializeClient();
        client.PhoneNumber = "+48 123 456 789";

        // Act
        var result = await _clientBaseValidator.TestValidateAsync(client);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Test]
    public async Task ValidatePhoneNumber_Success_With_Spaces()
    {
        // Arrange
        var client = InitializeClient();
        client.PhoneNumber = "123 456 789";

        // Act
        var result = await _clientBaseValidator.TestValidateAsync(client);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Test]
    public async Task ValidatePhoneNumber_Success_With_Dashes()
    {
        // Arrange
        var client = InitializeClient();
        client.PhoneNumber = "123-456-789";

        // Act
        var result = await _clientBaseValidator.TestValidateAsync(client);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Test]
    public async Task ValidatePhoneNumber_Success_Without_Separators()
    {
        // Arrange
        var client = InitializeClient();
        client.PhoneNumber = "123456789";

        // Act
        var result = await _clientBaseValidator.TestValidateAsync(client);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Test]
    public async Task ValidatePhoneNumber_Success_Stationary_Format()
    {
        // Arrange
        var client = InitializeClient();
        client.PhoneNumber = "12 34 567 89";

        // Act
        var result = await _clientBaseValidator.TestValidateAsync(client);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    #endregion

    #region EmailRules

    [Test]
    public async Task ValidateEmail_Failure_Invalid_Character()
    {
        // Arrange
        var client = InitializeClient();
        client.Email = "email!@email.com";

        // Act
        var result = await _clientBaseValidator.TestValidateAsync(client);

        // Assert
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
        // Arrange
        var client = InitializeClient();
        client.Email = new string('A', 91) + "@email.com"; // 101 characters

        // Act
        var result = await _clientBaseValidator.TestValidateAsync(client);

        // Assert
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
        // Arrange
        var client = InitializeClient();
        client.IdCard = "ABC123456";

        // Act
        var result = await _clientBaseValidator.TestValidateAsync(client);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Test]
    public async Task ValidateIdCard_Failure_Invalid_Character()
    {
        // Arrange
        var client = InitializeClient();
        client.IdCard = "ABC!23456";

        // Act
        var result = await _clientBaseValidator.TestValidateAsync(client);

        // Assert
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
        // Arrange
        var client = InitializeClient();
        client.IdCard = "ABC1234567";

        // Act
        var result = await _clientBaseValidator.TestValidateAsync(client);

        // Assert
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
        // Arrange
        var client = InitializeClient();
        client.City = "Paris!";

        // Act
        var result = await _clientBaseValidator.TestValidateAsync(client);

        // Assert
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
        // Arrange
        var client = InitializeClient();
        client.City = new string('A', 101);

        // Act
        var result = await _clientBaseValidator.TestValidateAsync(client);

        // Assert
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
        // Arrange
        var client = InitializeClient();
        client.Street = "al. AK 12B!";

        // Act
        var result = await _clientBaseValidator.TestValidateAsync(client);

        // Assert
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
        // Arrange
        var client = InitializeClient();
        client.Street = new string('A', 101);

        // Act
        var result = await _clientBaseValidator.TestValidateAsync(client);

        // Assert
        result.ShouldHaveAnyValidationError();
        result.ShouldHaveValidationErrorFor(x => x.Street);

        Assert.Multiple(() => {
            Assert.That(result.Errors, Has.Count.EqualTo(1));
            Assert.That(result.Errors[0].ErrorCode, Is.EqualTo("MaximumLengthValidator"));
        });
    }

    #endregion
}