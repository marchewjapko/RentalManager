using RentalManager.Core.Domain;

namespace RentalManager.Core.Repositories;

public interface IClientRepository
{
    Task<Client> AddAsync(Client client);
    Task<Client> GetAsync(int id);
    Task DeleteAsync(int id);
    Task<Client> UpdateAsync(Client client, int id);

    Task<IEnumerable<Client>> BrowseAllAsync(string? name = null,
        string? surname = null,
        string? phoneNumber = null,
        string? email = null,
        string? idCard = null,
        string? city = null,
        string? street = null,
        DateTime? from = null,
        DateTime? to = null);
}