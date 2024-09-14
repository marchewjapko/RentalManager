using System.Security.Claims;
using AutoMapper;
using RentalManager.Core.Domain;
using RentalManager.Core.Repositories;
using RentalManager.Global.Queries;
using RentalManager.Infrastructure.Extensions;
using RentalManager.Infrastructure.Models.Commands.ClientCommands;
using RentalManager.Infrastructure.Models.DTO;

namespace RentalManager.Infrastructure.Services.ClientService;

public class ClientService(IClientRepository clientRepository, IMapper mapper) : IClientService
{
    public async Task<ClientDto> AddAsync(CreateClient createClient, ClaimsPrincipal user)
    {
        var newClient = mapper.Map<Client>(createClient);
        newClient.CreatedBy = user.GetId();

        var result = await clientRepository.AddAsync(newClient);

        return mapper.Map<ClientDto>(result);
    }

    public async Task<IEnumerable<ClientDto>> BrowseAllAsync(QueryClients queryClients)
    {
        var result =
            await clientRepository.BrowseAllAsync(queryClients);

        return mapper.Map<IEnumerable<ClientDto>>(result);
    }

    public async Task DeleteAsync(int id)
    {
        await clientRepository.DeleteAsync(id);
    }

    public async Task<ClientDto> GetAsync(int id)
    {
        var result = await clientRepository.GetAsync(id);

        return mapper.Map<ClientDto>(result);
    }

    public async Task<ClientDto> UpdateAsync(UpdateClient updateClient, int id)
    {
        var result = await clientRepository.UpdateAsync(mapper.Map<Client>(updateClient), id);

        return mapper.Map<ClientDto>(result);
    }

    public async Task Deactivate(int id)
    {
        await clientRepository.Deactivate(id);
    }
}