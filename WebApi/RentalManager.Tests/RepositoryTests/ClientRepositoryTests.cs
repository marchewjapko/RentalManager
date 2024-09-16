using Bogus;
using Microsoft.EntityFrameworkCore;
using RentalManager.Core.Domain;
using RentalManager.Global.Queries;
using RentalManager.Infrastructure.ExceptionHandling.Exceptions;
using RentalManager.Infrastructure.Repositories;
using RentalManager.Infrastructure.Repositories.DbContext;

namespace RentalManager.Tests.RepositoryTests;

public class ClientRepositoryTests
{
    private AppDbContext _appDbContext = null!;
    private ClientRepository _clientRepository = null!;

    private Client MockClient { get; set; }

    [SetUp]
    public void Setup()
    {
        MockClient = new Faker<Client>()
            .RuleFor(x => x.Id, () => 1)
            .RuleFor(x => x.FirstName, f => f.Name.FirstName())
            .RuleFor(x => x.LastName, f => f.Name.LastName())
            .RuleFor(x => x.PhoneNumber, f => f.Phone.PhoneNumber("###-###-###"))
            .RuleFor(x => x.Email, f => f.Internet.Email())
            .RuleFor(x => x.IdCard, () => "ABC 123456")
            .RuleFor(x => x.City, f => f.Address.City())
            .RuleFor(x => x.Street, f => f.Address.StreetName())
            .Generate();

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
        // Act
        await _clientRepository.AddAsync(MockClient);

        // Assert
        Assert.That(_appDbContext.Clients.Count(), Is.EqualTo(1));
    }

    [Test]
    public async Task ShouldDelete()
    {
        // Arrange
        _appDbContext.Add(MockClient);
        await _appDbContext.SaveChangesAsync();

        Assume.That(_appDbContext.Clients.Count(), Is.EqualTo(1));

        // Act
        await _clientRepository.DeleteAsync(1);

        // Assert
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
        // Arrange
        _appDbContext.Add(MockClient);
        await _appDbContext.SaveChangesAsync();

        Assume.That(_appDbContext.Clients.Count(), Is.EqualTo(1));

        // Act
        var result = await _clientRepository.GetAsync(1);

        // Assert
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
        // Arrange
        var newClient = new Faker<Client>()
            .RuleFor(x => x.Id, () => 2)
            .RuleFor(x => x.FirstName, f => f.Name.FirstName())
            .RuleFor(x => x.LastName, f => f.Name.LastName())
            .RuleFor(x => x.PhoneNumber, f => f.Phone.PhoneNumber("###-###-###"))
            .RuleFor(x => x.Email, f => f.Internet.Email())
            .RuleFor(x => x.IdCard, () => "ABC 123456")
            .RuleFor(x => x.City, f => f.Address.City())
            .RuleFor(x => x.Street, f => f.Address.StreetName())
            .Generate();

        _appDbContext.Add(MockClient);
        _appDbContext.Add(newClient);
        await _appDbContext.SaveChangesAsync();

        Assume.That(_appDbContext.Clients.Count(), Is.EqualTo(2));
        Assume.That(_appDbContext.Clients.First()
            .Id, Is.EqualTo(1));
        Assume.That(_appDbContext.Clients.Skip(1)
            .First()
            .Id, Is.EqualTo(2));

        // Act
        var result = await _clientRepository.BrowseAllAsync(new QueryClients());

        // Assert
        Assert.That(result.Count(), Is.EqualTo(2));
    }

