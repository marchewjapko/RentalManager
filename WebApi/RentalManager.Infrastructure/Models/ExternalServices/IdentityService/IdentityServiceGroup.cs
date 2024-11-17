using System.Text.Json.Serialization;

namespace RentalManager.Infrastructure.Models.ExternalServices.IdentityService;

public class IdentityServiceGroup
{
    [JsonPropertyName("name")]
    public required string Name { get; init; }
}