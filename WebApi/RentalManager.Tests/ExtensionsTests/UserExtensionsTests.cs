using System.Security.Claims;
using Bogus;
using RentalManager.Infrastructure.ExceptionHandling.Exceptions;
using RentalManager.Infrastructure.Extensions;

namespace RentalManager.Tests.ExtensionsTests;

public class UserExtensionsTests
{
    [Test]
    public void ShouldGetIdFromClaim()
    {
        // Arrange
        var claims = new List<Claim>
        {
            new("id", "5")
        };

        var principal = new ClaimsPrincipal(new ClaimsIdentity(claims));

        // Act
        var result = principal.GetId();

        // Assert
        Assert.That(result, Is.EqualTo(5));
    }

    [Test]
    public void ShouldNotGetIdFromClaim_NoClaim()
    {
        // Arrange
        var principal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>()));

        // Assert
        Assert.Throws<UserDoesNotHaveIdClaimException>(() =>
            principal.GetId());
    }

    [Test]
    public void ShouldNotGetIdFromClaim_InvalidClaim()
    {
        // Arrange
        var claims = new List<Claim>
        {
            new("id", new Faker().Random.Word())
        };

        var principal = new ClaimsPrincipal(new ClaimsIdentity(claims));

        // Assert
        Assert.Throws<UserIdClaimInvalidException>(() =>
            principal.GetId());
    }
}