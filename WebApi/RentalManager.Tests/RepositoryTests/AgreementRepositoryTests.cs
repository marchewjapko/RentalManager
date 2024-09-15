﻿using Bogus;
using Microsoft.EntityFrameworkCore;
using RentalManager.Core.Domain;
using RentalManager.Global.Queries;
using RentalManager.Infrastructure.Exceptions;
using RentalManager.Infrastructure.Repositories;
using RentalManager.Infrastructure.Repositories.DbContext;

namespace RentalManager.Tests.RepositoryTests;

public class AgreementRepositoryTests
{
    private AgreementRepository _agreementRepository = null!;

    private AppDbContext _appDbContext = null!;

    private Client MockClient { get; set; }

    private Equipment MockEquipment { get; set; }

    private Agreement MockAgreement { get; set; }

    [SetUp]
    public void Setup()
    {
        MockClient = new Faker<Client>()
            .RuleFor(x => x.Id, () => 1)
            .RuleFor(x => x.FirstName, f => f.Name.FirstName())
            .RuleFor(x => x.LastName, f => f.Name.LastName())
            .RuleFor(x => x.City, f => f.Address.City())
            .RuleFor(x => x.Street, f => f.Address.StreetName())
            .RuleFor(x => x.IdCard, () => "ABC 123456")
            .RuleFor(x => x.PhoneNumber, f => f.Phone.PhoneNumber("###-###-###"))
            .Generate();

        MockEquipment = new Faker<Equipment>()
            .RuleFor(x => x.Id, () => 1)
            .RuleFor(x => x.Name, f => f.Commerce.ProductName())
            .RuleFor(x => x.Price, f => f.Random.Int(1, 200))
            .RuleFor(x => x.Agreements, () => new List<Agreement>())
            .Generate();

        MockAgreement = new Faker<Agreement>()
            .RuleFor(x => x.Id, () => 1)
            .RuleFor(x => x.UserId, f => f.Random.Int(1, 100))
            .RuleFor(x => x.ClientId, () => MockClient.Id)
            .RuleFor(x => x.Client, () => MockClient)
            .RuleFor(x => x.Comment, f => f.Lorem.Sentence())
            .RuleFor(x => x.Deposit, f => f.Random.Int(1, 100))
            .RuleFor(x => x.TransportFromPrice, () => null)
            .RuleFor(x => x.TransportToPrice, f => f.Random.Int(1, 100))
            .RuleFor(x => x.DateAdded, () => DateTime.Now)
            .RuleFor(x => x.Equipments, () => new List<Equipment> { MockEquipment })
            .RuleFor(x => x.Payments, () => new List<Payment>())
            .Generate();

        var guid = Guid.NewGuid()
            .ToString();

        _appDbContext = new AppDbContext(new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(guid)
            .Options);

        _agreementRepository = new AgreementRepository(_appDbContext);

        _appDbContext.Clients.Add(MockClient);
        _appDbContext.Equipments.Add(MockEquipment);
        _appDbContext.SaveChanges();

        Assume.That(_appDbContext.Agreements.Count(), Is.EqualTo(0));
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
        await _agreementRepository.AddAsync(MockAgreement);

        // assert
        Assert.That(_appDbContext.Agreements.Count(), Is.EqualTo(1));
    }

    [Test]
    public async Task ShouldGet()
    {
        // arrange
        await _agreementRepository.AddAsync(MockAgreement);
        await _appDbContext.SaveChangesAsync();
        Assume.That(_appDbContext.Agreements.Count(), Is.EqualTo(1));

        // act
        var result = await _agreementRepository.GetAsync(1);

        // assert
        Assert.Multiple(() => {
            Assert.That(result.Id, Is.EqualTo(MockAgreement.Id));
            Assert.That(result.UserId, Is.EqualTo(MockAgreement.UserId));
            Assert.That(result.Client, Is.Not.Null);
            Assert.That(result.Equipments, Has.Count.EqualTo(1));
            Assert.That(result.Deposit, Is.EqualTo(MockAgreement.Deposit));
            Assert.That(result.TransportToPrice, Is.EqualTo(MockAgreement.TransportToPrice));
        });
    }

