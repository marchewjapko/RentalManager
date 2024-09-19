using Bogus;
using FluentValidation.TestHelper;
using RentalManager.Infrastructure.Models.Commands.EquipmentCommands;
using RentalManager.Infrastructure.Validators.EquipmentValidators;

namespace RentalManager.Tests.ValidatorsTests;

public class EquipmentValidatorTests
{
    private readonly BaseEquipmentValidator _baseEquipmentValidator = new();
    private readonly CreateEquipmentValidator _createEquipmentValidator = new();
    private readonly UpdateEquipmentValidator _updateEquipmentValidator = new();

    private static BaseEquipmentCommand InitializeEquipment()
    {
        return new Faker<BaseEquipmentCommand>()
            .RuleFor(x => x.Name, f => f.Commerce.ProductName())
            .RuleFor(x => x.Price, f => f.Random.Int(1, 200))
            .Generate();
    }

    [Test]
    public async Task ValidateEquipment_Success()
    {
        // Arrange
        var equipment = InitializeEquipment();

        // Act
        var result = await _baseEquipmentValidator.TestValidateAsync(equipment);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    #region PriceRules

    [Test]
    public async Task ValidatePrice_Failure_Negative()
    {
        // Arrange
        var equipment = InitializeEquipment();
        equipment.Price = -1;

        // Act
        var result = await _baseEquipmentValidator.TestValidateAsync(equipment);

        // Assert
        result.ShouldHaveAnyValidationError();
        result.ShouldHaveValidationErrorFor(x => x.Price);

        Assert.Multiple(() => {
            Assert.That(result.Errors, Has.Count.EqualTo(1));
            Assert.That(result.Errors[0].ErrorCode, Is.EqualTo("GreaterThanValidator"));
        });
    }

    #endregion

    [Test]
    public async Task ValidateCreateEquipment_Success()
    {
        // Arrange
        var equipment = new Faker<CreateEquipmentCommand>()
            .RuleFor(x => x.Name, f => f.Commerce.ProductName())
            .RuleFor(x => x.Price, f => f.Random.Int(1, 200))
            .Generate();

        // Act
        var result = await _createEquipmentValidator.TestValidateAsync(equipment);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Test]
    public async Task ValidateUpdateEquipment_Success()
    {
        // Arrange
        var equipment = new Faker<UpdateEquipmentCommand>()
            .RuleFor(x => x.Name, f => f.Commerce.ProductName())
            .RuleFor(x => x.Price, f => f.Random.Int(1, 200))
            .Generate();

        // Act
        var result = await _updateEquipmentValidator.TestValidateAsync(equipment);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    #region NameRules

    [Test]
    public async Task ValidateName_Failure_Invalid_Character()
    {
        // Arrange
        var equipment = InitializeEquipment();
        equipment.Name = "Fun equipment mk4;";

        // Act
        var result = await _baseEquipmentValidator.TestValidateAsync(equipment);

        // Assert
        result.ShouldHaveAnyValidationError();
        result.ShouldHaveValidationErrorFor(x => x.Name);

        Assert.Multiple(() => {
            Assert.That(result.Errors, Has.Count.EqualTo(1));
            Assert.That(result.Errors[0].ErrorCode, Is.EqualTo("RegularExpressionValidator"));
        });
    }

    [Test]
    public async Task ValidateName_Failure_Too_Long()
    {
        // Arrange
        var equipment = InitializeEquipment();
        equipment.Name = new string('A', 101);

        // Act
        var result = await _baseEquipmentValidator.TestValidateAsync(equipment);

        // Assert
        result.ShouldHaveAnyValidationError();
        result.ShouldHaveValidationErrorFor(x => x.Name);

        Assert.Multiple(() => {
            Assert.That(result.Errors, Has.Count.EqualTo(1));
            Assert.That(result.Errors[0].ErrorCode, Is.EqualTo("MaximumLengthValidator"));
        });
    }

    #endregion
}