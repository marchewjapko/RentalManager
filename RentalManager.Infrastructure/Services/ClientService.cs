using System.Text.RegularExpressions;
using RentalManager.Core.Repositories;
using RentalManager.Infrastructure.Commands;
using RentalManager.Infrastructure.DTO;
using RentalManager.Infrastructure.DTO.ObjectConversions;

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
        if (!ValidateClient(createClient.ToDto()))
        {
            throw new Exception("Invalid client");
        }

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
        if (!ValidateClient(updateClient.ToDto()))
        {
            throw new Exception("Invalid client");
        }

        var result = await _clientRepository.UpdateAsync(updateClient.ToDomain(), id);
        return await Task.FromResult(result.ToDto());
    }

    private static bool ValidateClient(ClientDto client)
    {
        if (!client.Name.All(x => char.IsLetter(x) || char.IsWhiteSpace(x)))
        {
            return false;
        }

        if (!client.Surname.All(x => char.IsLetter(x) || char.IsWhiteSpace(x)))
        {
            return false;
        }

        if (!Regex.IsMatch(client.PhoneNumber, @"^([0-9]{3})?[-. ]?([0-9]{3})[-. ]?([0-9]{3})$"))
        {
            return false;
        }

        if (!string.IsNullOrEmpty(client.Email) &&
            !Regex.IsMatch(client.Email, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"))
        {
            return false;
        }

        if (!client.City.All(x => char.IsLetter(x) || char.IsWhiteSpace(x)))
        {
            return false;
        }

        if (client.IdCard != null && !Regex.IsMatch(client.IdCard, @"^([a-zA-Z]){3}[ ]?[0-9]{6}$"))
        {
            return false;
        }

        return true;
    }
}