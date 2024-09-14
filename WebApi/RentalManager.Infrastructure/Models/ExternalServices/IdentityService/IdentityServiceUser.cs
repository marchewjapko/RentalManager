using System.Text.Json.Serialization;

namespace RentalManager.Infrastructure.Models.ExternalServices.IdentityService;

public class IdentityServiceUser
{
    [JsonPropertyName("pk")]
    public int Pk { get; init; }

    [JsonPropertyName("username")]
    public string UserName { get; init; }

    [JsonPropertyName("groups_obj")]
    public IEnumerable<IdentityServiceGroup> Groups { get; init; }

    [JsonPropertyName("type")]
    public string Type { get; init; }

    [JsonPropertyName("attributes")]
    public Dictionary<string, object> Attributes { get; init; }
}