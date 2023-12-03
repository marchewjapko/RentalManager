using Microsoft.EntityFrameworkCore;
using RentalManager.Core.Domain;
using RentalManager.Infrastructure.Exceptions;
using RentalManager.Infrastructure.Repositories;

namespace RentalManager.Tests.RepositoryTests;

public class ClientRepositoryTests
{
    private AppDbContext _appDbContext = null!;
    private ClientRepository _clientRepository = null!;

    [SetUp]
    public void Setup()
    {
        var optionsBuilder =
            new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase("TestingDatabase");
        _appDbContext = new AppDbContext(optionsBuilder.Options);
        _clientRepository = new ClientRepository(_appDbContext);

        Assume.That(_appDbContext.Clients.Count(), Is.EqualTo(0));
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
        var newClient = new Client
        {
            Name = "Test Name",
            Surname = "Test Surname",
            City = "Test City",
            Street = "Test street",
            IdCard = "ABC 123456",
            PhoneNumber = "123 456 789"
        };

        // act
        var result = await _clientRepository.AddAsync(newClient);

        // assert
        Assert.Multiple(() => {
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(1));
        });
    }

    [Test]
    public async Task ShouldDelete()
    {
        // arrange
        var newClient = new Client
        {
            Name = "Test Name",
            Surname = "Test Surname",
            City = "Test City",
            Street = "Test street",
            IdCard = "ABC 123456",
            PhoneNumber = "123 456 789"
        };
        _appDbContext.Add(newClient);
        await _appDbContext.SaveChangesAsync();
        Assume.That(_appDbContext.Clients.Count(), Is.EqualTo(1));
        
        // act
        await _clientRepository.DeleteAsync(1);
        
        // assert
        Assert.That(_appDbContext.Clients.Count(), Is.EqualTo(0));
    }

    [Test]
    public void ShouldNotDelete()
    {
        Assert.ThrowsAsync<ClientNotFoundException>(async () =>
            await _clientRepository.DeleteAsync(1));
    }

    [Test]
    public async Task ShouldGet()
    {
        // arrange
        var newClient = new Client
        {
            Name = "Test Name",
            Surname = "Test Surname",
            City = "Test City",
            Street = "Test street",
            IdCard = "ABC 123456",
            PhoneNumber = "123 456 789"
        };
        _appDbContext.Add(newClient);
        await _appDbContext.SaveChangesAsync();
        
        // act
        var result = await _clientRepository.GetAsync(1);
        
        // assert
        Assert.That(result.Id, Is.EqualTo(1));
    }

    [Test]
    public void ShouldNotGetClient()
    {
        Assert.ThrowsAsync<ClientNotFoundException>(async () =>
            await _clientRepository.GetAsync(1));
    }

    [Test]
    public async Task ShouldBrowseAll()
    {
        // arrange
        var newClient1 = new Client
        {
            Name = "Test Name 1",
            Surname = "Test Surname 1",
            City = "Test City 1",
            Street = "Test street 1",
            IdCard = "ABC 123456",
            PhoneNumber = "123 456 789"
        };
        var newClient2 = new Client
        {
            Name = "Test Name 2",
            Surname = "Test Surname 2",
            City = "Test City 2",
            Street = "Test street 2",
            IdCard = "ABC 123456",
            PhoneNumber = "123 456 789"
        };
        _appDbContext.Add(newClient1);
        _appDbContext.Add(newClient2);
        await _appDbContext.SaveChangesAsync();
        
        // act
        var result = await _clientRepository.BrowseAllAsync();
        
        // assert
        Assert.That(result.Count(), Is.EqualTo(2));
    }

    [Test]
    public async Task ShouldFilter_byName()
    {
        // arrange
        var newClient1 = new Client
        {
            Name = "Test Name 1",
            Surname = "Test Surname 1",
            City = "Test City 1",
            Street = "Test street 1",
            IdCard = "ABC 123456",
            PhoneNumber = "111 111 111"
        };
        var newClient2 = new Client
        {
            Name = "Test Name 2",
            Surname = "Test Surname 2",
            City = "Test City 2",
            Street = "Test street 2",
            IdCard = "ABC 123456",
            PhoneNumber = "222 222 222"
        };
        _appDbContext.Add(newClient1);
        _appDbContext.Add(newClient2);
        await _appDbContext.SaveChangesAsync();
        
        // act
        var result = (await _clientRepository.BrowseAllAsync("Test Name 1")).ToList();
        
        // assert
        Assert.Multiple(() => {
            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result[0].Name, Is.EqualTo("Test Name 1"));
        });
    }

    [Test]
    public async Task ShouldFilter_bySurname()
    {
        // arrange
        var newClient1 = new Client
        {
            Name = "Test Name 1",
            Surname = "Test Surname 1",
            City = "Test City 1",
            Street = "Test street 1",
            IdCard = "ABC 123456",
            PhoneNumber = "111 111 111"
        };
        var newClient2 = new Client
        {
            Name = "Test Name 2",
            Surname = "Test Surname 2",
            City = "Test City 2",
            Street = "Test street 2",
            IdCard = "ABC 123456",
            PhoneNumber = "222 222 222"
        };
        _appDbContext.Add(newClient1);
        _appDbContext.Add(newClient2);
        await _appDbContext.SaveChangesAsync();
        
        // act
        var result = (await _clientRepository.BrowseAllAsync(null, "Test Surname 1")).ToList();
        
        // assert
        Assert.Multiple(() => {
            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result[0].Surname, Is.EqualTo("Test Surname 1"));
        });
    }

    [Test]
    public async Task ShouldFilter_byPhoneNumber()
    {
        // arrange
        var newClient1 = new Client
        {
            Name = "Test Name 1",
            Surname = "Test Surname 1",
            City = "Test City 1",
            Street = "Test street 1",
            IdCard = "ABC 123456",
            PhoneNumber = "111 111 111"
        };
        var newClient2 = new Client
        {
            Name = "Test Name 2",
            Surname = "Test Surname 2",
            City = "Test City 2",
            Street = "Test street 2",
            IdCard = "ABC 123456",
            PhoneNumber = "222 222 222"
        };
        _appDbContext.Add(newClient1);
        _appDbContext.Add(newClient2);
        await _appDbContext.SaveChangesAsync();
        
        // act
        var result = (await _clientRepository.BrowseAllAsync(null, null, "111 111 111")).ToList();
        
        // assert
        Assert.Multiple(() => {
            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result[0].Surname, Is.EqualTo("Test Surname 1"));
        });
    }

    [Test]
    public async Task ShouldUpdate()
    {
        // arrange
        var newClient = new Client
        {
            Name = "Test Name 1",
            Surname = "Test Surname 1",
            City = "Test City 1",
            Street = "Test street 1",
            IdCard = "ABC 123456",
            PhoneNumber = "111 111 111"
        };
        _appDbContext.Add(newClient);
        await _appDbContext.SaveChangesAsync();
        newClient.Name = "NEW TEST NAME";
        
        // act
        await _clientRepository.UpdateAsync(newClient, 1);
        
        // assert
        var updatedClient = _appDbContext.Clients.First();
        Assert.That(updatedClient.Name, Is.EqualTo("NEW TEST NAME"));
    }
}