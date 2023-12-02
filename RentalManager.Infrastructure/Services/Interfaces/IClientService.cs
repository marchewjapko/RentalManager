using RentalManager.Infrastructure.Commands.ClientCommands;
using RentalManager.Infrastructure.DTO;

namespace RentalManager.Infrastructure.Services;

public interface IClientService
{
    Task<ClientDto> AddAsync(CreateClient createClient);
    Task<ClientDto> GetAsync(int id);
    Task DeleteAsync(int id);
    Task<ClientDto> UpdateAsync(UpdateClient updateClient, int id);

    Task<IEnumerable<ClientDto>> BrowseAllAsync(string? name = null,
        string? surname = null,
        string? phoneNumber = null,
        string? email = null,
        string? idCard = null,
        string? city = null,
        string? street = null,
        DateTime? from = null,
        DateTime? to = null);
}