using Microsoft.AspNetCore.Mvc;
using RentalManager.Infrastructure.Commands;
using RentalManager.Infrastructure.DTO;
using RentalManager.Infrastructure.Services;

namespace RentalManager.WebAPI.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class RentalEquipmentController : Controller
    {
        private readonly IRentalEquipmentService _rentalEquipmentService;
        public RentalEquipmentController(IRentalEquipmentService rentalEquipmentService)
        {
            _rentalEquipmentService = rentalEquipmentService;
        }
        [HttpPost]
        public async Task<IActionResult> AddRentalEquipment([FromBody] CreateRentalEquipment createRentalEquipment)
        {
            var result = await _rentalEquipmentService.AddAsync(createRentalEquipment);
            return Json(result);
        }
        [HttpGet]
        public async Task<IActionResult> BrowseAllRentalEquipment(string? name = null, DateTime? from = null, DateTime? to = null)
        {
            var result = await _rentalEquipmentService.BrowseAllAsync(name, from, to);
            return Json(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRentalEquipment(int id)
        {
            await _rentalEquipmentService.DeleteAsync(id);
            return NoContent();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRentalEquipment(int id)
        {
            RentalEquipmentDTO clientDTO = await _rentalEquipmentService.GetAsync(id);
            return Json(clientDTO);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRentalEquipment([FromBody] UpdateRentalEquipment updateRentalEquipment, int id)
        {
            var result = await _rentalEquipmentService.UpdateAsync(updateRentalEquipment, id);
            return Json(result);
        }
    }
}
