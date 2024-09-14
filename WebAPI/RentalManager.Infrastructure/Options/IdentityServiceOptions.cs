namespace RentalManager.Infrastructure.Options;

public class IdentityServiceOptions
{
    public const string IdentityService = "IdentityService";

    public string Host { get; init; } = string.Empty;

    public string Scheme { get; init; } = string.Empty;

    public string PathPrefix { get; init; } = string.Empty;

    public string ApiKey { get; init; } = string.Empty;

    /// <summary>
    ///     Groups the user needs to be a member of to be considered this app's user
    /// </summary>
    public IEnumerable<string> AppGroups { get; init; } = new List<string>();

    /// <summary>
    ///     Path for GET /core/users/
    /// </summary>
    public string GetUsersPath { get; init; } = string.Empty;

    /// <summary>
    ///     Path for GET /core/users/{id}/
    /// </summary>
    public string GetUserPath { get; init; } = string.Empty;
}