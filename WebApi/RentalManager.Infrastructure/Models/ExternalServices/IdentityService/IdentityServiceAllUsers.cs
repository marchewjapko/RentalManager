using System.Text.Json.Serialization;

namespace RentalManager.Infrastructure.Models.ExternalServices.IdentityService;

public class IdentityServiceAllUsers
{
    [JsonPropertyName("results")]
    public IEnumerable<IdentityServiceUser> Results { get; init; }
}