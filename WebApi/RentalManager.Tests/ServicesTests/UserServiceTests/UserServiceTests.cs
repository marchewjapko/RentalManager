using System.Net;
using AutoMapper;
using Bogus;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using RentalManager.Infrastructure.Exceptions;
using RentalManager.Infrastructure.Models.DTO;
using RentalManager.Infrastructure.Models.ExternalServices.IdentityService;
using RentalManager.Infrastructure.Options;
using RentalManager.Infrastructure.Services.UserService;

namespace RentalManager.Tests.ServicesTests.UserServiceTests;

public class UserServiceTests
{
    [Test]
    public async Task ShouldGet()
    {
        // Arrange
        var httpClientFactoryMock = SetupHttpClientFactory("SendAsync", JsonResponses.GetUserByIdResponse());
        var options = SetupOptions();

        var expectedUser = new UserDto()
        {
            Id = 1,
            FirstName = "John",
            LastName = "Kowalski",
            UserName = "JohnKowalski",
            Roles = new List<string>()
            {
                "House M.D. Fans",
                "Cuddy Fans"
            }
        };

        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(m =>
                m.Map<UserDto>(It.IsAny<IdentityServiceUser>()))
            .Returns(expectedUser);

        var userService = new UserService(options, mapperMock.Object, httpClientFactoryMock.Object);

        // Act
        var result = await userService.GetAsync(1);
        
        // Assert
        Assert.Multiple(() => {
            Assert.That(result.Id, Is.EqualTo(expectedUser.Id));
            Assert.That(result.FirstName, Is.EqualTo(expectedUser.FirstName));
            Assert.That(result.LastName, Is.EqualTo(expectedUser.LastName));
            Assert.That(result.UserName, Is.EqualTo(expectedUser.UserName));
        });
    }
    
    [Test]
    public void ShouldNotGet_UserNotInGroup()
    {
        // Arrange
        var httpClientFactoryMock = SetupHttpClientFactory("SendAsync", JsonResponses.GetUserByIdResponse());
        
        var options = Options.Create(new IdentityServiceOptions
        {
            Host = new Faker().Internet.DomainName(),
            Scheme = new Faker().Internet.Protocol(),
            ApiKey = Guid.NewGuid()
                .ToString(),
            AppGroups = new List<string>
            {
                "House M.D. Haters"
            },
            PathPrefix = "api/",
            GetUserPath = "users/{id}/",
            GetUsersPath = "users/"
        });

        var mapperMock = new Mock<IMapper>();

        var userService = new UserService(options, mapperMock.Object, httpClientFactoryMock.Object);

        // Assert
        Assert.ThrowsAsync<UserNotFoundException>(async () =>
            await userService.GetAsync(1));
    }
    
    [Test]
    public void ShouldNotGet_UserNotInternal()
    {
        // Arrange
        var httpClientFactoryMock = SetupHttpClientFactory("SendAsync", JsonResponses.GetUserByIdResponse_NotInternal());
        var options = SetupOptions();

        var mapperMock = new Mock<IMapper>();

        var userService = new UserService(options, mapperMock.Object, httpClientFactoryMock.Object);

        // Assert
        Assert.ThrowsAsync<UserNotFoundException>(async () =>
            await userService.GetAsync(1));
    }
    
    [Test]
    public void ShouldNotGet_UserNotFound()
    {
        // Arrange
        var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.NotFound,
                Content = new StringContent("")
            });

        var client = new HttpClient(mockHttpMessageHandler.Object);

        var httpClientFactoryMock = new Mock<IHttpClientFactory>();
        
        httpClientFactoryMock.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(client);
        
        var options = SetupOptions();

        var mapperMock = new Mock<IMapper>();

        var userService = new UserService(options, mapperMock.Object, httpClientFactoryMock.Object);

        // Assert
        Assert.ThrowsAsync<UserNotFoundException>(async () =>
            await userService.GetAsync(1));
    }
    
    
    [Test]
    public async Task ShouldBrowseAll()
    {
        // Arrange
        var httpClientFactoryMock = SetupHttpClientFactory("SendAsync", JsonResponses.GetUsersResponse());
        var options = SetupOptions();

        var expectedUsers = new List<UserDto>()
        {
            new()
            {
                Id = 1,
                FirstName = "John",
                LastName = "Kowalski",
                UserName = "JohnKowalski",
                Roles = new List<string>()
                {
                    "House M.D. Fans",
                    "Cuddy Fans"
                }
            }
        };
            
        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(m => m.Map<IEnumerable<UserDto>>(It.Is<IEnumerable<IdentityServiceUser>>(users => users.Count() == 1)))
            .Returns(expectedUsers);

        var userService = new UserService(options, mapperMock.Object, httpClientFactoryMock.Object);

        // Act
        var result = await userService.BrowseAllAsync();
        var userDtos = result.ToList();

        // Assert
        Assert.Multiple(() => {
            Assert.That(userDtos, Has.Count.EqualTo(1));
            Assert.That(userDtos[0].Id, Is.EqualTo(expectedUsers[0].Id));
            Assert.That(userDtos[0].FirstName, Is.EqualTo(expectedUsers[0].FirstName));
            Assert.That(userDtos[0].LastName, Is.EqualTo(expectedUsers[0].LastName));
            Assert.That(userDtos[0].UserName, Is.EqualTo(expectedUsers[0].UserName));
        });
    }
    
    
    [Test]
    public async Task ShouldBrowseAll_Multiple_Users()
    {
        // Arrange
        var httpClientFactoryMock = SetupHttpClientFactory("SendAsync", JsonResponses.GetUsersResponse_MultipleUsers());
        var options = SetupOptions();

        var expectedUsers = new List<UserDto>()
        {
            new Faker<UserDto>().Generate(),
            new Faker<UserDto>().Generate()
        };
            
        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(m => m.Map<IEnumerable<UserDto>>(It.Is<IEnumerable<IdentityServiceUser>>(users => users.Count() == 2))).Returns(expectedUsers);

        var userService = new UserService(options, mapperMock.Object, httpClientFactoryMock.Object);

        // Act
        var result = await userService.BrowseAllAsync();
        var userDtos = result.ToList();

        // Assert
        Assert.That(userDtos, Has.Count.EqualTo(2));
    }

    private static Mock<IHttpClientFactory> SetupHttpClientFactory(string action, string jsonResponse)
    {
        var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(action, ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(jsonResponse)
            });

        var client = new HttpClient(mockHttpMessageHandler.Object);

        var result = new Mock<IHttpClientFactory>();
        
        result.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(client);

        return result;
    }

    private static IOptions<IdentityServiceOptions> SetupOptions()
    {
        return Options.Create(new IdentityServiceOptions
        {
            Host = new Faker().Internet.DomainName(),
            Scheme = new Faker().Internet.Protocol(),
            ApiKey = Guid.NewGuid()
                .ToString(),
            AppGroups = new List<string>
            {
                "House M.D. Fans"
            },
            PathPrefix = "api/",
            GetUserPath = "users/{id}/",
            GetUsersPath = "users/"
        });
    }
}