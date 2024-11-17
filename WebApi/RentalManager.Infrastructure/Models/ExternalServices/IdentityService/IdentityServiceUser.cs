using System.Text.Json.Serialization;

namespace RentalManager.Infrastructure.Models.ExternalServices.IdentityService;

public class IdentityServiceUser
{
    [JsonPropertyName("pk")]
    public int Pk { get; init; }

    [JsonPropertyName("username")]
    public required string UserName { get; init; }

    [JsonPropertyName("groups_obj")]
    public required IEnumerable<IdentityServiceGroup> Groups { get; init; }

    [JsonPropertyName("type")]
    public required string Type { get; init; }

    [JsonPropertyName("attributes")]
    public required Dictionary<string, object> Attributes { get; init; }
}