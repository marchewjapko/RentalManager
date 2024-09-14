using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;
using AutoMapper;
using Microsoft.Extensions.Options;
using RentalManager.Infrastructure.Exceptions;
using RentalManager.Infrastructure.Models.DTO;
using RentalManager.Infrastructure.Models.ExternalServices.IdentityService;
using RentalManager.Infrastructure.Options;

namespace RentalManager.Infrastructure.Services.UserService;

public class UserService(IOptions<IdentityServiceOptions> options, IMapper mapper) : IUserService
{
    private readonly HttpClient _client = new();

    public async Task<IEnumerable<UserWithRolesDto>> BrowseAllAsync()
    {
        var uri = new UriBuilder
        {
            Scheme = options.Value.Scheme,
            Host = options.Value.Host,
            Path = options.Value.PathPrefix + options.Value.GetUsersPath
        };

        var requestMessage = new HttpRequestMessage(HttpMethod.Get, uri.Uri)
        {
            Headers =
            {
                Authorization = new AuthenticationHeaderValue("Bearer", options.Value.ApiKey)
            }
        };

        var request = await _client.SendAsync(requestMessage);

        var jsonResponse = await request.Content.ReadAsStringAsync();

        var identityServiceUsers =
            JsonSerializer.Deserialize<IdentityServiceAllUsers>(jsonResponse)!.Results;

        identityServiceUsers = identityServiceUsers.Where(x => x.Type == "internal");

        identityServiceUsers = identityServiceUsers.Where(x => x.Groups.Select(group => group.Name)
            .Any(groupName => options.Value.AppGroups.Contains(groupName)));

        return mapper.Map<IEnumerable<UserWithRolesDto>>(identityServiceUsers);
    }

    public async Task<UserDto> GetAsync(int id)
    {
        var uri = new UriBuilder
        {
            Scheme = options.Value.Scheme,
            Host = options.Value.Host,
            Path = options.Value.PathPrefix +
                   options.Value.GetUserPath.Replace("{id}", id.ToString())
        };

        var requestMessage = new HttpRequestMessage(HttpMethod.Get, uri.Uri)
        {
            Headers =
            {
                Authorization = new AuthenticationHeaderValue("Bearer", options.Value.ApiKey)
            }
        };

        var request = await _client.SendAsync(requestMessage);

        if (request.StatusCode == HttpStatusCode.NotFound)
        {
            throw new UserNotFoundException(id);
        }

        var jsonResponse = await request.Content.ReadAsStringAsync();

        var user = JsonSerializer.Deserialize<IdentityServiceUser>(jsonResponse);

        if (user!.Type != "internal")
        {
            throw new UserNotFoundException(id);
        }

        if (!user.Groups.Select(x => x.Name)
                .Any(groupName => options.Value.AppGroups.Contains(groupName)))
        {
            throw new UserNotFoundException(id);
        }

        return mapper.Map<UserWithRolesDto>(user);
    }
}