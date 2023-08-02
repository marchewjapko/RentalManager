using Microsoft.AspNetCore.Mvc;
using RentalManager.Infrastructure.Commands.ClientCommands;
using RentalManager.Infrastructure.DTO;
using RentalManager.Infrastructure.Services;

namespace RentalManager.WebAPI.Controllers;

[ApiController]
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
        var result = await _clientService.AddAsync(createClient);
        return Json(result);
    }

    [ProducesResponseType(typeof(IEnumerable<ClientDto>), 200)]
    [HttpGet]
    public async Task<IActionResult> BrowseAllClients(string? name = null, string? surname = null,
        string? phoneNumber = null, string? email = null, string? idCard = null, string? city = null,
        string? street = null, DateTime? from = null, DateTime? to = null)
    {
        var result =
            await _clientService.BrowseAllAsync(name, surname, phoneNumber, email, idCard, city, street, from, to);
        return Json(result);
    }

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
        var result = await _clientService.UpdateAsync(updateClient, id);
        return Json(result);
    }
}