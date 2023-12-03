using Bogus;
using RentalManager.Infrastructure.Commands.PaymentCommands;
using RentalManager.Infrastructure.Validators;

namespace RentalManager.Tests.ValidatorsTests;

public class PaymentValidatorTests
{
    private readonly PaymentValidator _paymentValidator = new PaymentValidator();

    private static PaymentBaseCommand InitializePayment()
    {
        return new Faker<PaymentBaseCommand>()
            .RuleFor(x => x.Method, () => "Cash")
            .RuleFor(x => x.Amount, f => f.Random.Int(1, 100))
            .RuleFor(x => x.DateTimeFrom, () => DateTime.Today)
            .RuleFor(x => x.DateTimeTo, () => DateTime.Today.AddMonths(1))
            .Generate();
    }

    [Test]
    public async Task ValidatePayment_Success()
    {
        // arrange
        var payment = InitializePayment();

        // act
        var result = await _paymentValidator.ValidateAsync(payment);

        // assert
        Assert.That(result.IsValid);
    }

    #region MethodRules

    [Test]
    public async Task ValidateMethod_Failure_Invalid_Character()
    {
        // arrange
        var payment = InitializePayment();
        payment.Method = "Cash;";

        // act
        var result = await _paymentValidator.ValidateAsync(payment);

        // assert
        Assert.Multiple((() => {
            Assert.That(!result.IsValid);
            Assert.That(result.Errors, Has.Count.EqualTo(1));
            Assert.That(result.Errors[0].ErrorCode, Is.EqualTo("RegularExpressionValidator"));
        }));
    }
    
    [Test]
    public async Task ValidateMethod_Failure_Too_Long()
    {
        // arrange
        var payment = InitializePayment();
        payment.Method = new string('A', 11);

        // act
        var result = await _paymentValidator.ValidateAsync(payment);

        // assert
        Assert.Multiple((() => {
            Assert.That(!result.IsValid);
            Assert.That(result.Errors, Has.Count.EqualTo(1));
            Assert.That(result.Errors[0].ErrorCode, Is.EqualTo("MaximumLengthValidator"));
        }));
    }

    #endregion

    #region AmountRules

    [Test]
    public async Task ValidateAmount_Failure_Negative()
    {
        // arrange
        var payment = InitializePayment();
        payment.Amount = -1;

        // act
        var result = await _paymentValidator.ValidateAsync(payment);

        // assert
        Assert.Multiple((() => {
            Assert.That(!result.IsValid);
            Assert.That(result.Errors, Has.Count.EqualTo(1));
            Assert.That(result.Errors[0].ErrorCode, Is.EqualTo("GreaterThanValidator"));
        }));
    }

    #endregion

    #region DateTimeToRules

    [Test]
    public async Task ValidateDateTimeTo_Failure_Earlier_Than_From()
    {
        // arrange
        var payment = InitializePayment();
        payment.DateTimeFrom = DateTime.Today;
        payment.DateTimeTo = DateTime.Today.AddDays(-1);
        
        // act
        var result = await _paymentValidator.ValidateAsync(payment);

        // assert
        Assert.Multiple((() => {
            Assert.That(!result.IsValid);
            Assert.That(result.Errors, Has.Count.EqualTo(1));
            Assert.That(result.Errors[0].ErrorCode, Is.EqualTo("GreaterThanValidator"));
        }));
    }

    #endregion
}