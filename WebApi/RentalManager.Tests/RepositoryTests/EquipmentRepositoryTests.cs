using Bogus;
using Microsoft.EntityFrameworkCore;
using RentalManager.Core.Domain;
using RentalManager.Global.Queries;
using RentalManager.Infrastructure.ExceptionHandling.Exceptions;
using RentalManager.Infrastructure.Repositories;
using RentalManager.Infrastructure.Repositories.DbContext;

namespace RentalManager.Tests.RepositoryTests;

public class EquipmentRepositoryTests
{
    private AppDbContext _appDbContext = null!;

    private EquipmentRepository _equipmentRepository = null!;

    private Equipment MockEquipment { get; set; }

    [SetUp]
    public void Setup()
    {
        MockEquipment = new Faker<Equipment>()
            .RuleFor(x => x.Id, () => 1)
            .RuleFor(x => x.Name, f => f.Commerce.ProductName())
            .RuleFor(x => x.Price, f => f.Random.Int(1, 100))
            .Generate();

        var optionsBuilder =
            new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase("TestingDatabase");
        _appDbContext = new AppDbContext(optionsBuilder.Options);
        _equipmentRepository = new EquipmentRepository(_appDbContext);

        Assume.That(_appDbContext.Equipments.Count(), Is.EqualTo(0));
    }

    [TearDown]
    public void TearDown()
    {
        _appDbContext.Database.EnsureDeleted();
        _appDbContext.Dispose();
    }

    [Test]
    public async Task ShouldAdd()
    {
        // Act
        await _equipmentRepository.AddAsync(MockEquipment);

        // Assert
        Assert.That(_appDbContext.Equipments.Count(), Is.EqualTo(1));
    }

    [Test]
    public async Task ShouldDelete()
    {
        // Arrange
        _appDbContext.Add(MockEquipment);
        await _appDbContext.SaveChangesAsync();

        Assume.That(_appDbContext.Equipments.Count(), Is.EqualTo(1));

        // Act
        await _equipmentRepository.DeleteAsync(1);

        // Assert
        Assert.That(_appDbContext.Equipments.Count(), Is.EqualTo(0));
    }

    [Test]
    public void ShouldNotDelete()
    {
        Assert.ThrowsAsync<EquipmentNotFoundException>(async () =>
            await _equipmentRepository.DeleteAsync(1));
    }

    [Test]
    public async Task ShouldGet()
    {
        // Arrange
        _appDbContext.Add(MockEquipment);
        await _appDbContext.SaveChangesAsync();

        Assume.That(_appDbContext.Equipments.Count(), Is.EqualTo(1));

        // Act
        var result = await _equipmentRepository.GetAsync(1);

        // Assert
        Assert.That(result.Id, Is.EqualTo(1));
    }

    [Test]
    public async Task ShouldGetAll()
    {
        // Arrange
        var newEquipment = new Faker<Equipment>()
            .RuleFor(x => x.Id, () => 2)
            .RuleFor(x => x.Name, f => f.Commerce.ProductName())
            .RuleFor(x => x.Price, f => f.Random.Int(1, 100))
            .Generate();

        _appDbContext.Add(MockEquipment);
        _appDbContext.Add(newEquipment);
        await _appDbContext.SaveChangesAsync();

        Assume.That(_appDbContext.Equipments.Count(), Is.EqualTo(2));

        // Act
        var result = await _equipmentRepository.GetAsync([1, 2]);

        // Assert
        Assert.That(result.Count(), Is.EqualTo(2));
    }

    [Test]
    public async Task ShouldNotGetAll()
    {
        // Arrange
        _appDbContext.Add(MockEquipment);
        await _appDbContext.SaveChangesAsync();

        // Assert
        var exception = Assert.ThrowsAsync<EquipmentNotFoundException>(async () =>
            await _equipmentRepository.GetAsync([1, 2]));
        Assert.That(exception.Message, Is.EqualTo("Equipment with ids: 2 not found"));
    }

    [Test]
    public void ShouldNotGetEquipment()
    {
        Assert.ThrowsAsync<EquipmentNotFoundException>(async () =>
            await _equipmentRepository.GetAsync(1));
    }

    [Test]
    public async Task ShouldBrowseAll()
    {
        // Arrange
        var newEquipment = new Faker<Equipment>()
            .RuleFor(x => x.Id, () => 2)
            .RuleFor(x => x.Name, f => f.Commerce.ProductName())
            .RuleFor(x => x.Price, f => f.Random.Int(1, 100))
            .Generate();

        _appDbContext.Add(MockEquipment);
        _appDbContext.Add(newEquipment);
        await _appDbContext.SaveChangesAsync();

        Assume.That(_appDbContext.Equipments.Count(), Is.EqualTo(2));
        Assume.That(_appDbContext.Equipments.First()
            .Id, Is.EqualTo(1));
        Assume.That(_appDbContext.Equipments.Skip(1)
            .First()
            .Id, Is.EqualTo(2));

        // Act
        var result = await _equipmentRepository.BrowseAllAsync(new QueryEquipment());

        // Assert
        Assert.That(result.Count(), Is.EqualTo(2));
    }

