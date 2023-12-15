using RentalManager.Core.Domain;
using RentalManager.Global.Queries;

namespace RentalManager.Core.Repositories;

public interface IClientRepository
{
    Task AddAsync(Client client);

    Task<Client> GetAsync(int id);

    Task DeleteAsync(int id);

    Task UpdateAsync(Client client, int id);

    Task<IEnumerable<Client>> BrowseAllAsync(QueryClients queryClients);

    Task Deactivate(int id);
}