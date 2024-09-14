using System.Text.Json.Serialization;

namespace RentalManager.Infrastructure.Models.ExternalServices.IdentityService;

public class IdentityServiceGroup
{
    [JsonPropertyName("name")]
    public string Name { get; init; }
}