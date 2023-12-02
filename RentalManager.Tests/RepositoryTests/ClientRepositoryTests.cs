using Microsoft.EntityFrameworkCore;
using RentalManager.Core.Domain;
using RentalManager.Infrastructure.Repositories;

namespace RentalManager.Tests.RepositoryTests;

public class ClientRepositoryTests
{
    private AppDbContext _appDbContext = null!;
    private ClientRepository _clientRepository = null!;

    [SetUp]
    public void Setup()
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase("TestingDatabase");
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
        var newClient = new Client
        {
            Name = "Test Name",
            Surname = "Test Surname",
            City = "Test City",
            Street = "Test street",
            IdCard = "ABC 123456",
            PhoneNumber = "123 456 789"
        };
        var result = await _clientRepository.AddAsync(newClient);
        Assert.Multiple(() => {
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(1));
        });
    }

    [Test]
    public async Task ShouldDelete()
    {
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
        Assert.That(_appDbContext.Clients.Count(), Is.EqualTo(1));
        await _clientRepository.DeleteAsync(1);
        Assert.That(_appDbContext.Clients.Count(), Is.EqualTo(0));
    }

    [Test]
    public void ShouldNotDelete()
    {
        var ex = Assert.ThrowsAsync<Exception>(async () => await _clientRepository.DeleteAsync(1));
        Assert.That(ex.Message, Is.EqualTo("Unable to find client"));
    }

    [Test]
    public async Task ShouldGet()
    {
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
        var result = await _clientRepository.GetAsync(1);
        Assert.That(result.Id, Is.EqualTo(1));
    }

    [Test]
    public void ShouldNotGetClient()
    {
        var ex = Assert.ThrowsAsync<Exception>(async () => await _clientRepository.GetAsync(1));
        Assert.That(ex.Message, Is.EqualTo("Unable to find client"));
    }

    [Test]
    public async Task ShouldBrowseAll()
    {
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
        var result = await _clientRepository.BrowseAllAsync();
        Assert.That(result.Count(), Is.EqualTo(2));
    }

    [Test]
    public async Task ShouldFilter_byName()
    {
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
        var result = (await _clientRepository.BrowseAllAsync("Test Name 1")).ToList();
        Assert.Multiple(() => {
            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result[0].Name, Is.EqualTo("Test Name 1"));
        });
    }

    [Test]
    public async Task ShouldFilter_bySurname()
    {
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
        var result = (await _clientRepository.BrowseAllAsync(null, "Test Surname 1")).ToList();
        Assert.Multiple(() => {
            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result[0].Surname, Is.EqualTo("Test Surname 1"));
        });
    }

    [Test]
    public async Task ShouldFilter_byPhoneNumber()
    {
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
        var result = (await _clientRepository.BrowseAllAsync(null, null, "111 111 111")).ToList();
        Assert.Multiple(() => {
            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result[0].Surname, Is.EqualTo("Test Surname 1"));
        });
    }

    [Test]
    public async Task ShouldUpdate()
    {
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
        await _clientRepository.UpdateAsync(newClient, 1);
        var updatedClient = _appDbContext.Clients.First();
        Assert.That(updatedClient.Name, Is.EqualTo("NEW TEST NAME"));
    }
}