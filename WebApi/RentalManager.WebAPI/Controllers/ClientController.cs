using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentalManager.Global.Queries;
using RentalManager.Infrastructure.Models.Commands.ClientCommands;
using RentalManager.Infrastructure.Models.DTO;
using RentalManager.Infrastructure.Services.ClientService;

// ReSharper disable RouteTemplates.RouteParameterConstraintNotResolved

namespace RentalManager.WebAPI.Controllers;

[ApiController]
[Authorize]
[Route("[Controller]")]
public class ClientController(IClientService clientService) : Controller
{
    [ProducesResponseType(typeof(ClientDto), 200)]
    [HttpPost]
    public async Task<IActionResult> AddClient([FromBody] CreateClientCommand createClient)
    {
        var result = await clientService.AddAsync(createClient, User);

        return Json(result);
    }

    [ProducesResponseType(typeof(IEnumerable<ClientDto>), 200)]
    [HttpGet]
    public async Task<IActionResult> BrowseAllClients([FromQuery] QueryClients queryClients)
    {
        var result =
            await clientService.BrowseAllAsync(queryClients);

        return Json(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteClient(int id)
    {
        await clientService.DeleteAsync(id);

        return NoContent();
    }

    [ProducesResponseType(typeof(ClientDto), 200)]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetClient(int id)
    {
        var clientDto = await clientService.GetAsync(id);

        return Json(clientDto);
    }

    [ProducesResponseType(typeof(ClientDto), 200)]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateClient([FromBody] UpdateClientCommand updateClient, int id)
    {
        var result = await clientService.UpdateAsync(updateClient, id);

        return Json(result);
    }

    [Route("Deactivate/{id}")]
    [HttpPatch]
    public async Task<IActionResult> DeactivateClient(int id)
    {
        await clientService.Deactivate(id);

        return NoContent();
    }
}