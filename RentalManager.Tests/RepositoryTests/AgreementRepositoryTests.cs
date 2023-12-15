using Bogus;
using Microsoft.EntityFrameworkCore;
using RentalManager.Core.Domain;
using RentalManager.Global.Queries;
using RentalManager.Infrastructure.Exceptions;
using RentalManager.Infrastructure.Repositories;
using RentalManager.Infrastructure.Repositories.DbContext;

namespace RentalManager.Tests.RepositoryTests;

public class AgreementRepositoryTests
{
    private static readonly Client MockClient = new Faker<Client>()
        .RuleFor(x => x.Id, () => 1)
        .RuleFor(x => x.Name, f => f.Name.FirstName())
        .RuleFor(x => x.Surname, f => f.Name.LastName())
        .RuleFor(x => x.City, f => f.Address.City())
        .RuleFor(x => x.Street, f => f.Address.StreetName())
        .RuleFor(x => x.IdCard, () => "ABC 123456")
        .RuleFor(x => x.PhoneNumber, f => f.Phone.PhoneNumber("###-###-###"))
        .Generate();

    private static readonly Equipment MockEquipment = new Faker<Equipment>()
        .RuleFor(x => x.Id, () => 1)
        .RuleFor(x => x.Name, f => f.Commerce.ProductName())
        .RuleFor(x => x.Price, f => f.Random.Int(1, 200))
        .Generate();

    private static readonly User MockUser = new Faker<User>()
        .RuleFor(x => x.Id, () => 1)
        .RuleFor(x => x.Name, f => f.Name.FirstName())
        .RuleFor(x => x.Surname, f => f.Name.LastName())
        .RuleFor(x => x.UserName, f => f.Internet.UserName())
        .RuleFor(x => x.Gender, () => Gender.Man)
        .RuleFor(x => x.CreatedTs, () => DateTime.Now)
        .Generate();

    private static readonly Agreement MockAgreement = new Faker<Agreement>()
        .RuleFor(x => x.Id, () => 1)
        .RuleFor(x => x.EmployeeId, () => MockUser.Id)
        .RuleFor(x => x.Employee, () => MockUser)
        .RuleFor(x => x.User, () => MockUser)
        .RuleFor(x => x.ClientId, () => MockClient.Id)
        .RuleFor(x => x.Client, () => MockClient)
        .RuleFor(x => x.Comment, f => f.Lorem.Sentence())
        .RuleFor(x => x.Deposit, f => f.Random.Int(1, 100))
        .RuleFor(x => x.TransportFromPrice, () => null)
        .RuleFor(x => x.TransportToPrice, f => f.Random.Int(1, 100))
        .RuleFor(x => x.DateAdded, () => DateTime.Now)
        .RuleFor(x => x.Equipment, () => new List<Equipment> { MockEquipment })
        .RuleFor(x => x.Payments, () => new List<Payment>())
        .Generate();

    private AgreementRepository _agreementRepository = null!;
    private AppDbContext _appDbContext = null!;

    [SetUp]
    public void Setup()
    {
        var optionsBuilder =
            new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase("TestingDatabase");
        _appDbContext = new AppDbContext(optionsBuilder.Options);
        _agreementRepository = new AgreementRepository(_appDbContext);
        _appDbContext.Clients.Add(MockClient);
        _appDbContext.Users.Add(MockUser);
        _appDbContext.Equipment.Add(MockEquipment);
        _appDbContext.SaveChanges();

        Assume.That(_appDbContext.Agreements.Count(), Is.EqualTo(0));
    }

