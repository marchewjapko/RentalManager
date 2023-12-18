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
public class ClientController(IClientService clientService) : Controller
{
    [HttpPost]
    public async Task<IActionResult> AddClient([FromBody] CreateClient createClient)
    {
        await clientService.AddAsync(createClient, User);

        return Ok();
    }

    [ProducesResponseType(typeof(IEnumerable<ClientDto>), 200)]
    [HttpGet]
    public async Task<IActionResult> BrowseAllClients([FromQuery] QueryClients queryClients)
    {
        var result =
            await clientService.BrowseAllAsync(queryClients);

        return Json(result);
    }

    [Authorize(Roles = "Administrator")]
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

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateClient([FromBody] UpdateClient updateClient, int id)
    {
        await clientService.UpdateAsync(updateClient, id);

        return Ok();
    }

    [Route("Deactivate/{id}")]
    [HttpPatch]
    public async Task<IActionResult> DeactivateClient(int id)
    {
        await clientService.Deactivate(id);

        return NoContent();
    }
}