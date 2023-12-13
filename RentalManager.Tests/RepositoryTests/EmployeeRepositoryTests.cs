using Bogus;
using Microsoft.EntityFrameworkCore;
using RentalManager.Core.Domain;
using RentalManager.Infrastructure.Exceptions;
using RentalManager.Infrastructure.Repositories;

namespace RentalManager.Tests.RepositoryTests;

public class EmployeeRepositoryTests
{
    private AppDbContext _appDbContext = null!;
    private EmployeeRepository _employeeRepository = null!;
    
    private readonly Employee _mockEmployee = new Faker<Employee>()
        .RuleFor(x => x.Name, (f) => f.Name.FirstName())
        .RuleFor(x => x.Surname, (f) => f.Name.LastName())
        .RuleFor(x => x.UserName, (f) => f.Internet.UserName())
        .RuleFor(x => x.Gender, () => Gender.Man)
        .RuleFor(x => x.DateAdded, () => DateTime.Now)
        .Generate();

    [SetUp]
    public void Setup()
    {
        var optionsBuilder =
            new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase("TestingDatabase");
        _appDbContext = new AppDbContext(optionsBuilder.Options);
        _employeeRepository = new EmployeeRepository(_appDbContext);

        Assume.That(_appDbContext.Employees.Count(), Is.EqualTo(0));
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
        var result = await _employeeRepository.AddAsync(_mockEmployee);

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
        _appDbContext.Add(_mockEmployee);
        await _appDbContext.SaveChangesAsync();
        Assume.That(_appDbContext.Employees.Count(), Is.EqualTo(1));

        // act
        await _employeeRepository.DeleteAsync(1);

        // assert
        Assert.That(_appDbContext.Employees.Count(), Is.EqualTo(0));
    }

    [Test]
    public void ShouldNotDelete()
    {
        Assert.ThrowsAsync<EmployeeNotFoundException>(async () =>
            await _employeeRepository.DeleteAsync(1));
    }

    [Test]
    public async Task ShouldGet()
    {
        // arrange
        _appDbContext.Add(_mockEmployee);
        await _appDbContext.SaveChangesAsync();

        // act
        var result = await _employeeRepository.GetAsync(1);

        // assert
        Assert.That(result.Id, Is.EqualTo(1));
    }

    [Test]
    public void ShouldNotGetEmployee()
    {
        Assert.ThrowsAsync<EmployeeNotFoundException>(async () =>
            await _employeeRepository.GetAsync(1));
    }

    [Test]
    public async Task ShouldBrowseAll()
    {
        // arrange
        var newEmployee2 = new Faker<Employee>()
            .RuleFor(x => x.Id, () => 2)
            .RuleFor(x => x.Name, (f) => f.Name.FirstName())
            .RuleFor(x => x.Surname, (f) => f.Name.LastName())
            .RuleFor(x => x.UserName, (f) => f.Internet.UserName())
            .RuleFor(x => x.Gender, () => Gender.Man)
            .RuleFor(x => x.DateAdded, () => DateTime.Now)
            .Generate();

        var xx = _appDbContext.Employees.ToList();
        
        _appDbContext.Add(_mockEmployee);
        _appDbContext.Add(newEmployee2);
        await _appDbContext.SaveChangesAsync();

        // act
        var result = await _employeeRepository.BrowseAllAsync();

        // assert
        Assert.That(result.Count(), Is.EqualTo(2));
    }

    [Test]
    public async Task ShouldFilter_byName()
    {
        // arrange
        var newEmployee2 = new Faker<Employee>()
            .RuleFor(x => x.Id, () => 2)
            .RuleFor(x => x.Name, (f) => f.Name.FirstName())
            .RuleFor(x => x.Surname, (f) => f.Name.LastName())
            .RuleFor(x => x.UserName, (f) => f.Internet.UserName())
            .RuleFor(x => x.Gender, () => Gender.Man)
            .RuleFor(x => x.DateAdded, () => DateTime.Now)
            .Generate();
        
        _appDbContext.Add(_mockEmployee);
        _appDbContext.Add(newEmployee2);
        await _appDbContext.SaveChangesAsync();

        // act
        var result = (await _employeeRepository.BrowseAllAsync(_mockEmployee.Name)).ToList();

        // assert
        Assert.Multiple(() => {
            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result[0].Name, Is.EqualTo(_mockEmployee.Name));
        });
    }

    [Test]
    public async Task ShouldUpdate()
    {
        // arrange
        _appDbContext.Add(_mockEmployee);
        await _appDbContext.SaveChangesAsync();
        _mockEmployee.Name = "NEW TEST NAME";

        // act
        await _employeeRepository.UpdateAsync(_mockEmployee, 1);

        // assert
        var updatedEmployee = _appDbContext.Employees.First();
        Assert.That(updatedEmployee.Name, Is.EqualTo("NEW TEST NAME"));
    }
}