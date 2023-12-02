using Microsoft.EntityFrameworkCore;
using RentalManager.Core.Domain;
using RentalManager.Infrastructure.Repositories;

namespace RentalManager.Tests.RepositoryTests;

public class RentalAgreementRepositoryTests
{
    private readonly Client _mockClient;
    private readonly Employee _mockEmployee;
    private readonly RentalEquipment _mockRentalEquipment;
    private AppDbContext _appDbContext = null!;
    private RentalAgreementRepository _rentalAgreementRepository = null!;

    public RentalAgreementRepositoryTests()
    {
        _mockClient = new Client
        {
            Name = "Test Name 1",
            Surname = "Test Surname 1",
            City = "Test City 1",
            Street = "Test street 1",
            IdCard = "ABC 123456",
            PhoneNumber = "111 111 111"
        };
        _mockEmployee = new Employee
        {
            Name = "Test Name",
            Surname = "Test Surname"
        };
        _mockRentalEquipment = new RentalEquipment
        {
            Name = "Test Name",
            Price = 100
        };
    }

    [SetUp]
    public void Setup()
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase("TestingDatabase");
        _appDbContext = new AppDbContext(optionsBuilder.Options);
        _rentalAgreementRepository = new RentalAgreementRepository(_appDbContext);
        _appDbContext.Clients.Add(_mockClient);
        _appDbContext.Employees.Add(_mockEmployee);
        _appDbContext.RentalEquipment.Add(_mockRentalEquipment);
        _appDbContext.SaveChanges();

