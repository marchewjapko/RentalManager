using Bogus;
using FluentValidation.TestHelper;
using RentalManager.Infrastructure.Models.Commands.AgreementCommands;
using RentalManager.Infrastructure.Validators.AgreementValidators;

namespace RentalManager.Tests.ValidatorsTests;

public class AgreementValidatorTests
{
    private readonly BaseAgreementValidator _baseAgreementValidator = new();
    private readonly CreateAgreementValidator _createAgreementValidator = new();
    private readonly UpdateAgreementValidator _updateAgreementValidator = new();

    [Test]
    public async Task ValidateAgreement_Success()
    {
        // Arrange
        var agreement = new Faker<BaseAgreementCommand>()
            .RuleFor(x => x.UserId, () => 1)
            .RuleFor(x => x.IsActive, () => true)
            .RuleFor(x => x.ClientId, f => f.Random.Int())
            .RuleFor(x => x.EquipmentsIds, f => new List<int>
            {
                f.Random.Int(),
                f.Random.Int()
            })
            .RuleFor(x => x.Comment, f => f.Lorem.Sentence())
            .RuleFor(x => x.Deposit, f => f.Random.Int(1, 100))
            .RuleFor(x => x.TransportFromPrice, f => f.Random.Int(1, 100))
            .RuleFor(x => x.TransportToPrice, f => f.Random.Int(1, 100))
            .RuleFor(x => x.DateAdded, () => DateTime.Today)
            .Generate();

        // Act
        var result = await _baseAgreementValidator.TestValidateAsync(agreement);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Test]
    public async Task ValidateCreateAgreement_Success()
    {
        // Arrange
        var agreement = new Faker<CreateAgreementCommand>()
            .RuleFor(x => x.EquipmentsIds, f => f.Make(5, () => f.Random.Int()))
            .RuleFor(x => x.UserId, f => f.Random.Int())
            .RuleFor(x => x.ClientId, f => f.Random.Int())
            .RuleFor(x => x.Deposit, f => f.Random.Int(1, 100))
            .RuleFor(x => x.TransportToPrice, f => f.Random.Int(1, 100))
            .Generate();

        // Act
        var result = await _createAgreementValidator.TestValidateAsync(agreement);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Test]
    public async Task ValidateUpdateAgreement_Success()
    {
        // Arrange
        var agreement = new Faker<UpdateAgreementCommand>()
            .RuleFor(x => x.UserId, () => 1)
            .RuleFor(x => x.IsActive, () => true)
            .RuleFor(x => x.ClientId, f => f.Random.Int())
            .RuleFor(x => x.EquipmentsIds, f => new List<int>
            {
                f.Random.Int(),
                f.Random.Int()
            })
            .RuleFor(x => x.Comment, f => f.Lorem.Sentence())
            .RuleFor(x => x.Deposit, f => f.Random.Int(1, 100))
            .RuleFor(x => x.TransportFromPrice, f => f.Random.Int(1, 100))
            .RuleFor(x => x.TransportToPrice, f => f.Random.Int(1, 100))
            .RuleFor(x => x.DateAdded, () => DateTime.Today)
            .Generate();

        // Act
        var result = await _updateAgreementValidator.TestValidateAsync(agreement);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
}