using System.Security.Claims;
using RentalManager.Global.Queries;
using RentalManager.Infrastructure.Commands.ClientCommands;
using RentalManager.Infrastructure.DTO;

namespace RentalManager.Infrastructure.Services.Interfaces;

public interface IClientService
{
    Task AddAsync(CreateClient createClient, ClaimsPrincipal user);
    Task<ClientDto> GetAsync(int id);
    Task DeleteAsync(int id);
    Task UpdateAsync(UpdateClient updateClient, int id);

    Task<IEnumerable<ClientDto>> BrowseAllAsync(QueryClients queryClients);

    Task Deactivate(int id);
}