using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using RentalManager.Core.Domain;
using RentalManager.Core.Repositories;
using RentalManager.Global.Queries;
using RentalManager.Infrastructure.Commands.ClientCommands;
using RentalManager.Infrastructure.DTO;
using RentalManager.Infrastructure.DTO.ObjectConversions;
using RentalManager.Infrastructure.Services.Interfaces;

namespace RentalManager.Infrastructure.Services.Implementation;

public class ClientService
    (IClientRepository clientRepository, UserManager<User> userManager) : IClientService
{
    public async Task<ClientDto> AddAsync(CreateClient createClient, ClaimsPrincipal user)
    {
        var newClient = createClient.ToDomain();
        newClient.User = (await userManager.GetUserAsync(user))!;

        var result = await clientRepository.AddAsync(newClient);

        return result.ToDto();
    }

    public async Task<IEnumerable<ClientDto>> BrowseAllAsync(QueryClients queryClients)
    {
        var result =
            await clientRepository.BrowseAllAsync(queryClients);

        return await Task.FromResult(result.Select(x => x.ToDto()));
    }

    public async Task DeleteAsync(int id)
    {
        await clientRepository.DeleteAsync(id);
    }

    public async Task<ClientDto> GetAsync(int id)
    {
        var result = await clientRepository.GetAsync(id);

        return await Task.FromResult(result.ToDto());
    }

    public async Task<ClientDto> UpdateAsync(UpdateClient updateClient, int id)
    {
        var result = await clientRepository.UpdateAsync(updateClient.ToDomain(), id);

        return result.ToDto();
    }

    public async Task Deactivate(int id)
    {
        await clientRepository.Deactivate(id);
    }
}