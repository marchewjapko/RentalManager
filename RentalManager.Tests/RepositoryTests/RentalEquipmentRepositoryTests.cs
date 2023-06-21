using Microsoft.EntityFrameworkCore;
using RentalManager.Core.Domain;
using RentalManager.Infrastructure.Repositories;

namespace RentalManager.Tests.RepositoryTests;

public class RentalEquipmentRepositoryTests
{
    private AppDbContext _appDbContext = null!;
    private RentalEquipmentRepository _rentalEquipmentRepository = null!;

    [SetUp]
    public void Setup()
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase("TestingDatabase");
        _appDbContext = new AppDbContext(optionsBuilder.Options);
        _rentalEquipmentRepository = new RentalEquipmentRepository(_appDbContext);

        Assume.That(_appDbContext.RentalEquipment.Count(), Is.EqualTo(0));
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
        var newRentalEquipment = new RentalEquipment
        {
            Name = "Test Name",
            Price = 100
        };
        var result = await _rentalEquipmentRepository.AddAsync(newRentalEquipment);
        Assert.That(result.Id, Is.EqualTo(1));
    }

    [Test]
    public async Task ShouldDelete()
    {
        var newRentalEquipment = new RentalEquipment
        {
            Name = "Test Name",
            Price = 100
        };
        _appDbContext.Add(newRentalEquipment);
        await _appDbContext.SaveChangesAsync();
        Assert.That(_appDbContext.RentalEquipment.Count(), Is.EqualTo(1));
        await _rentalEquipmentRepository.DeleteAsync(1);
        Assert.That(_appDbContext.RentalEquipment.Count(), Is.EqualTo(0));
    }

    [Test]
    public void ShouldNotDelete()
    {
        var ex = Assert.ThrowsAsync<Exception>(async () => await _rentalEquipmentRepository.DeleteAsync(1));
        Assert.That(ex.Message, Is.EqualTo("Unable to find rental equipment"));
    }

    [Test]
    public async Task ShouldGet()
    {
        var newRentalEquipment = new RentalEquipment
        {
            Name = "Test Name",
            Price = 100
        };
        _appDbContext.Add(newRentalEquipment);
        await _appDbContext.SaveChangesAsync();
        var result = await _rentalEquipmentRepository.GetAsync(1);
        Assert.That(result.Id, Is.EqualTo(1));
    }

    [Test]
    public void ShouldNotGetRentalEquipment()
    {
        var ex = Assert.ThrowsAsync<Exception>(async () => await _rentalEquipmentRepository.GetAsync(1));
        Assert.That(ex.Message, Is.EqualTo("Unable to find rental equipment"));
    }

    [Test]
    public async Task ShouldBrowseAll()
    {
        var newRentalEquipment1 = new RentalEquipment
        {
            Name = "Test Name 1",
            Price = 100
        };
        var newRentalEquipment2 = new RentalEquipment
        {
            Name = "Test Name 2",
            Price = 200
        };
        _appDbContext.Add(newRentalEquipment1);
        _appDbContext.Add(newRentalEquipment2);
        await _appDbContext.SaveChangesAsync();
        var result = await _rentalEquipmentRepository.BrowseAllAsync();
        Assert.That(result.Count(), Is.EqualTo(2));
    }

    [Test]
    public async Task ShouldFilter_byName()
    {
        var newRentalEquipment1 = new RentalEquipment
        {
            Name = "Test Name 1",
            Price = 100
        };
        var newRentalEquipment2 = new RentalEquipment
        {
            Name = "Test Name 2",
            Price = 200
        };
        _appDbContext.Add(newRentalEquipment1);
        _appDbContext.Add(newRentalEquipment2);
        await _appDbContext.SaveChangesAsync();
        var result = (await _rentalEquipmentRepository.BrowseAllAsync("Test Name 1")).ToList();
        Assert.Multiple(() =>
        {
            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result[0].Name, Is.EqualTo("Test Name 1"));
        });
    }

    [Test]
    public async Task ShouldUpdate()
    {
        var newRentalEquipment = new RentalEquipment
        {
            Name = "Test Name",
            Price = 100
        };
        _appDbContext.Add(newRentalEquipment);
        await _appDbContext.SaveChangesAsync();
        newRentalEquipment.Name = "NEW TEST NAME";
        await _rentalEquipmentRepository.UpdateAsync(newRentalEquipment, 1);
        var updatedRentalEquipment = _appDbContext.RentalEquipment.First();
        Assert.That(updatedRentalEquipment.Name, Is.EqualTo("NEW TEST NAME"));
    }
}