﻿using System.Security.Claims;
using RentalManager.Global.Queries;
using RentalManager.Infrastructure.Models.Commands.ClientCommands;
using RentalManager.Infrastructure.Models.DTO;

namespace RentalManager.Infrastructure.Services.ClientService;

public interface IClientService
{
    Task<ClientDto> AddAsync(CreateClientCommand createClient, ClaimsPrincipal user);

    Task<ClientDto> GetAsync(int id);

    Task DeleteAsync(int id);

    Task<ClientDto> UpdateAsync(UpdateClientCommand updateClient, int id);

    Task<IEnumerable<ClientDto>> BrowseAllAsync(QueryClients queryClients);

    Task Deactivate(int id);
}