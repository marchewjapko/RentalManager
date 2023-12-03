using Microsoft.EntityFrameworkCore;
using RentalManager.Core.Domain;
using RentalManager.Infrastructure.Exceptions;
using RentalManager.Infrastructure.Repositories;

namespace RentalManager.Tests.RepositoryTests;

public class EmployeeRepositoryTests
{
    private AppDbContext _appDbContext = null!;
    private EmployeeRepository _employeeRepository = null!;

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
        // arrange
        var newEmployee = new Employee
        {
            Name = "Test Name",
            Surname = "Test Surname"
        };
        
        // act
        var result = await _employeeRepository.AddAsync(newEmployee);
        
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
        var newEmployee = new Employee
        {
            Name = "Test Name",
            Surname = "Test Surname"
        };
        _appDbContext.Add(newEmployee);
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
        var newEmployee = new Employee
        {
            Name = "Test Name",
            Surname = "Test Surname"
        };
        _appDbContext.Add(newEmployee);
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
        var newEmployee1 = new Employee
        {
            Name = "Test Name 1",
            Surname = "Test Surname 1"
        };
        var newEmployee2 = new Employee
        {
            Name = "Test Name 2",
            Surname = "Test Surname 2"
        };
        _appDbContext.Add(newEmployee1);
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
        var newEmployee1 = new Employee
        {
            Name = "Test Name 1",
            Surname = "Test Surname 1"
        };
        var newEmployee2 = new Employee
        {
            Name = "Test Name 2",
            Surname = "Test Surname 2"
        };
        _appDbContext.Add(newEmployee1);
        _appDbContext.Add(newEmployee2);
        await _appDbContext.SaveChangesAsync();
        
        // act
        var result = (await _employeeRepository.BrowseAllAsync("Test Name 1")).ToList();
        
        // assert
        Assert.Multiple(() => {
            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result[0].Name, Is.EqualTo("Test Name 1"));
        });
    }

    [Test]
    public async Task ShouldUpdate()
    {
        // arrange
        var newEmployee = new Employee
        {
            Name = "Test Name 1",
            Surname = "Test Surname 1"
        };
        _appDbContext.Add(newEmployee);
        await _appDbContext.SaveChangesAsync();
        newEmployee.Name = "NEW TEST NAME";
        
        // act
        await _employeeRepository.UpdateAsync(newEmployee, 1);
        
        // assert
        var updatedEmployee = _appDbContext.Employees.First();
        Assert.That(updatedEmployee.Name, Is.EqualTo("NEW TEST NAME"));
    }
}