using Bogus;
using Microsoft.AspNetCore.Http;
using RentalManager.Infrastructure.Commands.EquipmentCommands;
using RentalManager.Infrastructure.Validators;

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
        var result = await _equipmentBaseValidator.ValidateAsync(equipment);

        // assert
        Assert.That(result.IsValid);
    }

    #region PriceRules

    [Test]
    public async Task ValidatePrice_Failure_Negative()
    {
        // arrange
        var equipment = InitializeEquipment();
        equipment.Price = -1;

        // act
        var result = await _equipmentBaseValidator.ValidateAsync(equipment);

        // assert
        Assert.Multiple(() => {
            Assert.That(!result.IsValid);
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
        var result = await _equipmentBaseValidator.ValidateAsync(equipment);

        // assert
        Assert.Multiple(() => {
            Assert.That(!result.IsValid);
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
        var result = await _equipmentBaseValidator.ValidateAsync(equipment);

        // assert
        Assert.Multiple(() => {
            Assert.That(!result.IsValid);
            Assert.That(result.Errors, Has.Count.EqualTo(1));
            Assert.That(result.Errors[0].ErrorCode, Is.EqualTo("MaximumLengthValidator"));
        });
    }

    #endregion

    #region ImageRules

    [Test]
    public async Task ValidateFile_Success()
    {
        // arrange

        const string content = "file-content";
        const string fileName = "test-file.png";
        var stream = new MemoryStream();
        var writer = new StreamWriter(stream);
        await writer.WriteAsync(content);
        await writer.FlushAsync();
        stream.Position = 0;

        var employee = InitializeEquipment();
        employee.Image = new FormFile(stream, 0, stream.Length, "id_from_form", fileName);

        // act
        var result = await _equipmentBaseValidator.ValidateAsync(employee);

        //assert
        Assert.That(result.IsValid);
    }

    [Test]
    public async Task ValidateFile_Failure_Wrong_Extension()
    {
        // arrange
        const string content = "file-content";
        const string fileName = "test-file.txt";
        var stream = new MemoryStream();
        var writer = new StreamWriter(stream);
        await writer.WriteAsync(content);
        await writer.FlushAsync();
        stream.Position = 0;

        var employee = InitializeEquipment();
        employee.Image = new FormFile(stream, 0, stream.Length, "id_from_form", fileName);

        // act
        var result = await _equipmentBaseValidator.ValidateAsync(employee);

        //assert
        Assert.Multiple(() => {
            Assert.That(!result.IsValid);
            Assert.That(result.Errors, Has.Count.EqualTo(1));
            Assert.That(result.Errors[0].ErrorMessage, Does.Contain("Unacceptable extension"));
        });
    }

    [Test]
    public async Task ValidateFile_Failure_Too_Large()
    {
        // arrange
        var content = new string('A', 1024 * 1024 + 1);
        const string fileName = "test-file.png";
        var stream = new MemoryStream();
        var writer = new StreamWriter(stream);
        await writer.WriteAsync(content);
        await writer.FlushAsync();
        stream.Position = 0;

        var employee = InitializeEquipment();
        employee.Image = new FormFile(stream, 0, stream.Length, "id_from_form", fileName);

        // act
        var result = await _equipmentBaseValidator.ValidateAsync(employee);

        //assert
        Assert.Multiple(() => {
            Assert.That(!result.IsValid);
            Assert.That(result.Errors, Has.Count.EqualTo(1));
            Assert.That(result.Errors[0].ErrorMessage, Does.Contain("File too large"));
        });
    }

    #endregion
}