    [TearDown]
    public void TearDown()
    {
        _appDbContext.Agreements.RemoveRange(_appDbContext.Agreements);
        _appDbContext.Clients.RemoveRange(_appDbContext.Clients);
        _appDbContext.Equipment.RemoveRange(_appDbContext.Equipment);
        _appDbContext.SaveChanges();
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
        var xx = _appDbContext.Agreements.Add(MockAgreement);
        await _appDbContext.SaveChangesAsync();

        Assume.That(_appDbContext.Agreements.Count(), Is.EqualTo(1));

        // act
        var result = await _agreementRepository.GetAsync(1);

        // assert
        Assert.Multiple(() => {
            Assert.That(result.Id, Is.EqualTo(MockAgreement.Id));
            Assert.That(result.Employee, Is.Not.Null);
            Assert.That(result.Client, Is.Not.Null);
            Assert.That(result.Equipment, Has.Count.EqualTo(1));
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
            .RuleFor(x => x.EmployeeId, () => MockUser.Id)
            .RuleFor(x => x.Employee, () => MockUser)
            .RuleFor(x => x.User, () => MockUser)
            .RuleFor(x => x.ClientId, () => MockClient.Id)
            .RuleFor(x => x.Client, () => MockClient)
            .RuleFor(x => x.Comment, f => f.Lorem.Sentence())
            .RuleFor(x => x.Deposit, f => f.Random.Int(1, 100))
            .RuleFor(x => x.TransportFromPrice, () => null)
            .RuleFor(x => x.TransportToPrice, f => f.Random.Int(1, 100))
            .RuleFor(x => x.DateAdded, () => DateTime.Now)
            .RuleFor(x => x.Equipment, () => new List<Equipment>())
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
            .RuleFor(x => x.Name, f => f.Name.FirstName())
            .RuleFor(x => x.Surname, f => f.Name.LastName())
            .RuleFor(x => x.City, () => MockClient.City)
            .RuleFor(x => x.Street, f => f.Address.StreetName())
            .RuleFor(x => x.IdCard, () => "ABC 123456")
            .RuleFor(x => x.PhoneNumber, f => f.Phone.PhoneNumber("###-###-###"))
            .Generate();

        var newUser = new Faker<User>()
            .RuleFor(x => x.Name, f => f.Name.FirstName())
            .RuleFor(x => x.Surname, f => f.Name.LastName())
            .RuleFor(x => x.UserName, f => f.Internet.UserName())
            .RuleFor(x => x.Gender, () => Gender.Man)
            .RuleFor(x => x.CreatedTs, () => DateTime.Now)
            .Generate();

        var newEquipment = new Faker<Equipment>()
            .RuleFor(x => x.Name, f => f.Commerce.ProductName())
            .RuleFor(x => x.Price, f => f.Random.Int(1, 200))
            .Generate();

        var clientEntry = _appDbContext.Clients.Add(newClient)
            .Entity;
        var userEntry = _appDbContext.Users.Add(newUser)
            .Entity;
        var equipmentEntry = _appDbContext.Equipment.Add(newEquipment)
            .Entity;

        var newAgreement = new Faker<Agreement>()
            .RuleFor(x => x.Id, () => 2)
            .RuleFor(x => x.EmployeeId, () => clientEntry.Id)
            .RuleFor(x => x.Employee, () => userEntry)
            .RuleFor(x => x.User, () => userEntry)
            .RuleFor(x => x.ClientId, () => clientEntry.Id)
            .RuleFor(x => x.Client, () => clientEntry)
            .RuleFor(x => x.Comment, f => f.Lorem.Sentence())
            .RuleFor(x => x.Deposit, f => f.Random.Int(1, 100))
            .RuleFor(x => x.TransportFromPrice, () => null)
            .RuleFor(x => x.TransportToPrice, f => f.Random.Int(1, 100))
            .RuleFor(x => x.DateAdded, () => DateTime.Now)
            .RuleFor(x => x.Equipment, () => new List<Equipment> { equipmentEntry })
            .RuleFor(x => x.Payments, () => new List<Payment>())
            .Generate();

        _appDbContext.Agreements.Add(MockAgreement);
        _appDbContext.Agreements.Add(newAgreement);
        await _appDbContext.SaveChangesAsync();
        Assume.That(_appDbContext.Agreements.Count(), Is.EqualTo(2));

        var query1 = new QueryAgreements
        {
            Surname = newAgreement.Client.Surname
        };

        var query2 = new QueryAgreements
        {
            City = MockClient.City
        };
        
        // act
        var result1 = await _agreementRepository.BrowseAllAsync(query1);
        var result2 = await _agreementRepository.BrowseAllAsync(query2);

        // assert
        Assert.Multiple(() => {
            Assert.That(result1.Count(), Is.EqualTo(1));
            Assert.That(result1.First()
                .Employee.Name, Is.EqualTo(newAgreement.Employee.Name));
            Assert.That(result2.Count(), Is.EqualTo(2));
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
            .RuleFor(x => x.EmployeeId, () => MockUser.Id)
            .RuleFor(x => x.Employee, () => MockUser)
            .RuleFor(x => x.User, () => MockUser)
            .RuleFor(x => x.ClientId, () => MockClient.Id)
            .RuleFor(x => x.Client, () => MockClient)
            .RuleFor(x => x.Comment, f => f.Lorem.Sentence())
            .RuleFor(x => x.Deposit, f => f.Random.Int(1, 100))
            .RuleFor(x => x.TransportFromPrice, () => null)
            .RuleFor(x => x.TransportToPrice, f => f.Random.Int(1, 100))
            .RuleFor(x => x.DateAdded, () => DateTime.Now)
            .RuleFor(x => x.Equipment, () => new List<Equipment> { MockEquipment })
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
            .RuleFor(x => x.EmployeeId, () => MockUser.Id)
            .RuleFor(x => x.Employee, () => MockUser)
            .RuleFor(x => x.User, () => MockUser)
            .RuleFor(x => x.ClientId, () => MockClient.Id)
            .RuleFor(x => x.Client, () => MockClient)
            .RuleFor(x => x.Comment, f => f.Lorem.Sentence())
            .RuleFor(x => x.Deposit, f => f.Random.Int(1, 100))
            .RuleFor(x => x.TransportFromPrice, () => null)
            .RuleFor(x => x.TransportToPrice, f => f.Random.Int(1, 100))
            .RuleFor(x => x.DateAdded, () => DateTime.Now)
            .RuleFor(x => x.Equipment, () => new List<Equipment> { MockEquipment })
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

    private Tuple<Client, User, Equipment> GetMocks()
    {
        var client = _appDbContext.Clients.First();
        var user = _appDbContext.Users.First();
        var equipment = _appDbContext.Equipment.First();

        if (client == null || user == null || equipment == null)
        {
            throw new Exception("No mocks in database");
        }

        return Tuple.Create(client, user, equipment);
    }
}