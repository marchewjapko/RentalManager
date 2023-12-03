using Microsoft.EntityFrameworkCore;
using RentalManager.Core.Domain;
using RentalManager.Infrastructure.Exceptions;
using RentalManager.Infrastructure.Repositories;

namespace RentalManager.Tests.RepositoryTests;

public class AgreementRepositoryTests
{
    private readonly Client _mockClient = new()
    {
        Name = "Test Name 1",
        Surname = "Test Surname 1",
        City = "Test City 1",
        Street = "Test street 1",
        IdCard = "ABC 123456",
        PhoneNumber = "111 111 111"
    };

    private readonly Employee _mockEmployee = new()
    {
        Name = "Test Name",
        Surname = "Test Surname"
    };

    private readonly Equipment _mockEquipment = new()
    {
        Name = "Test Name",
        Price = 100
    };

    private AgreementRepository _agreementRepository = null!;
    private AppDbContext _appDbContext = null!;

    [SetUp]
    public void Setup()
    {
        var optionsBuilder =
            new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase("TestingDatabase");
        _appDbContext = new AppDbContext(optionsBuilder.Options);
        _agreementRepository = new AgreementRepository(_appDbContext);
        _appDbContext.Clients.Add(_mockClient);
        _appDbContext.Employees.Add(_mockEmployee);
        _appDbContext.Equipment.Add(_mockEquipment);
        _appDbContext.SaveChanges();

        Assume.That(_appDbContext.Agreements.Count(), Is.EqualTo(0));
    }

    [TearDown]
    public void TearDown()
    {
        _appDbContext.Agreements.RemoveRange(_appDbContext.Agreements);
        _appDbContext.SaveChanges();
        _appDbContext.Database.EnsureDeleted();
        _appDbContext.Dispose();
    }

    [Test]
    public async Task ShouldAdd()
    {
        // arrange
        var (client, employee, equipment) = GetMocks();
        var newAgreement = new Agreement
        {
            EmployeeId = employee.Id,
            IsActive = true,
            ClientId = client.Id,
            Deposit = 100,
            TransportToPrice = 200,
            Equipment = new List<Equipment> { equipment }
        };

        // act
        var result = await _agreementRepository.AddAsync(newAgreement);

        // assert
        Assert.Multiple(() => {
            Assert.That(result.Id, Is.EqualTo(1));
            Assert.That(result.Employee, Is.Not.Null);
            Assert.That(result.Client, Is.Not.Null);
            Assert.That(result.Equipment, Has.Count.EqualTo(1));
            Assert.That(result.Deposit, Is.EqualTo(100));
            Assert.That(result.TransportToPrice, Is.EqualTo(200));
        });
    }

