using Bogus;
using FluentValidation.TestHelper;
using RentalManager.Infrastructure.Models.Commands.EquipmentCommands;
using RentalManager.Infrastructure.Validators.EquipmentValidators;

namespace RentalManager.Tests.ValidatorsTests;

public class EquipmentValidatorTests
{
    private readonly EquipmentBaseValidator _equipmentBaseValidator = new();

    private static EquipmentBaseCommand InitializeEquipment()
    {
        return new Faker<EquipmentBaseCommand>()
            .RuleFor(x => x.Name, f => f.Commerce.ProductName())
            .RuleFor(x => x.Price, f => f.Random.Int(1, 200))
            .Generate();
    }

    [Test]
    public async Task ValidateEquipment_Success()
    {
        // arrange
        var equipment = InitializeEquipment();

        // act
        var result = await _equipmentBaseValidator.TestValidateAsync(equipment);

        // assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    #region PriceRules

    [Test]
    public async Task ValidatePrice_Failure_Negative()
    {
        // arrange
        var equipment = InitializeEquipment();
        equipment.Price = -1;

        // act
        var result = await _equipmentBaseValidator.TestValidateAsync(equipment);

        // assert
        result.ShouldHaveAnyValidationError();
        result.ShouldHaveValidationErrorFor(x => x.Price);

        Assert.Multiple(() => {
            Assert.That(result.Errors, Has.Count.EqualTo(1));
            Assert.That(result.Errors[0].ErrorCode, Is.EqualTo("GreaterThanValidator"));
        });
    }

    #endregion

    #region NameRules

    [Test]
    public async Task ValidateName_Failure_Invalid_Character()
    {
        // arrange
        var equipment = InitializeEquipment();
        equipment.Name = "Fun equipment mk4;";

        // act
        var result = await _equipmentBaseValidator.TestValidateAsync(equipment);

        // assert
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
        // arrange
        var equipment = InitializeEquipment();
        equipment.Name = new string('A', 101);

        // act
        var result = await _equipmentBaseValidator.TestValidateAsync(equipment);

        // assert
        result.ShouldHaveAnyValidationError();
        result.ShouldHaveValidationErrorFor(x => x.Name);

        Assert.Multiple(() => {
            Assert.That(result.Errors, Has.Count.EqualTo(1));
            Assert.That(result.Errors[0].ErrorCode, Is.EqualTo("MaximumLengthValidator"));
        });
    }

    #endregion
}