        Assume.That(_appDbContext.RentalAgreements.Count(), Is.EqualTo(0));
    }

    [TearDown]
    public void TearDown()
    {
        _appDbContext.RentalAgreements.RemoveRange(_appDbContext.RentalAgreements);
        _appDbContext.SaveChanges();
        _appDbContext.Database.EnsureDeleted();
        _appDbContext.Dispose();
    }

    [Test]
    public async Task ShouldAdd()
    {
        var (client, employee, rentalEquipment) = GetMocks();
        var newAgreement = new RentalAgreement
        {
            EmployeeId = employee.Id,
            IsActive = true,
            ClientId = client.Id,
            Deposit = 100,
            TransportToPrice = 200,
            RentalEquipment = new List<RentalEquipment> { rentalEquipment }
        };
        var result = await _rentalAgreementRepository.AddAsync(newAgreement);
        Assert.Multiple(() => {
            Assert.That(result.Id, Is.EqualTo(1));
            Assert.That(result.Employee, Is.Not.Null);
            Assert.That(result.Client, Is.Not.Null);
            Assert.That(result.RentalEquipment, Has.Count.EqualTo(1));
            Assert.That(result.Deposit, Is.EqualTo(100));
            Assert.That(result.TransportToPrice, Is.EqualTo(200));
        });
    }

    [Test]
    public async Task ShouldGet()
    {
        var (client, employee, rentalEquipment) = GetMocks();
        var newAgreement = new RentalAgreement
        {
            EmployeeId = employee.Id,
            IsActive = true,
            ClientId = client.Id,
            Deposit = 100,
            TransportToPrice = 200,
            RentalEquipment = new List<RentalEquipment> { rentalEquipment }
        };
        _appDbContext.RentalAgreements.Add(newAgreement);
        await _appDbContext.SaveChangesAsync();
        var result = await _rentalAgreementRepository.GetAsync(1);
        Assert.Multiple(() => {
            Assert.That(result.Id, Is.EqualTo(1));
            Assert.That(result.Employee, Is.Not.Null);
            Assert.That(result.Client, Is.Not.Null);
            Assert.That(result.RentalEquipment, Has.Count.EqualTo(1));
            Assert.That(result.Deposit, Is.EqualTo(100));
            Assert.That(result.TransportToPrice, Is.EqualTo(200));
        });
    }

    [Test]
    public void ShouldNotGet()
    {
        var ex = Assert.ThrowsAsync<Exception>(async () => await _rentalAgreementRepository.GetAsync(1));
        Assert.That(ex.Message, Is.EqualTo("Unable to find rental agreement"));
    }

    [Test]
    public async Task ShouldDelete()
    {
        var (client, employee, rentalEquipment) = GetMocks();
        var newAgreement = new RentalAgreement
        {
            EmployeeId = employee.Id,
            IsActive = true,
            ClientId = client.Id,
            Deposit = 100,
            TransportToPrice = 200,
            RentalEquipment = new List<RentalEquipment> { rentalEquipment }
        };
        _appDbContext.RentalAgreements.Add(newAgreement);
        await _appDbContext.SaveChangesAsync();
        Assert.That(_appDbContext.RentalAgreements.Count(), Is.EqualTo(1));
        await _rentalAgreementRepository.DeleteAsync(1);
        Assert.That(_appDbContext.RentalAgreements.Count(), Is.EqualTo(0));
    }

    [Test]
    public void ShouldNotDelete()
    {
        var ex = Assert.ThrowsAsync<Exception>(async () => await _rentalAgreementRepository.DeleteAsync(1));
        Assert.That(ex.Message, Is.EqualTo("Unable to find rental agreement"));
    }

    [Test]
    public async Task ShouldBrowseAll()
    {
        var (client, employee, rentalEquipment) = GetMocks();
        var newAgreement = new RentalAgreement
        {
            EmployeeId = employee.Id,
            IsActive = true,
            ClientId = client.Id,
            Deposit = 100,
            TransportToPrice = 200,
            RentalEquipment = new List<RentalEquipment> { rentalEquipment }
        };
        var newAgreement2 = new RentalAgreement
        {
            EmployeeId = employee.Id,
            IsActive = true,
            ClientId = client.Id,
            Deposit = 200,
            TransportToPrice = 300,
            RentalEquipment = new List<RentalEquipment> { rentalEquipment }
        };
        _appDbContext.RentalAgreements.Add(newAgreement);
        _appDbContext.RentalAgreements.Add(newAgreement2);
        await _appDbContext.SaveChangesAsync();
        Assert.That(_appDbContext.RentalAgreements.Count(), Is.EqualTo(2));
        var result = await _rentalAgreementRepository.BrowseAllAsync();
        Assert.That(result.Count(), Is.EqualTo(2));
    }

    [Test]
    public async Task ShouldFilter()
    {
        var (client, employee, rentalEquipment) = GetMocks();
        var newAgreement = new RentalAgreement
        {
            EmployeeId = employee.Id,
            IsActive = true,
            ClientId = client.Id,
            Deposit = 100,
            TransportToPrice = 200,
            RentalEquipment = new List<RentalEquipment> { rentalEquipment }
        };
        _appDbContext.RentalAgreements.Add(newAgreement);

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
        var newRentalEquipment = new RentalEquipment
        {
            Name = "Test Name 2",
            Price = 200
        };
        var clientEntry = _appDbContext.Clients.Add(newClient);
        var employeeEntry = _appDbContext.Employees.Add(newEmployee);
        var rentalEquipmentEntry = _appDbContext.RentalEquipment.Add(newRentalEquipment);
        var newAgreement2 = new RentalAgreement
        {
            EmployeeId = employeeEntry.Entity.Id,
            IsActive = true,
            ClientId = clientEntry.Entity.Id,
            Deposit = 100,
            TransportToPrice = 200,
            RentalEquipment = new List<RentalEquipment> { rentalEquipmentEntry.Entity }
        };
        _appDbContext.RentalAgreements.Add(newAgreement);
        _appDbContext.RentalAgreements.Add(newAgreement2);
        await _appDbContext.SaveChangesAsync();
        Assert.That(_appDbContext.RentalAgreements.Count(), Is.EqualTo(2));

        var result1 = await _rentalAgreementRepository.BrowseAllAsync(null, "Test Surname 2");
        Assert.That(result1.Count(), Is.EqualTo(1));
        var result2 = await _rentalAgreementRepository.BrowseAllAsync(null, null, null, "Test City 1");
        Assert.That(result2.Count(), Is.EqualTo(2));
    }

    [Test]
    public async Task ShouldUpdate()
    {
        var (client, employee, rentalEquipment) = GetMocks();
        var newAgreement = new RentalAgreement
        {
            EmployeeId = employee.Id,
            IsActive = true,
            ClientId = client.Id,
            Deposit = 100,
            TransportToPrice = 200,
            RentalEquipment = new List<RentalEquipment> { rentalEquipment }
        };
        var id = _appDbContext.RentalAgreements.Add(newAgreement).Entity.Id;
        await _appDbContext.SaveChangesAsync();
        Assert.That(_appDbContext.RentalAgreements.Count(), Is.EqualTo(1));
        newAgreement.IsActive = false;
        newAgreement.Deposit = 250;
        var result = await _rentalAgreementRepository.UpdateAsync(newAgreement, id);
        Assert.Multiple(() => {
            Assert.That(result.IsActive, Is.False);
            Assert.That(result.RentalEquipment, Has.Count.EqualTo(1));
            Assert.That(result.Deposit, Is.EqualTo(250));
        });
    }

    private Tuple<Client, Employee, RentalEquipment> GetMocks()
    {
        var client = _appDbContext.Clients.First();
        var employee = _appDbContext.Employees.First();
        var rentalEquipment = _appDbContext.RentalEquipment.First();

        if (client == null || employee == null || rentalEquipment == null) throw new Exception("No mocks in database");

        return Tuple.Create(client, employee, rentalEquipment);
    }
}