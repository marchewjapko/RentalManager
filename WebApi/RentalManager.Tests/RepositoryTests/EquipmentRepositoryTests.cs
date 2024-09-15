﻿using Bogus;
using Microsoft.EntityFrameworkCore;
using RentalManager.Core.Domain;
using RentalManager.Global.Queries;
using RentalManager.Infrastructure.Exceptions;
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
        // act
        await _equipmentRepository.AddAsync(MockEquipment);

        // assert
        Assert.That(_appDbContext.Equipments.Count(), Is.EqualTo(1));
    }

    [Test]
    public async Task ShouldDelete()
    {
        // arrange
        _appDbContext.Add(MockEquipment);
        await _appDbContext.SaveChangesAsync();

        Assume.That(_appDbContext.Equipments.Count(), Is.EqualTo(1));

        // act
        await _equipmentRepository.DeleteAsync(1);

        // assert
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
        // arrange
        _appDbContext.Add(MockEquipment);
        await _appDbContext.SaveChangesAsync();

        Assume.That(_appDbContext.Equipments.Count(), Is.EqualTo(1));

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

        // act
        var result = await _equipmentRepository.BrowseAllAsync(new QueryEquipment());

        // assert
        Assert.That(result.Count(), Is.EqualTo(2));
    }

    [Test]
    public async Task ShouldFilter_byName()
    {
        // arrange
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

        // act
        var result = (await _equipmentRepository.BrowseAllAsync(query)).ToList();

        // assert
        Assert.Multiple(() => {
            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result[0].Name, Is.EqualTo(newEquipment.Name));
        });
    }

    [Test]
    public async Task ShouldUpdate()
    {
        // arrange
        var newEquipment = new Faker<Equipment>()
            .RuleFor(x => x.Id, () => 1)
            .RuleFor(x => x.Name, f => f.Commerce.ProductName())
            .RuleFor(x => x.Price, f => f.Random.Int(1, 100))
            .Generate();
        _appDbContext.Add(newEquipment);
        await _appDbContext.SaveChangesAsync();
        newEquipment.Name = "NEW TEST NAME";

        Assume.That(_appDbContext.Equipments.Count(), Is.EqualTo(1));

        // act
        await _equipmentRepository.UpdateAsync(newEquipment, 1);

        // assert
        var updatedEquipment = _appDbContext.Equipments.First();
        Assert.That(updatedEquipment.Name, Is.EqualTo("NEW TEST NAME"));
    }

    [Test]
    public async Task ShouldDeactivate()
    {
        // arrange
        _appDbContext.Add(MockEquipment);
        await _appDbContext.SaveChangesAsync();

        Assume.That(_appDbContext.Equipments.Count(), Is.EqualTo(1));

        // act
        await _equipmentRepository.Deactivate(1);

        // assert
        Assert.Multiple(() => {
            Assert.That(_appDbContext.Equipments.Count(), Is.EqualTo(1));
            Assert.That(_appDbContext.Equipments.First()
                .IsActive, Is.False);
        });
    }
}