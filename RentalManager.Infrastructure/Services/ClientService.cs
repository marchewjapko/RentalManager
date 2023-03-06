using RentalManager.Core.Repositories;
using RentalManager.Infrastructure.Commands;
using RentalManager.Infrastructure.DTO;

namespace RentalManager.Infrastructure.Services;

public class ClientService : IClientService
{
    private readonly IClientRepository _clientRepository;

    public ClientService(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public async Task<ClientDto> AddAsync(CreateClient createClient)
    {
        var result = await _clientRepository.AddAsync(createClient.ToDomain());
        return result.ToDto();
    }

    public async Task<IEnumerable<ClientDto>> BrowseAllAsync(string? name = null, string? surname = null,
        string? phoneNumber = null, string? email = null, string? idCard = null, string? city = null,
        string? street = null, DateTime? from = null, DateTime? to = null)
    {
        var result =
            await _clientRepository.BrowseAllAsync(name, surname, phoneNumber, email, idCard, city, street, from, to);
        return await Task.FromResult(result.Select(x => x.ToDto()));
    }

    public async Task DeleteAsync(int id)
    {
        await _clientRepository.DeleteAsync(id);
    }

    public async Task<ClientDto> GetAsync(int id)
    {
        var result = await _clientRepository.GetAsync(id);
        return await Task.FromResult(result.ToDto());
    }

    public async Task<ClientDto> UpdateAsync(UpdateClient updateClient, int id)
    {
        var result = await _clientRepository.UpdateAsync(updateClient.ToDomain(), id);
        return await Task.FromResult(result.ToDto());
    }
}