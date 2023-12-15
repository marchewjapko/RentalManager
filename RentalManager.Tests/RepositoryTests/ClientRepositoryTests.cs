using Bogus;
using Microsoft.EntityFrameworkCore;
using RentalManager.Core.Domain;
using RentalManager.Global.Queries;
using RentalManager.Infrastructure.Exceptions;
using RentalManager.Infrastructure.Repositories;
using RentalManager.Infrastructure.Repositories.DbContext;

namespace RentalManager.Tests.RepositoryTests;

public class ClientRepositoryTests
{
    private readonly Client _mockClient = new Faker<Client>()
        .RuleFor(x => x.Id, () => 1)
        .RuleFor(x => x.Name, f => f.Name.FirstName())
        .RuleFor(x => x.Surname, f => f.Name.LastName())
        .RuleFor(x => x.PhoneNumber, f => f.Phone.PhoneNumber("###-###-###"))
        .RuleFor(x => x.Email, f => f.Internet.Email())
        .RuleFor(x => x.IdCard, () => "ABC 123456")
        .RuleFor(x => x.City, f => f.Address.City())
        .RuleFor(x => x.Street, f => f.Address.StreetName())
        .Generate();

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
        // act
        await _clientRepository.AddAsync(_mockClient);

        // assert
        Assert.That(_appDbContext.Clients.Count(), Is.EqualTo(1));
    }

    [Test]
    public async Task ShouldDelete()
    {
        // arrange
        _appDbContext.Add(_mockClient);
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
        _appDbContext.Add(_mockClient);
        await _appDbContext.SaveChangesAsync();

        Assume.That(_appDbContext.Clients.Count(), Is.EqualTo(1));

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
        var newClient = new Faker<Client>()
            .RuleFor(x => x.Id, () => 2)
            .RuleFor(x => x.Name, f => f.Name.FirstName())
            .RuleFor(x => x.Surname, f => f.Name.LastName())
            .RuleFor(x => x.PhoneNumber, f => f.Phone.PhoneNumber("###-###-###"))
            .RuleFor(x => x.Email, f => f.Internet.Email())
            .RuleFor(x => x.IdCard, () => "ABC 123456")
            .RuleFor(x => x.City, f => f.Address.City())
            .RuleFor(x => x.Street, f => f.Address.StreetName())
            .Generate();

        _appDbContext.Add(_mockClient);
        _appDbContext.Add(newClient);
        await _appDbContext.SaveChangesAsync();

        Assume.That(_appDbContext.Clients.Count(), Is.EqualTo(2));
        Assume.That(_appDbContext.Clients.First()
            .Id, Is.EqualTo(1));
        Assume.That(_appDbContext.Clients.Skip(1)
            .First()
            .Id, Is.EqualTo(2));

        // act
        var result = await _clientRepository.BrowseAllAsync(new QueryClients());

        // assert
        Assert.That(result.Count(), Is.EqualTo(2));
    }

    [Test]
    public async Task ShouldFilter_byName()
    {
        // arrange
        var newClient = new Faker<Client>()
            .RuleFor(x => x.Id, () => 2)
            .RuleFor(x => x.Name, f => f.Name.FirstName())
            .RuleFor(x => x.Surname, f => f.Name.LastName())
            .RuleFor(x => x.PhoneNumber, f => f.Phone.PhoneNumber("###-###-###"))
            .RuleFor(x => x.Email, f => f.Internet.Email())
            .RuleFor(x => x.IdCard, () => "ABC 123456")
            .RuleFor(x => x.City, f => f.Address.City())
            .RuleFor(x => x.Street, f => f.Address.StreetName())
            .Generate();

        _appDbContext.Add(_mockClient);
        _appDbContext.Add(newClient);
        await _appDbContext.SaveChangesAsync();

        var query = new QueryClients
        {
            Name = newClient.Name
        };

        Assume.That(_appDbContext.Clients.Count(), Is.EqualTo(2));
        Assume.That(_appDbContext.Clients.First()
            .Id, Is.EqualTo(1));
        Assume.That(_appDbContext.Clients.Skip(1)
            .First()
            .Id, Is.EqualTo(2));

        // act
        var result = (await _clientRepository.BrowseAllAsync(query)).ToList();

        // assert
        Assert.Multiple(() => {
            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result[0].Name, Is.EqualTo(newClient.Name));
        });
    }

    [Test]
    public async Task ShouldFilter_bySurname()
    {
        // arrange
        var newClient = new Faker<Client>()
            .RuleFor(x => x.Id, () => 2)
            .RuleFor(x => x.Name, f => f.Name.FirstName())
            .RuleFor(x => x.Surname, f => f.Name.LastName())
            .RuleFor(x => x.PhoneNumber, f => f.Phone.PhoneNumber("###-###-###"))
            .RuleFor(x => x.Email, f => f.Internet.Email())
            .RuleFor(x => x.IdCard, () => "ABC 123456")
            .RuleFor(x => x.City, f => f.Address.City())
            .RuleFor(x => x.Street, f => f.Address.StreetName())
            .Generate();

        _appDbContext.Add(_mockClient);
        _appDbContext.Add(newClient);
        await _appDbContext.SaveChangesAsync();

        var query = new QueryClients
        {
            Surname = newClient.Surname
        };

        Assume.That(_appDbContext.Clients.Count(), Is.EqualTo(2));
        Assume.That(_appDbContext.Clients.First()
            .Id, Is.EqualTo(1));
        Assume.That(_appDbContext.Clients.Skip(1)
            .First()
            .Id, Is.EqualTo(2));

        // act
        var result = (await _clientRepository.BrowseAllAsync(query)).ToList();

        // assert
        Assert.Multiple(() => {
            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result[0].Surname, Is.EqualTo(newClient.Surname));
        });
    }

    [Test]
    public async Task ShouldFilter_byPhoneNumber()
    {
        // arrange
        var newClient = new Faker<Client>()
            .RuleFor(x => x.Id, () => 2)
            .RuleFor(x => x.Name, f => f.Name.FirstName())
            .RuleFor(x => x.Surname, f => f.Name.LastName())
            .RuleFor(x => x.PhoneNumber, f => f.Phone.PhoneNumber("###-###-###"))
            .RuleFor(x => x.Email, f => f.Internet.Email())
            .RuleFor(x => x.IdCard, () => "ABC 123456")
            .RuleFor(x => x.City, f => f.Address.City())
            .RuleFor(x => x.Street, f => f.Address.StreetName())
            .Generate();

        _appDbContext.Add(_mockClient);
        _appDbContext.Add(newClient);
        await _appDbContext.SaveChangesAsync();

        var query = new QueryClients
        {
            PhoneNumber = newClient.PhoneNumber
        };

        Assume.That(_appDbContext.Clients.Count(), Is.EqualTo(2));
        Assume.That(_appDbContext.Clients.First()
            .Id, Is.EqualTo(1));
        Assume.That(_appDbContext.Clients.Skip(1)
            .First()
            .Id, Is.EqualTo(2));

        // act
        var result = (await _clientRepository.BrowseAllAsync(query)).ToList();
        
        // assert
        Assert.Multiple(() => {
            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result[0].Surname, Is.EqualTo(newClient.Surname));
        });
    }

    [Test]
    public async Task ShouldUpdate()
    {
        // arrange
        var newClient = new Faker<Client>()
            .RuleFor(x => x.Id, () => 1)
            .RuleFor(x => x.Name, f => f.Name.FirstName())
            .RuleFor(x => x.Surname, f => f.Name.LastName())
            .RuleFor(x => x.PhoneNumber, f => f.Phone.PhoneNumber("###-###-###"))
            .RuleFor(x => x.Email, f => f.Internet.Email())
            .RuleFor(x => x.IdCard, () => "ABC 123456")
            .RuleFor(x => x.City, f => f.Address.City())
            .RuleFor(x => x.Street, f => f.Address.StreetName())
            .Generate();
        _appDbContext.Add(newClient);
        await _appDbContext.SaveChangesAsync();
        newClient.Name = "NEW TEST NAME";

        Assume.That(_appDbContext.Clients.Count(), Is.EqualTo(1));

        // act
        await _clientRepository.UpdateAsync(newClient, 1);

        // assert
        var updatedClient = _appDbContext.Clients.First();
        Assert.That(updatedClient.Name, Is.EqualTo("NEW TEST NAME"));
    }

    [Test]
    public async Task ShouldDeactivate()
    {
        // arrange
        _appDbContext.Add(_mockClient);
        await _appDbContext.SaveChangesAsync();
        Assume.That(_appDbContext.Clients.Count(), Is.EqualTo(1));

        // act
        await _clientRepository.Deactivate(1);

        // assert
        Assert.Multiple(() => {
            Assert.That(_appDbContext.Clients.Count(), Is.EqualTo(1));
            Assert.That(_appDbContext.Clients.First()
                .IsActive, Is.False);
        });
    }
}