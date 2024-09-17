using Bogus;
using FluentValidation.TestHelper;
using RentalManager.Infrastructure.Models.Commands.AgreementCommands;
using RentalManager.Infrastructure.Validators.AgreementValidators;

namespace RentalManager.Tests.ValidatorsTests;

public class AgreementValidatorTests
{
    private readonly AgreementBaseValidator _agreementBaseValidator = new();

    private static AgreementBaseCommand InitializeAgreement()
    {
        return new Faker<AgreementBaseCommand>()
            .RuleFor(x => x.UserId, () => 1)
            .RuleFor(x => x.IsActive, () => true)
            .RuleFor(x => x.ClientId, f => f.Random.Int())
            .RuleFor(x => x.EquipmentsIds, f => new List<int>()
            {
                f.Random.Int(),
                f.Random.Int(),
            })
            .RuleFor(x => x.Comment, f => f.Lorem.Sentence())
            .RuleFor(x => x.Deposit, f => f.Random.Int(1, 100))
            .RuleFor(x => x.TransportFromPrice, f => f.Random.Int(1, 100))
            .RuleFor(x => x.TransportToPrice, f => f.Random.Int(1, 100))
            .RuleFor(x => x.DateAdded, () => DateTime.Today)
            .Generate();
    }

    [Test]
    public async Task ValidateAgreement_Success()
    {
        // Arrange
        var agreement = InitializeAgreement();

        // Act
        var result = await _agreementBaseValidator.TestValidateAsync(agreement);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
}