    [Test]
    public void ShouldNotGet()
    {
        // assert
        Assert.ThrowsAsync<AgreementNotFoundException>(async () =>
            await _agreementRepository.GetAsync(1));
    }

    [Test]
    public async Task ShouldDelete()
    {
        // arrange
        _appDbContext.Agreements.Add(MockAgreement);
        await _appDbContext.SaveChangesAsync();

        Assume.That(_appDbContext.Agreements.Count(), Is.EqualTo(1));

        // act
        await _agreementRepository.DeleteAsync(1);

        // assert
        Assert.That(_appDbContext.Agreements.Count(), Is.EqualTo(0));
    }

    [Test]
    public void ShouldNotDelete()
    {
        // assert
        Assert.ThrowsAsync<AgreementNotFoundException>(async () =>
            await _agreementRepository.DeleteAsync(1));
    }

    [Test]
    public async Task ShouldBrowseAll()
    {
        // arrange
        var newAgreement = new Faker<Agreement>()
            .RuleFor(x => x.Id, () => 2)
            .RuleFor(x => x.UserId, f => f.Random.Int(1, 100))
            .RuleFor(x => x.ClientId, () => MockClient.Id)
            .RuleFor(x => x.Client, () => MockClient)
            .RuleFor(x => x.Comment, f => f.Lorem.Sentence())
            .RuleFor(x => x.Deposit, f => f.Random.Int(1, 100))
            .RuleFor(x => x.TransportFromPrice, () => null)
            .RuleFor(x => x.TransportToPrice, f => f.Random.Int(1, 100))
            .RuleFor(x => x.DateAdded, () => DateTime.Now)
            .RuleFor(x => x.Equipments, () => new List<Equipment>())
            .RuleFor(x => x.Payments, () => new List<Payment>())
            .Generate();

        _appDbContext.Agreements.Add(MockAgreement);
        _appDbContext.Agreements.Add(newAgreement);
        await _appDbContext.SaveChangesAsync();

        Assume.That(_appDbContext.Agreements.Count(), Is.EqualTo(2));

        // act
        var result = await _agreementRepository.BrowseAllAsync(new QueryAgreements());

        // assert
        Assert.That(result.Count(), Is.EqualTo(2));
    }

    [Test]
    public async Task ShouldFilter()
    {
        // arrange
        var newClient = new Faker<Client>()
            .RuleFor(x => x.FirstName, f => f.Name.FirstName())
            .RuleFor(x => x.LastName, f => f.Name.LastName())
            .RuleFor(x => x.City, () => MockClient.City)
            .RuleFor(x => x.Street, f => f.Address.StreetName())
            .RuleFor(x => x.IdCard, () => "ABC 123456")
            .RuleFor(x => x.PhoneNumber, f => f.Phone.PhoneNumber("###-###-###"))
            .Generate();

        var newEquipment = new Faker<Equipment>()
            .RuleFor(x => x.Name, f => f.Commerce.ProductName())
            .RuleFor(x => x.Price, f => f.Random.Int(1, 200))
            .Generate();

        var clientEntry = _appDbContext.Clients.Add(newClient)
            .Entity;
        var equipmentEntry = _appDbContext.Equipments.Add(newEquipment)
            .Entity;

        var newAgreement = new Faker<Agreement>()
            .RuleFor(x => x.Id, () => 2)
            .RuleFor(x => x.UserId, f => f.Random.Int(1, 100))
            .RuleFor(x => x.ClientId, () => clientEntry.Id)
            .RuleFor(x => x.Client, () => clientEntry)
            .RuleFor(x => x.Comment, f => f.Lorem.Sentence())
            .RuleFor(x => x.Deposit, f => f.Random.Int(1, 100))
            .RuleFor(x => x.TransportFromPrice, () => null)
            .RuleFor(x => x.TransportToPrice, f => f.Random.Int(1, 100))
            .RuleFor(x => x.DateAdded, () => DateTime.Now)
            .RuleFor(x => x.Equipments, () => new List<Equipment> { equipmentEntry })
            .RuleFor(x => x.Payments, () => new List<Payment>())
            .Generate();

        _appDbContext.Agreements.Add(MockAgreement);
        _appDbContext.Agreements.Add(newAgreement);
        await _appDbContext.SaveChangesAsync();
        Assume.That(_appDbContext.Agreements.Count(), Is.EqualTo(2));

        var query1 = new QueryAgreements
        {
            Surname = newAgreement.Client.LastName
        };

        var query2 = new QueryAgreements
        {
            City = MockClient.City
        };

        // act
        var result1 = (await _agreementRepository.BrowseAllAsync(query1)).ToList();
        var result2 = (await _agreementRepository.BrowseAllAsync(query2)).ToList();

        // assert
        Assert.Multiple(() => {
            Assert.That(result1, Has.Count.EqualTo(1));
            Assert.That(result1.First()
                .UserId, Is.EqualTo(newAgreement.UserId));
            Assert.That(result2, Has.Count.EqualTo(2));
            Assert.That(result2.First()
                .Client.City, Is.EqualTo(MockAgreement.Client.City));
            Assert.That(result2.Skip(1)
                .First()
                .Client.City, Is.EqualTo(newAgreement.Client.City));
        });
    }

