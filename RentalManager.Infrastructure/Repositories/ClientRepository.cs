using Microsoft.EntityFrameworkCore;
using RentalManager.Core.Domain;
using RentalManager.Core.Repositories;
using RentalManager.Global.Queries;
using RentalManager.Infrastructure.Exceptions;
using RentalManager.Infrastructure.Repositories.DbContext;

namespace RentalManager.Infrastructure.Repositories;

public class ClientRepository(AppDbContext appDbContext) : IClientRepository
{
    public async Task AddAsync(Client client)
    {
        appDbContext.Clients.Add(client);
        await appDbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var result = await appDbContext.Clients.FirstOrDefaultAsync(client => client.Id == id);

        if (result == null)
        {
            throw new ClientNotFoundException(id);
        }

        appDbContext.Clients.Remove(result);
        await appDbContext.SaveChangesAsync();
    }

    public async Task<Client> GetAsync(int id)
    {
        var result = await Task.FromResult(appDbContext.Clients
            .FirstOrDefault(x => x.Id == id));

        if (result == null)
        {
            throw new ClientNotFoundException(id);
        }

        return result;
    }

    public async Task<IEnumerable<Client>> BrowseAllAsync(QueryClients queryClients)
    {
        var result = appDbContext.Clients.Where(x => x.IsActive)
            .AsQueryable();
        if (queryClients.Name != null)
        {
            result = result.Where(x => x.Name.Contains(queryClients.Name));
        }

        if (queryClients.Surname != null)
        {
            result = result.Where(x => x.Surname.Contains(queryClients.Surname));
        }

        if (queryClients.PhoneNumber != null)
        {
            result = result.Where(x => x.PhoneNumber == queryClients.PhoneNumber);
        }

        if (queryClients.Email != null)
        {
            result = result.Where(x => x.Email != null && x.Email.Contains(queryClients.Email));
        }

        if (queryClients.IdCard != null)
        {
            result = result.Where(x => x.IdCard != null && x.IdCard.Contains(queryClients.IdCard));
        }

        if (queryClients.City != null)
        {
            result = result.Where(x => x.City.Contains(queryClients.City));
        }

        if (queryClients.Street != null)
        {
            result = result.Where(x => x.Street.Contains(queryClients.Street));
        }

        if (queryClients.From != null)
        {
            result = result.Where(x => x.CreatedTs.Date > queryClients.From.Value.Date);
        }

        if (queryClients.To != null)
        {
            result = result.Where(x => x.CreatedTs.Date < queryClients.To.Value.Date);
        }

        if (queryClients.OnlyActive)
        {
            result = result.Where(x => x.IsActive);
        }

        return await Task.FromResult(result.AsEnumerable());
    }

    public async Task UpdateAsync(Client client, int id)
    {
        var clientToUpdate = appDbContext.Clients.FirstOrDefault(x => x.Id == id);

        if (clientToUpdate == null)
        {
            throw new ClientNotFoundException(id);
        }

        clientToUpdate.Name = client.Name;
        clientToUpdate.Surname = client.Surname;
        clientToUpdate.PhoneNumber = client.PhoneNumber;
        clientToUpdate.Email = client.Email;
        clientToUpdate.IdCard = client.IdCard;
        clientToUpdate.City = client.City;
        clientToUpdate.Street = client.Street;
        clientToUpdate.UpdatedTs = DateTime.Now;
        await appDbContext.SaveChangesAsync();
    }

    public async Task Deactivate(int id)
    {
        var result = await appDbContext.Clients.FirstOrDefaultAsync(client => client.Id == id);

        if (result == null)
        {
            throw new ClientNotFoundException(id);
        }

        result.IsActive = false;
        await appDbContext.SaveChangesAsync();
    }
}