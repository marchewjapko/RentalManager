using Microsoft.AspNetCore.Mvc;
using RentalManager.Infrastructure.Commands;
using RentalManager.Infrastructure.DTO;
using RentalManager.Infrastructure.Services;

namespace RentalManager.WebAPI.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class ClientController : Controller
    {
        private readonly IClientService _clientService;
        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }
        [HttpPost]
        public async Task<IActionResult> AddClient([FromBody] CreateClient createClient)
        {
            var result = await _clientService.AddAsync(createClient);
            return Json(result);
        }
        [HttpGet]
        public async Task<IActionResult> BrowseAllClients(string? name = null, string? surname = null, string? phoneNumber = null, string? email = null, string? idCard = null, string? city = null, string? street = null, DateTime? from = null, DateTime? to = null)
        {
            var result = await _clientService.BrowseAllAsync(name, surname, phoneNumber, email, idCard, city, street, from, to);
            return Json(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            await _clientService.DeleteAsync(id);
            return NoContent();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetClient(int id)
        {
            ClientDTO clientDTO = await _clientService.GetAsync(id);
            return Json(clientDTO);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClient([FromBody] UpdateClient updateClient, int id)
        {
            var result = await _clientService.UpdateAsync(updateClient, id);
            return Json(result);
        }
    }
}
