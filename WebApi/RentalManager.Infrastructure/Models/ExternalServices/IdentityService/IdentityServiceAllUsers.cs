using System.Text.Json.Serialization;

namespace RentalManager.Infrastructure.Models.ExternalServices.IdentityService;

public class IdentityServiceAllUsers
{
    [JsonPropertyName("results")]
    public required IEnumerable<IdentityServiceUser> Results { get; init; }
}