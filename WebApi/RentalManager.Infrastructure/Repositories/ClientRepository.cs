﻿using Microsoft.EntityFrameworkCore;
using RentalManager.Core.Domain;
using RentalManager.Core.Repositories;
using RentalManager.Global.Queries;
using RentalManager.Global.Queries.Sorting;
using RentalManager.Infrastructure.ExceptionHandling.Exceptions;
using RentalManager.Infrastructure.Extensions;
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
        var result = await appDbContext.Clients.FirstOrDefaultAsync(x => x.Id == id);

        if (result == null)
        {
            throw new ClientNotFoundException(id);
        }

        return result;
    }

    public async Task<IEnumerable<Client>> BrowseAllAsync(QueryClients queryClients)
    {
        var result = appDbContext.Clients.AsQueryable();

        result = FilterClients(result, queryClients);

        result = SortClients(result, queryClients);

        return await result.ToListAsync();
    }

    public async Task<Client> UpdateAsync(Client client, int id)
    {
        var clientToUpdate = await appDbContext.Clients.FirstOrDefaultAsync(x => x.Id == id);

        if (clientToUpdate == null)
        {
            throw new ClientNotFoundException(id);
        }

        clientToUpdate.FirstName = client.FirstName;
        clientToUpdate.LastName = client.LastName;
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
        clients = clients.Filter(x => x.FirstName, queryClients.FirstName, FilterOperand.Contains);
        clients = clients.Filter(x => x.LastName, queryClients.LastName, FilterOperand.Contains);
        clients = clients.Filter(x => x.Email, queryClients.Email, FilterOperand.Contains);
        clients = clients.Filter(x => x.City, queryClients.City, FilterOperand.Contains);
        clients = clients.Filter(x => x.Street, queryClients.Street, FilterOperand.Contains);
        clients = clients.Filter(x => x.CreatedTs.Date, queryClients.AddedFrom?.Date, FilterOperand.GreaterThanOrEqualTo);
        clients = clients.Filter(x => x.CreatedTs.Date, queryClients.AddedTo?.Date, FilterOperand.LessThanOrEqualTo);

        if (queryClients.OnlyActive)
        {
            clients = clients.Filter(x => x.IsActive, true, FilterOperand.Equals);
        }

        return clients;
    }

    private static IQueryable<Client> SortClients(IQueryable<Client> clients,
        QueryClients queryClients)
    {
        var sortOrder = queryClients.Descending ? SortOrder.Desc : SortOrder.Asc;

        clients = queryClients.SortClientsBy switch
        {
            SortClientsBy.Id => clients.Sort(x => x.Id, sortOrder),
            SortClientsBy.Name => clients.Sort(x => x.FirstName, sortOrder),
            SortClientsBy.Surname => clients.Sort(x => x.LastName, sortOrder),
            SortClientsBy.DateAdded => clients.Sort(x => x.CreatedTs, sortOrder),
            _ => clients.Sort(x => x.CreatedTs, sortOrder)
        };

        clients = clients.Skip(queryClients.Position);
        clients = clients.Take(queryClients.PageSize);

        return clients;
    }
}