    [Test]
    public async Task ShouldUpdate()
    {
        // arrange
        var newAgreement = new Faker<Agreement>()
            .RuleFor(x => x.Id, () => 1)
            .RuleFor(x => x.UserId, () => MockAgreement.UserId)
            .RuleFor(x => x.ClientId, () => MockClient.Id)
            .RuleFor(x => x.Client, () => MockClient)
            .RuleFor(x => x.Comment, f => f.Lorem.Sentence())
            .RuleFor(x => x.Deposit, f => f.Random.Int(1, 100))
            .RuleFor(x => x.TransportFromPrice, () => null)
            .RuleFor(x => x.TransportToPrice, f => f.Random.Int(1, 100))
            .RuleFor(x => x.DateAdded, () => DateTime.Now)
            .RuleFor(x => x.Equipments, () => new List<Equipment> { MockEquipment })
            .RuleFor(x => x.Payments, () => new List<Payment>())
            .Generate();

        _appDbContext.Add(newAgreement);
        await _appDbContext.SaveChangesAsync();
        newAgreement.Comment = "NEW COMMENT";

        Assume.That(_appDbContext.Clients.Count(), Is.EqualTo(1));

        // act
        await _agreementRepository.UpdateAsync(newAgreement, 1);

        // assert
        var updatedAgreement = _appDbContext.Agreements.First();
        Assert.That(updatedAgreement.Comment, Is.EqualTo("NEW COMMENT"));
    }

    [Test]
    public async Task ShouldDeactivate()
    {
        // arrange
        var newAgreement = new Faker<Agreement>()
            .RuleFor(x => x.Id, () => 2)
            .RuleFor(x => x.UserId, () => MockAgreement.UserId)
            .RuleFor(x => x.ClientId, () => MockClient.Id)
            .RuleFor(x => x.Client, () => MockClient)
            .RuleFor(x => x.Comment, f => f.Lorem.Sentence())
            .RuleFor(x => x.Deposit, f => f.Random.Int(1, 100))
            .RuleFor(x => x.TransportFromPrice, () => null)
            .RuleFor(x => x.TransportToPrice, f => f.Random.Int(1, 100))
            .RuleFor(x => x.DateAdded, () => DateTime.Now)
            .RuleFor(x => x.Equipments, () => new List<Equipment> { MockEquipment })
            .RuleFor(x => x.Payments, () => new List<Payment>())
            .Generate();

        _appDbContext.Add(newAgreement);
        await _appDbContext.SaveChangesAsync();

        Assume.That(_appDbContext.Clients.Count(), Is.EqualTo(1));

        // act
        await _agreementRepository.Deactivate(2);

        // assert
        var agreement = _appDbContext.Agreements.First();
        Assert.That(agreement.IsActive, Is.False);
    }
}