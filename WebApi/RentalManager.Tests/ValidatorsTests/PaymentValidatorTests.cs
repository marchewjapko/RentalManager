using Bogus;
using FluentValidation.TestHelper;
using RentalManager.Infrastructure.Models.Commands.PaymentCommands;
using RentalManager.Infrastructure.Validators.PaymentValidators;

namespace RentalManager.Tests.ValidatorsTests;

public class PaymentValidatorTests
{
    private readonly BasePaymentValidator _basePaymentValidator = new();
    private readonly CreatePaymentValidator _createPaymentValidator = new();
    private readonly UpdatePaymentValidator _updatePaymentValidator = new();

    private static BasePaymentCommand InitializePayment()
    {
        return new Faker<BasePaymentCommand>()
            .RuleFor(x => x.Method, () => "Cash")
            .RuleFor(x => x.Amount, f => f.Random.Int(1, 100))
            .RuleFor(x => x.DateFrom, () => DateTime.Today)
            .RuleFor(x => x.DateTo, () => DateTime.Today.AddMonths(1))
            .Generate();
    }

    [Test]
    public async Task ValidatePayment_Success()
    {
        // Arrange
        var payment = InitializePayment();

        // Act
        var result = await _basePaymentValidator.TestValidateAsync(payment);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    #region AmountRules

    [Test]
    public async Task ValidateAmount_Failure_Negative()
    {
        // Arrange
        var payment = InitializePayment();
        payment.Amount = -1;

        // Act
        var result = await _basePaymentValidator.TestValidateAsync(payment);

        // Assert
        result.ShouldHaveAnyValidationError();
        result.ShouldHaveValidationErrorFor(x => x.Amount);

        Assert.Multiple(() => {
            Assert.That(result.Errors, Has.Count.EqualTo(1));
            Assert.That(result.Errors[0].ErrorCode, Is.EqualTo("GreaterThanValidator"));
        });
    }

    #endregion

    #region DateTimeToRules

    [Test]
    public async Task ValidateDateTimeTo_Failure_Earlier_Than_From()
    {
        // Arrange
        var payment = InitializePayment();
        payment.DateFrom = DateTime.Today;
        payment.DateTo = DateTime.Today.AddDays(-1);

        // Act
        var result = await _basePaymentValidator.TestValidateAsync(payment);

        // Assert
        result.ShouldHaveAnyValidationError();
        result.ShouldHaveValidationErrorFor(x => x.DateTo);

        Assert.Multiple(() => {
            Assert.That(result.Errors, Has.Count.EqualTo(1));
            Assert.That(result.Errors[0].ErrorCode, Is.EqualTo("GreaterThanValidator"));
        });
    }

    #endregion

    [Test]
    public async Task ValidateCreatePayment_Success()
    {
        // Arrange
        var payment = new Faker<CreatePaymentCommand>()
            .RuleFor(x => x.Method, () => "Cash")
            .RuleFor(x => x.Amount, f => f.Random.Int(1, 100))
            .RuleFor(x => x.DateFrom, () => DateTime.Today)
            .RuleFor(x => x.DateTo, () => DateTime.Today.AddMonths(1))
            .Generate();

        // Act
        var result = await _createPaymentValidator.TestValidateAsync(payment);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Test]
    public async Task ValidateUpdatePayment_Success()
    {
        // Arrange
        var payment = new Faker<UpdatePaymentCommand>()
            .RuleFor(x => x.Method, () => "Cash")
            .RuleFor(x => x.Amount, f => f.Random.Int(1, 100))
            .RuleFor(x => x.DateFrom, () => DateTime.Today)
            .RuleFor(x => x.DateTo, () => DateTime.Today.AddMonths(1))
            .Generate();

        // Act
        var result = await _updatePaymentValidator.TestValidateAsync(payment);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    #region MethodRules

    [Test]
    public async Task ValidateMethod_Failure_Invalid_Character()
    {
        // Arrange
        var payment = InitializePayment();
        payment.Method = "Cash;";

        // Act
        var result = await _basePaymentValidator.TestValidateAsync(payment);

        // Assert
        result.ShouldHaveAnyValidationError();
        result.ShouldHaveValidationErrorFor(x => x.Method);

        Assert.Multiple(() => {
            Assert.That(result.Errors, Has.Count.EqualTo(1));
            Assert.That(result.Errors[0].ErrorCode, Is.EqualTo("RegularExpressionValidator"));
        });
    }

    [Test]
    public async Task ValidateMethod_Failure_Too_Long()
    {
        // Arrange
        var payment = InitializePayment();
        payment.Method = new string('A', 11);

        // Act
        var result = await _basePaymentValidator.TestValidateAsync(payment);

        // Assert
        result.ShouldHaveAnyValidationError();
        result.ShouldHaveValidationErrorFor(x => x.Method);

        Assert.Multiple(() => {
            Assert.That(result.Errors, Has.Count.EqualTo(1));
            Assert.That(result.Errors[0].ErrorCode, Is.EqualTo("MaximumLengthValidator"));
        });
    }

    #endregion
}