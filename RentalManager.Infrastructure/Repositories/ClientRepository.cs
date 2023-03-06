using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using RentalManager.Core.Domain;
using RentalManager.Core.Repositories;

namespace RentalManager.Infrastructure.Repositories;

public class ClientRepository : IClientRepository
{
    private readonly AppDbContext _appDbContext;

    public ClientRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<Client> AddAsync(Client client)
    {
        if (!CheckClient(client))
        {
            throw new Exception("Invalid client");
        }

        try
        {
            client.PhoneNumber = Regex.Replace(client.PhoneNumber, @"\s+", "");
            _appDbContext.Clients.Add(client);
            await _appDbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("Unable to add client\n" + ex.Message);
        }

        return await Task.FromResult(client);
    }

    public async Task DeleteAsync(int id)
    {
        var result = await _appDbContext.Clients.FirstOrDefaultAsync(client => client.Id == id);
        if (result == null)
        {
            throw new Exception("Unable to find client");
        }

        _appDbContext.Clients.Remove(result);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task<Client> GetAsync(int id)
    {
        var result = await Task.FromResult(_appDbContext.Clients.FirstOrDefault(x => x.Id == id));
        if (result == null)
        {
            throw new Exception("Unable to find client");
        }

        return result;
    }

    public async Task<IEnumerable<Client>> BrowseAllAsync(string? name = null, string? surname = null,
        string? phoneNumber = null, string? email = null, string? idCard = null, string? city = null,
        string? street = null, DateTime? from = null, DateTime? to = null)
    {
        var result = _appDbContext.Clients.AsQueryable();
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
            result = result.Where(x => x.IdCard.Contains(idCard));
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
        var z = _appDbContext.Clients.FirstOrDefault(x => x.Id == id);
        if (z == null)
        {
            throw new Exception("Unable to update client");
        }

        z.Name = client.Name;
        z.Surname = client.Surname;
        z.PhoneNumber = client.PhoneNumber;
        z.Email = client.Email;
        z.IdCard = client.IdCard;
        z.City = client.City;
        z.Street = client.Street;
        await _appDbContext.SaveChangesAsync();
        return await Task.FromResult(z);
    }

    private static bool CheckClient(Client client)
    {
        if (!Regex.IsMatch(client.PhoneNumber, @"^([0-9]{3})?[-. ]?([0-9]{3})[-. ]?([0-9]{3})$"))
        {
            return false;
        }

        if (!string.IsNullOrEmpty(client.Email) &&
            !Regex.IsMatch(client.Email, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"))
        {
            return false;
        }

        return string.IsNullOrEmpty(client.IdCard) ||
               Regex.IsMatch(client.IdCard, @"\b([a-zA-Z]){3}[ ]?[0-9]{6}\b");
    }
}