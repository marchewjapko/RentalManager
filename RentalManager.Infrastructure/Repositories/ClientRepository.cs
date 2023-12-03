using Microsoft.EntityFrameworkCore;
using RentalManager.Core.Domain;
using RentalManager.Core.Repositories;
using RentalManager.Infrastructure.Exceptions;

namespace RentalManager.Infrastructure.Repositories;

public class ClientRepository(AppDbContext appDbContext) : IClientRepository
{
    public async Task<Client> AddAsync(Client client)
    {
        appDbContext.Clients.Add(client);
        await appDbContext.SaveChangesAsync();

        return await Task.FromResult(client);
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

    public async Task<IEnumerable<Client>> BrowseAllAsync(string? name = null,
        string? surname = null,
        string? phoneNumber = null,
        string? email = null,
        string? idCard = null,
        string? city = null,
        string? street = null,
        DateTime? from = null,
        DateTime? to = null)
    {
        var result = appDbContext.Clients.AsQueryable();
        if (name != null)
        {
            result = result.Where(x => x.Name.Contains(name));
        }

        if (surname != null)
        {
            result = result.Where(x => x.Surname.Contains(surname));
        }

        if (phoneNumber != null)
        {
            result = result.Where(x => x.PhoneNumber == phoneNumber);
        }

        if (email != null)
        {
            result = result.Where(x => x.Email != null && x.Email.Contains(email));
        }

        if (idCard != null)
        {
            result = result.Where(x => x.IdCard != null && x.IdCard.Contains(idCard));
        }

        if (city != null)
        {
            result = result.Where(x => x.City.Contains(city));
        }

        if (street != null)
        {
            result = result.Where(x => x.Street.Contains(street));
        }

        if (from != null)
        {
            result = result.Where(x => x.DateAdded.Date > from.Value.Date);
        }

        if (to != null)
        {
            result = result.Where(x => x.DateAdded.Date < to.Value.Date);
        }

        return await Task.FromResult(result.AsEnumerable());
    }

    public async Task<Client> UpdateAsync(Client client, int id)
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
        await appDbContext.SaveChangesAsync();

        return await Task.FromResult(clientToUpdate);
    }
}