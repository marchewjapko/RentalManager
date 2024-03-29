﻿using Microsoft.EntityFrameworkCore;
using RentalManager.Core.Domain;
using RentalManager.Core.Repositories;
using RentalManager.Global.Queries;
using RentalManager.Global.Queries.Sorting;
using RentalManager.Infrastructure.Exceptions;
using RentalManager.Infrastructure.Repositories.DbContext;

namespace RentalManager.Infrastructure.Repositories;

public class ClientRepository(AppDbContext appDbContext) : IClientRepository
{
    public async Task<Client> AddAsync(Client client)
    {
        var result = appDbContext.Clients.Add(client);
        await appDbContext.SaveChangesAsync();

        return result.Entity;
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

        result = FilterClients(result, queryClients);

        result = SortClients(result, queryClients);

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
        clientToUpdate.UpdatedTs = DateTime.Now;
        await appDbContext.SaveChangesAsync();

        return clientToUpdate;
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

    private static IQueryable<Client> FilterClients(IQueryable<Client> clients,
        QueryClients queryClients)
    {
        if (queryClients.Name != null)
        {
            clients = clients.Where(x => x.Name.Contains(queryClients.Name));
        }

        if (queryClients.Surname != null)
        {
            clients = clients.Where(x => x.Surname.Contains(queryClients.Surname));
        }

        if (queryClients.Email != null)
        {
            clients = clients.Where(x => x.Email != null && x.Email.Contains(queryClients.Email));
        }

        if (queryClients.City != null)
        {
            clients = clients.Where(x => x.City.Contains(queryClients.City));
        }

        if (queryClients.Street != null)
        {
            clients = clients.Where(x => x.Street.Contains(queryClients.Street));
        }

        if (queryClients.AddedFrom != null)
        {
            clients = clients.Where(x => x.CreatedTs.Date > queryClients.AddedFrom.Value.Date);
        }

        if (queryClients.AddedTo != null)
        {
            clients = clients.Where(x => x.CreatedTs.Date < queryClients.AddedTo.Value.Date);
        }

        if (queryClients.OnlyActive)
        {
            clients = clients.Where(x => x.IsActive);
        }

        return clients;
    }

    private static IQueryable<Client> SortClients(IQueryable<Client> clients,
        QueryClients queryClients)
    {
        if (queryClients.Descending)
        {
            clients = queryClients.SortClientsBy switch
            {
                SortClientsBy.Id => clients.OrderByDescending(x => x.Id),
                SortClientsBy.Name => clients.OrderByDescending(x => x.Name),
                SortClientsBy.Surname => clients.OrderByDescending(x => x.Surname),
                SortClientsBy.DateAdded => clients.OrderByDescending(x => x.CreatedTs),
                _ => clients.OrderByDescending(x => x.CreatedTs)
            };
        }
        else
        {
            clients = queryClients.SortClientsBy switch
            {
                SortClientsBy.Id => clients.OrderBy(x => x.Id),
                SortClientsBy.Name => clients.OrderBy(x => x.Name),
                SortClientsBy.Surname => clients.OrderBy(x => x.Surname),
                SortClientsBy.DateAdded => clients.OrderBy(x => x.CreatedTs),
                _ => clients.OrderBy(x => x.CreatedTs)
            };
        }

        clients = clients.Skip(queryClients.Position);
        clients = clients.Take(queryClients.PageSize);

        return clients;
    }
}