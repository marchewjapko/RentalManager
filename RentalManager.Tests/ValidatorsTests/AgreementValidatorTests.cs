using Bogus;
using RentalManager.Infrastructure.Commands.AgreementCommands;
using RentalManager.Infrastructure.Commands.PaymentCommands;
using RentalManager.Infrastructure.Validators;

namespace RentalManager.Tests.ValidatorsTests;

public class AgreementValidatorTests
{
    private readonly AgreementBaseValidator _agreementBaseValidator = new();

    private static AgreementBaseCommand InitializeAgreement()
    {
        return new Faker<AgreementBaseCommand>()
            .RuleFor(x => x.EmployeeId, () => 1)
            .RuleFor(x => x.IsActive, () => true)
            .RuleFor(x => x.ClientId, () => 1)
            .RuleFor(x => x.EquipmentIds, () => new List<int> { 1 })
            .RuleFor(x => x.Comment, f => f.Lorem.Sentence())
            .RuleFor(x => x.Deposit, f => f.Random.Int(1, 100))
            .RuleFor(x => x.TransportFromPrice, f => f.Random.Int(1, 100))
            .RuleFor(x => x.TransportToPrice, f => f.Random.Int(1, 100))
            .RuleFor(x => x.DateAdded, () => DateTime.Today)
            .RuleFor(x => x.Payments, () => new List<CreatePayment>())
            .Generate();
    }

    [Test]
    public async Task ValidateAgreement_Success()
    {
        // arrange
        var agreement = InitializeAgreement();

        // act
        var result = await _agreementBaseValidator.ValidateAsync(agreement);

        // assert
        Assert.That(result.IsValid);
    }
}