using Microsoft.EntityFrameworkCore;
using RentalManager.Core.Domain;
using RentalManager.Infrastructure.Exceptions;
using RentalManager.Infrastructure.Repositories;

namespace RentalManager.Tests.RepositoryTests;

public class EquipmentRepositoryTests
{
    private AppDbContext _appDbContext = null!;
    private EquipmentRepository _equipmentRepository = null!;

    [SetUp]
    public void Setup()
    {
        var optionsBuilder =
            new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase("TestingDatabase");
        _appDbContext = new AppDbContext(optionsBuilder.Options);
        _equipmentRepository = new EquipmentRepository(_appDbContext);

        Assume.That(_appDbContext.Equipment.Count(), Is.EqualTo(0));
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
        // arrange
        var newEquipment = new Equipment
        {
            Name = "Test Name",
            Price = 100
        };

        // act
        var result = await _equipmentRepository.AddAsync(newEquipment);

        // assert
        Assert.That(result.Id, Is.EqualTo(1));
    }

    [Test]
    public async Task ShouldDelete()
    {
        // arrange
        var newEquipment = new Equipment
        {
            Name = "Test Name",
            Price = 100
        };
        _appDbContext.Add(newEquipment);
        await _appDbContext.SaveChangesAsync();
        Assume.That(_appDbContext.Equipment.Count(), Is.EqualTo(1));

        // act
        await _equipmentRepository.DeleteAsync(1);

        // assert
        Assert.That(_appDbContext.Equipment.Count(), Is.EqualTo(0));
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
        // arrange
        var newEquipment = new Equipment
        {
            Name = "Test Name",
            Price = 100
        };
        _appDbContext.Add(newEquipment);
        await _appDbContext.SaveChangesAsync();

        // act
        var result = await _equipmentRepository.GetAsync(1);

        // assert
        Assert.That(result.Id, Is.EqualTo(1));
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
        // arrange
        var newEquipment1 = new Equipment
        {
            Name = "Test Name 1",
            Price = 100
        };
        var newEquipment2 = new Equipment
        {
            Name = "Test Name 2",
            Price = 200
        };
        _appDbContext.Add(newEquipment1);
        _appDbContext.Add(newEquipment2);
        await _appDbContext.SaveChangesAsync();

        // act
        var result = await _equipmentRepository.BrowseAllAsync();

        // assert
        Assert.That(result.Count(), Is.EqualTo(2));
    }

    [Test]
    public async Task ShouldFilter_byName()
    {
        // arrange
        var newEquipment1 = new Equipment
        {
            Name = "Test Name 1",
            Price = 100
        };
        var newEquipment2 = new Equipment
        {
            Name = "Test Name 2",
            Price = 200
        };
        _appDbContext.Add(newEquipment1);
        _appDbContext.Add(newEquipment2);
        await _appDbContext.SaveChangesAsync();

        // act
        var result = (await _equipmentRepository.BrowseAllAsync("Test Name 1")).ToList();

        // assert
        Assert.Multiple(() => {
            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result[0].Name, Is.EqualTo("Test Name 1"));
        });
    }

    [Test]
    public async Task ShouldUpdate()
    {
        // arrange
        var newEquipment = new Equipment
        {
            Name = "Test Name",
            Price = 100
        };
        _appDbContext.Add(newEquipment);
        await _appDbContext.SaveChangesAsync();
        newEquipment.Name = "NEW TEST NAME";

        // act
        await _equipmentRepository.UpdateAsync(newEquipment, 1);

        // assert
        var updatedEquipment = _appDbContext.Equipment.First();
        Assert.That(updatedEquipment.Name, Is.EqualTo("NEW TEST NAME"));
    }
}