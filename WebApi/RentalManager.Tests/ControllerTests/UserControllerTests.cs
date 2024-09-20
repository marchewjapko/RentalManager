using System.Security.Claims;
using Bogus;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RentalManager.Infrastructure.Models.DTO;
using RentalManager.Infrastructure.Services.UserService;
using RentalManager.WebAPI.Controllers;

namespace RentalManager.Tests.ControllerTests;

public class UserControllerTests
{
    [Test]
    public async Task BrowseAllUsers_ShouldReturnAllUsers()
    {
        // Arrange
        var userService = new Mock<IUserService>();
        userService.Setup(x => x.BrowseAllAsync())
            .ReturnsAsync(new Faker<UserDto>().Generate(5));

        var controller = new UserController(userService.Object);

        // Act
        var result = await controller.BrowseAllUsers() as OkObjectResult;

        // Assert
        Assert.Multiple(() => {
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.StatusCode, Is.EqualTo(200));
            Assert.That(result.Value, Is.Not.Null);
            Assert.That(result.Value, Is.TypeOf<List<UserDto>>());
        });
    }

    [Test]
    public async Task GetUser_ShouldReturnUser()
    {
        // Arrange
        var userService = new Mock<IUserService>();
        userService.Setup(x => x.GetAsync(It.IsAny<int>()))
            .ReturnsAsync(new Faker<UserDto>().Generate());

        var controller = new UserController(userService.Object);

        // Act
        var result = await controller.GetUser(1) as OkObjectResult;

        // Assert
        Assert.Multiple(() => {
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.StatusCode, Is.EqualTo(200));
            Assert.That(result.Value, Is.Not.Null);
            Assert.That(result.Value, Is.TypeOf<UserDto>());
        });
    }

    [Test]
    public async Task WhoAmI_ShouldReturnMe()
    {
        // Arrange
        var context = new DefaultHttpContext();
        context.User.AddIdentity(new ClaimsIdentity([
            new Claim("id", new Faker().Random.Int()
                .ToString()),
            new Claim("first_name", new Faker().Person.FirstName),
            new Claim("last_name", new Faker().Person.LastName),
            new Claim("preferred_username", new Faker().Internet.UserName()),
            new Claim("groups", new Faker().Lorem.Word()),
            new Claim("groups", new Faker().Lorem.Word())
        ]));

        var userService = new Mock<IUserService>();

        var controller = new UserController(userService.Object);

        controller.ControllerContext = new ControllerContext
        {
            HttpContext = context
        };

        // Act
        var result = controller.WhoAmI() as OkObjectResult;

        // Assert
        Assert.Multiple(() => {
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.StatusCode, Is.EqualTo(200));
            Assert.That(result.Value, Is.Not.Null);
            Assert.That(result.Value, Is.TypeOf<UserDto>());
        });
    }
}