    [Test]
    public async Task ShouldFilter_byName()
    {
        // Arrange
        var newClient = new Faker<Client>()
            .RuleFor(x => x.Id, () => 2)
            .RuleFor(x => x.FirstName, f => f.Name.FirstName())
            .RuleFor(x => x.LastName, f => f.Name.LastName())
            .RuleFor(x => x.PhoneNumber, f => f.Phone.PhoneNumber("###-###-###"))
            .RuleFor(x => x.Email, f => f.Internet.Email())
            .RuleFor(x => x.IdCard, () => "ABC 123456")
            .RuleFor(x => x.City, f => f.Address.City())
            .RuleFor(x => x.Street, f => f.Address.StreetName())
            .Generate();

        _appDbContext.Add(MockClient);
        _appDbContext.Add(newClient);
        await _appDbContext.SaveChangesAsync();

        var query = new QueryClients
        {
            Name = newClient.FirstName
        };

        Assume.That(_appDbContext.Clients.Count(), Is.EqualTo(2));
        Assume.That(_appDbContext.Clients.First()
            .Id, Is.EqualTo(1));
        Assume.That(_appDbContext.Clients.Skip(1)
            .First()
            .Id, Is.EqualTo(2));

        // Act
        var result = (await _clientRepository.BrowseAllAsync(query)).ToList();

        // Assert
        Assert.Multiple(() => {
            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result[0].FirstName, Is.EqualTo(newClient.FirstName));
        });
    }

    [Test]
    public async Task ShouldFilter_bySurname()
    {
        // Arrange
        var newClient = new Faker<Client>()
            .RuleFor(x => x.Id, () => 2)
            .RuleFor(x => x.FirstName, f => f.Name.FirstName())
            .RuleFor(x => x.LastName, f => f.Name.LastName())
            .RuleFor(x => x.PhoneNumber, f => f.Phone.PhoneNumber("###-###-###"))
            .RuleFor(x => x.Email, f => f.Internet.Email())
            .RuleFor(x => x.IdCard, () => "ABC 123456")
            .RuleFor(x => x.City, f => f.Address.City())
            .RuleFor(x => x.Street, f => f.Address.StreetName())
            .Generate();

        _appDbContext.Add(MockClient);
        _appDbContext.Add(newClient);
        await _appDbContext.SaveChangesAsync();

        var query = new QueryClients
        {
            Surname = newClient.LastName
        };

        Assume.That(_appDbContext.Clients.Count(), Is.EqualTo(2));
        Assume.That(_appDbContext.Clients.First()
            .Id, Is.EqualTo(1));
        Assume.That(_appDbContext.Clients.Skip(1)
            .First()
            .Id, Is.EqualTo(2));

        // Act
        var result = (await _clientRepository.BrowseAllAsync(query)).ToList();

        // Assert
        Assert.Multiple(() => {
            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result[0].LastName, Is.EqualTo(newClient.LastName));
        });
    }

    [Test]
    public async Task ShouldFilter_byPhoneNumber()
    {
        // Arrange
        var newClient = new Faker<Client>()
            .RuleFor(x => x.Id, () => 2)
            .RuleFor(x => x.FirstName, f => f.Name.FirstName())
            .RuleFor(x => x.LastName, f => f.Name.LastName())
            .RuleFor(x => x.PhoneNumber, f => f.Phone.PhoneNumber("###-###-###"))
            .RuleFor(x => x.Email, f => f.Internet.Email())
            .RuleFor(x => x.IdCard, () => "ABC 123456")
            .RuleFor(x => x.City, f => f.Address.City())
            .RuleFor(x => x.Street, f => f.Address.StreetName())
            .Generate();

        _appDbContext.Add(MockClient);
        _appDbContext.Add(newClient);
        await _appDbContext.SaveChangesAsync();

        var query = new QueryClients
        {
            Name = newClient.FirstName
        };

        Assume.That(_appDbContext.Clients.Count(), Is.EqualTo(2));
        Assume.That(_appDbContext.Clients.First()
            .Id, Is.EqualTo(1));
        Assume.That(_appDbContext.Clients.Skip(1)
            .First()
            .Id, Is.EqualTo(2));

        // Act
        var result = (await _clientRepository.BrowseAllAsync(query)).ToList();

        // Assert
        Assert.Multiple(() => {
            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result[0].LastName, Is.EqualTo(newClient.LastName));
        });
    }

    [Test]
    public async Task ShouldUpdate()
    {
        // Arrange
        var newClient = new Faker<Client>()
            .RuleFor(x => x.Id, () => 1)
            .RuleFor(x => x.FirstName, f => f.Name.FirstName())
            .RuleFor(x => x.LastName, f => f.Name.LastName())
            .RuleFor(x => x.PhoneNumber, f => f.Phone.PhoneNumber("###-###-###"))
            .RuleFor(x => x.Email, f => f.Internet.Email())
            .RuleFor(x => x.IdCard, () => "ABC 123456")
            .RuleFor(x => x.City, f => f.Address.City())
            .RuleFor(x => x.Street, f => f.Address.StreetName())
            .Generate();
        _appDbContext.Add(newClient);
        await _appDbContext.SaveChangesAsync();
        newClient.FirstName = "NEW TEST NAME";

        Assume.That(_appDbContext.Clients.Count(), Is.EqualTo(1));

        // Act
        await _clientRepository.UpdateAsync(newClient, 1);

        // Assert
        var updatedClient = _appDbContext.Clients.First();
        Assert.That(updatedClient.FirstName, Is.EqualTo("NEW TEST NAME"));
    }

    [Test]
    public async Task ShouldDeactivate()
    {
        // Arrange
        _appDbContext.Add(MockClient);
        await _appDbContext.SaveChangesAsync();
        Assume.That(_appDbContext.Clients.Count(), Is.EqualTo(1));

        // Act
        await _clientRepository.Deactivate(1);

        // Assert
        Assert.Multiple(() => {
            Assert.That(_appDbContext.Clients.Count(), Is.EqualTo(1));
            Assert.That(_appDbContext.Clients.First()
                .IsActive, Is.False);
        });
    }
}