    [Test]
    public async Task ShouldFilter_byName()
    {
        // Arrange
        var newEquipment = new Faker<Equipment>()
            .RuleFor(x => x.Id, () => 2)
            .RuleFor(x => x.Name, f => f.Commerce.ProductName())
            .RuleFor(x => x.Price, f => f.Random.Int(1, 100))
            .Generate();

        _appDbContext.Add(MockEquipment);
        _appDbContext.Add(newEquipment);
        await _appDbContext.SaveChangesAsync();

        var query = new QueryEquipment
        {
            Name = newEquipment.Name
        };

        Assume.That(_appDbContext.Equipments.Count(), Is.EqualTo(2));
        Assume.That(_appDbContext.Equipments.First()
            .Id, Is.EqualTo(1));
        Assume.That(_appDbContext.Equipments.Skip(1)
            .First()
            .Id, Is.EqualTo(2));

        // Act
        var result = (await _equipmentRepository.BrowseAllAsync(query)).ToList();

        // Assert
        Assert.Multiple(() => {
            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result[0].Name, Is.EqualTo(newEquipment.Name));
        });
    }

    [Test]
    public async Task ShouldFilter_DateAddedBetween()
    {
        // Arrange
        var dateTimeSearchFrom = new DateTime(2020, 1, 1);
        var dateTimeSearchTo = new DateTime(2020, 1, 16);

        var dateTimeInRange = new DateTime(2020, 1, 10);
        var dateTimeOutsideRange = new DateTime(2020, 1, 31);

        var newEquipments = new Faker<Equipment>()
            .RuleFor(x => x.Id, f => f.UniqueIndex)
            .RuleFor(x => x.Name, f => f.Commerce.ProductName())
            .RuleFor(x => x.Price, f => f.Random.Int(1, 100))
            .RuleFor(x => x.CreatedTs, () => dateTimeInRange)
            .Generate(2);

        _appDbContext.Add(MockEquipment);
        _appDbContext.AddRange(newEquipments);
        await _appDbContext.SaveChangesAsync();

        var query = new QueryEquipment
        {
            AddedFrom = dateTimeSearchFrom,
            AddedTo = dateTimeSearchTo
        };

        Assume.That(_appDbContext.Equipments.Count(), Is.EqualTo(3));

        // Act
        var result = (await _equipmentRepository.BrowseAllAsync(query)).ToList();

        // Assert
        Assert.Multiple(() => {
            Assert.That(result, Has.Count.EqualTo(2));
            Assert.That(result.All(x => x.CreatedTs == dateTimeInRange), Is.True);
        });
    }

    [Test]
    public async Task ShouldUpdate()
    {
        // Arrange
        var newEquipment = new Faker<Equipment>()
            .RuleFor(x => x.Id, () => 1)
            .RuleFor(x => x.Name, f => f.Commerce.ProductName())
            .RuleFor(x => x.Price, f => f.Random.Int(1, 100))
            .Generate();
        _appDbContext.Add(newEquipment);
        await _appDbContext.SaveChangesAsync();
        newEquipment.Name = "NEW TEST NAME";

        Assume.That(_appDbContext.Equipments.Count(), Is.EqualTo(1));

        // Act
        await _equipmentRepository.UpdateAsync(newEquipment, 1);

        // Assert
        var updatedEquipment = _appDbContext.Equipments.First();
        Assert.That(updatedEquipment.Name, Is.EqualTo("NEW TEST NAME"));
    }

    [Test]
    public void ShouldNotUpdate_NotFound()
    {
        // Assert
        Assert.ThrowsAsync<EquipmentNotFoundException>(async () =>
            await _equipmentRepository.UpdateAsync(new Equipment(), 1));
    }

    [Test]
    public async Task ShouldDeactivate()
    {
        // Arrange
        _appDbContext.Add(MockEquipment);
        await _appDbContext.SaveChangesAsync();

        Assume.That(_appDbContext.Equipments.Count(), Is.EqualTo(1));

        // Act
        await _equipmentRepository.Deactivate(1);

        // Assert
        Assert.Multiple(() => {
            Assert.That(_appDbContext.Equipments.Count(), Is.EqualTo(1));
            Assert.That(_appDbContext.Equipments.First()
                .IsActive, Is.False);
        });
    }

    [Test]
    public void ShouldNotDeactivate_NotFound()
    {
        // Assert
        Assert.ThrowsAsync<EquipmentNotFoundException>(async () =>
            await _equipmentRepository.Deactivate(1));
    }
}