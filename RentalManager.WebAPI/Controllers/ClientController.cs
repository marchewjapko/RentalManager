using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentalManager.Global.Queries;
using RentalManager.Infrastructure.Commands.ClientCommands;
using RentalManager.Infrastructure.DTO;
using RentalManager.Infrastructure.Services.Interfaces;

// ReSharper disable RouteTemplates.RouteParameterConstraintNotResolved

namespace RentalManager.WebAPI.Controllers;

[ApiController]
[Authorize]
[Route("[Controller]")]
public class ClientController : Controller
{
    private readonly IClientService _clientService;

    public ClientController(IClientService clientService)
    {
        _clientService = clientService;
    }

    [ProducesResponseType(typeof(ClientDto), 200)]
    [HttpPost]
    public async Task<IActionResult> AddClient([FromBody] CreateClient createClient)
    {
        await _clientService.AddAsync(createClient, User);

        return Ok();
    }

    [ProducesResponseType(typeof(IEnumerable<ClientDto>), 200)]
    [HttpGet]
    public async Task<IActionResult> BrowseAllClients([FromQuery] QueryClients queryClients)
    {
        var result =
            await _clientService.BrowseAllAsync(queryClients);

        return Json(result);
    }

    [Authorize(Roles = "Administrator")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteClient(int id)
    {
        await _clientService.DeleteAsync(id);

        return NoContent();
    }

    [ProducesResponseType(typeof(ClientDto), 200)]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetClient(int id)
    {
        var clientDto = await _clientService.GetAsync(id);

        return Json(clientDto);
    }

    [ProducesResponseType(typeof(ClientDto), 200)]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateClient([FromBody] UpdateClient updateClient, int id)
    {
        await _clientService.UpdateAsync(updateClient, id);

        return Ok();
    }

    [Route("/Client/Deactivate/{id}")]
    [HttpGet]
    public async Task<IActionResult> DeactivateEquipment(int id)
    {
        await _clientService.Deactivate(id);

        return NoContent();
    }
}