using Bogus;
using Microsoft.EntityFrameworkCore;
using RentalManager.Core.Domain;
using RentalManager.Global.Queries;
using RentalManager.Infrastructure.Exceptions;
using RentalManager.Infrastructure.Repositories;
using RentalManager.Infrastructure.Repositories.DbContext;

namespace RentalManager.Tests.RepositoryTests;

public class UserRepositoryTests
{
    private readonly User _mockUser = new Faker<User>()
        .RuleFor(x => x.Name, f => f.Name.FirstName())
        .RuleFor(x => x.Surname, f => f.Name.LastName())
        .RuleFor(x => x.UserName, f => f.Internet.UserName())
        .RuleFor(x => x.Gender, () => Gender.Man)
        .RuleFor(x => x.CreatedTs, () => DateTime.Now)
        .Generate();

    private AppDbContext _appDbContext = null!;
    private UserRepository _userRepository = null!;

    [SetUp]
    public void Setup()
    {
        var optionsBuilder =
            new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase("TestingDatabase");
        _appDbContext = new AppDbContext(optionsBuilder.Options);
        _userRepository = new UserRepository(_appDbContext);

        Assume.That(_appDbContext.Users.Count(), Is.EqualTo(0));
    }

    [TearDown]
    public void TearDown()
    {
        _appDbContext.Database.EnsureDeleted();
        _appDbContext.Dispose();
    }

    [Test]
    public async Task ShouldDelete()
    {
        // arrange
        _appDbContext.Add(_mockUser);
        await _appDbContext.SaveChangesAsync();

        Assume.That(_appDbContext.Equipment.Count(), Is.EqualTo(1));

        // act
        await _userRepository.DeleteAsync(1);

        // assert
        Assert.That(_appDbContext.Users.Count(), Is.EqualTo(0));
    }

    [Test]
    public void ShouldNotDelete()
    {
        Assert.ThrowsAsync<UserNotFoundException>(async () =>
            await _userRepository.DeleteAsync(1));
    }

    [Test]
    public async Task ShouldGet()
    {
        // arrange
        _appDbContext.Add(_mockUser);
        await _appDbContext.SaveChangesAsync();

        Assume.That(_appDbContext.Equipment.Count(), Is.EqualTo(1));

        // act
        var result = await _userRepository.GetAsync(1);

        // assert
        Assert.That(result.Id, Is.EqualTo(1));
    }

    [Test]
    public void ShouldNotGetUser()
    {
        Assert.ThrowsAsync<UserNotFoundException>(async () =>
            await _userRepository.GetAsync(1));
    }

    [Test]
    public async Task ShouldBrowseAll()
    {
        // arrange
        var newUser2 = new Faker<User>()
            .RuleFor(x => x.Id, () => 2)
            .RuleFor(x => x.Name, f => f.Name.FirstName())
            .RuleFor(x => x.Surname, f => f.Name.LastName())
            .RuleFor(x => x.UserName, f => f.Internet.UserName())
            .RuleFor(x => x.Gender, () => Gender.Man)
            .RuleFor(x => x.CreatedTs, () => DateTime.Now)
            .Generate();

        _appDbContext.Add(_mockUser);
        _appDbContext.Add(newUser2);
        await _appDbContext.SaveChangesAsync();

        Assume.That(_appDbContext.Users.Count(), Is.EqualTo(2));
        Assume.That(_appDbContext.Users.First()
            .Id, Is.EqualTo(1));
        Assume.That(_appDbContext.Users.Skip(1)
            .First()
            .Id, Is.EqualTo(2));

        // act
        var result = await _userRepository.BrowseAllAsync(new QueryUser());

        // assert
        Assert.That(result.Count(), Is.EqualTo(2));
    }

    [Test]
    public async Task ShouldFilter_byName()
    {
        // arrange
        var newUser2 = new Faker<User>()
            .RuleFor(x => x.Id, () => 2)
            .RuleFor(x => x.Name, f => f.Name.FirstName())
            .RuleFor(x => x.Surname, f => f.Name.LastName())
            .RuleFor(x => x.UserName, f => f.Internet.UserName())
            .RuleFor(x => x.Gender, () => Gender.Man)
            .RuleFor(x => x.CreatedTs, () => DateTime.Now)
            .Generate();

        _appDbContext.Add(_mockUser);
        _appDbContext.Add(newUser2);
        await _appDbContext.SaveChangesAsync();

        var query = new QueryUser()
        {
            Name = _mockUser.Name
        };
        
        Assume.That(_appDbContext.Users.Count(), Is.EqualTo(2));
        Assume.That(_appDbContext.Users.First()
            .Id, Is.EqualTo(1));
        Assume.That(_appDbContext.Users.Skip(1)
            .First()
            .Id, Is.EqualTo(2));

        // act
        var result = (await _userRepository.BrowseAllAsync(query)).ToList();

        // assert
        Assert.Multiple(() => {
            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result[0].Name, Is.EqualTo(_mockUser.Name));
        });
    }

    [Test]
    public async Task ShouldUpdate()
    {
        // arrange
        _appDbContext.Add(_mockUser);
        await _appDbContext.SaveChangesAsync();
        _mockUser.Name = "NEW TEST NAME";

        Assume.That(_appDbContext.Users.Count(), Is.EqualTo(1));

        // act
        await _userRepository.UpdateAsync(_mockUser, 1);

        // assert
        var updatedUser = _appDbContext.Users.First();
        Assert.That(updatedUser.Name, Is.EqualTo("NEW TEST NAME"));
    }
}