    [Test]
    public async Task ShouldGet()
    {
        // arrange
        var (client, employee, equipment) = GetMocks();
        var newAgreement = new Agreement
        {
            EmployeeId = employee.Id,
            IsActive = true,
            ClientId = client.Id,
            Deposit = 100,
            TransportToPrice = 200,
            Equipment = new List<Equipment> { equipment }
        };

        // act
        _appDbContext.Agreements.Add(newAgreement);
        await _appDbContext.SaveChangesAsync();
        var result = await _agreementRepository.GetAsync(1);

        // assert
        Assert.Multiple(() => {
            Assert.That(result.Id, Is.EqualTo(1));
            Assert.That(result.Employee, Is.Not.Null);
            Assert.That(result.Client, Is.Not.Null);
            Assert.That(result.Equipment, Has.Count.EqualTo(1));
            Assert.That(result.Deposit, Is.EqualTo(100));
            Assert.That(result.TransportToPrice, Is.EqualTo(200));
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
        var (client, employee, equipment) = GetMocks();
        var newAgreement = new Agreement
        {
            EmployeeId = employee.Id,
            IsActive = true,
            ClientId = client.Id,
            Deposit = 100,
            TransportToPrice = 200,
            Equipment = new List<Equipment> { equipment }
        };

        // act
        _appDbContext.Agreements.Add(newAgreement);
        await _appDbContext.SaveChangesAsync();

        // assert
        Assert.That(_appDbContext.Agreements.Count(), Is.EqualTo(1));
        await _agreementRepository.DeleteAsync(1);
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
        var (client, employee, equipment) = GetMocks();
        var newAgreement = new Agreement
        {
            EmployeeId = employee.Id,
            IsActive = true,
            ClientId = client.Id,
            Deposit = 100,
            TransportToPrice = 200,
            Equipment = new List<Equipment> { equipment }
        };
        var newAgreement2 = new Agreement
        {
            EmployeeId = employee.Id,
            IsActive = true,
            ClientId = client.Id,
            Deposit = 200,
            TransportToPrice = 300,
            Equipment = new List<Equipment> { equipment }
        };

        // act
        _appDbContext.Agreements.Add(newAgreement);
        _appDbContext.Agreements.Add(newAgreement2);
        await _appDbContext.SaveChangesAsync();

        // assert
        Assert.That(_appDbContext.Agreements.Count(), Is.EqualTo(2));
        var result = await _agreementRepository.BrowseAllAsync();
        Assert.That(result.Count(), Is.EqualTo(2));
    }

    [Test]
    public async Task ShouldFilter()
    {
        // arrange
        var (client, employee, equipment) = GetMocks();
        var newAgreement = new Agreement
        {
            EmployeeId = employee.Id,
            IsActive = true,
            ClientId = client.Id,
            Deposit = 100,
            TransportToPrice = 200,
            Equipment = new List<Equipment> { equipment }
        };
        _appDbContext.Agreements.Add(newAgreement);

        var newClient = new Client
        {
            Name = "Test Name 2",
            Surname = "Test Surname 2",
            City = "Test City 1",
            Street = "Test street 2",
            IdCard = "ABC 123456",
            PhoneNumber = "111 111 111"
        };
        var newEmployee = new Employee
        {
            Name = "Test Name 2",
            Surname = "Test Surname 2"
        };
        var newEquipment = new Equipment
        {
            Name = "Test Name 2",
            Price = 200
        };
        var clientEntry = _appDbContext.Clients.Add(newClient);
        var employeeEntry = _appDbContext.Employees.Add(newEmployee);
        var equipmentEntry = _appDbContext.Equipment.Add(newEquipment);
        var newAgreement2 = new Agreement
        {
            EmployeeId = employeeEntry.Entity.Id,
            IsActive = true,
            ClientId = clientEntry.Entity.Id,
            Deposit = 100,
            TransportToPrice = 200,
            Equipment = new List<Equipment> { equipmentEntry.Entity }
        };
        _appDbContext.Agreements.Add(newAgreement);
        _appDbContext.Agreements.Add(newAgreement2);
        await _appDbContext.SaveChangesAsync();
        Assert.That(_appDbContext.Agreements.Count(), Is.EqualTo(2));

        // act
        var result1 = await _agreementRepository.BrowseAllAsync(null, "Test Surname 2");
        var result2 = await _agreementRepository.BrowseAllAsync(null, null, null, "Test City 1");

        // assert
        Assert.Multiple(() => {
            Assert.That(result1.Count(), Is.EqualTo(1));
            Assert.That(result2.Count(), Is.EqualTo(2));
        });
    }

    [Test]
    public async Task ShouldUpdate()
    {
        var (client, employee, equipment) = GetMocks();
        var newAgreement = new Agreement
        {
            EmployeeId = employee.Id,
            IsActive = true,
            ClientId = client.Id,
            Deposit = 100,
            TransportToPrice = 200,
            Equipment = new List<Equipment> { equipment }
        };
        var id = _appDbContext.Agreements.Add(newAgreement)
            .Entity.Id;
        await _appDbContext.SaveChangesAsync();
        Assert.That(_appDbContext.Agreements.Count(), Is.EqualTo(1));
        newAgreement.IsActive = false;
        newAgreement.Deposit = 250;
        var result = await _agreementRepository.UpdateAsync(newAgreement, id);
        Assert.Multiple(() => {
            Assert.That(result.IsActive, Is.False);
            Assert.That(result.Equipment, Has.Count.EqualTo(1));
            Assert.That(result.Deposit, Is.EqualTo(250));
        });
    }

    private Tuple<Client, Employee, Equipment> GetMocks()
    {
        var client = _appDbContext.Clients.First();
        var employee = _appDbContext.Employees.First();
        var equipment = _appDbContext.Equipment.First();

        if (client == null || employee == null || equipment == null)
        {
            throw new Exception("No mocks in database");
        }

        return Tuple.Create(client, employee, equipment);
    }
}