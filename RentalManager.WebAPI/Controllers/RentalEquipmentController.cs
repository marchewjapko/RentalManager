using Microsoft.AspNetCore.Mvc;
using RentalManager.Infrastructure.Commands.RentalEquipmentCommands;
using RentalManager.Infrastructure.DTO;
using RentalManager.Infrastructure.Services;

namespace RentalManager.WebAPI.Controllers;

[ApiController]
[Route("[Controller]")]
public class RentalEquipmentController : Controller
{
    private readonly IRentalEquipmentService _rentalEquipmentService;

    public RentalEquipmentController(IRentalEquipmentService rentalEquipmentService)
    {
        _rentalEquipmentService = rentalEquipmentService;
    }

    [ProducesResponseType(typeof(RentalEquipmentDto), 200)]
    [HttpPost]
    public async Task<IActionResult> AddRentalEquipment([FromForm] CreateRentalEquipment createRentalEquipment)
    {
        var result = await _rentalEquipmentService.AddAsync(createRentalEquipment);

        return Json(result);
    }

    [ProducesResponseType(typeof(IEnumerable<RentalEquipmentDto>), 200)]
    [HttpGet]
    public async Task<IActionResult> BrowseAllRentalEquipment(string? name = null,
        DateTime? from = null,
        DateTime? to = null)
    {
        var result = await _rentalEquipmentService.BrowseAllAsync(name, from, to);

        return Json(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteRentalEquipment(int id)
    {
        await _rentalEquipmentService.DeleteAsync(id);

        return NoContent();
    }

    [ProducesResponseType(typeof(RentalEquipmentDto), 200)]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetRentalEquipment(int id)
    {
        var clientDto = await _rentalEquipmentService.GetAsync(id);

        return Json(clientDto);
    }

    [ProducesResponseType(typeof(RentalEquipmentDto), 200)]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateRentalEquipment([FromForm] UpdateRentalEquipment updateRentalEquipment,
        int id)
    {
        var result = await _rentalEquipmentService.UpdateAsync(updateRentalEquipment, id);

        return Json(result);
    }

    [ProducesResponseType(typeof(File), 200)]
    [Route("/RentalEquipment/Image/{id}")]
    [HttpGet]
    public async Task<IActionResult?> GetRentalEquipmentImage(int id)
    {
        var rentalEquipmentDto = await _rentalEquipmentService.GetAsync(id);

        if (rentalEquipmentDto.Image is null)
        {
            return File("DefaultEquipmentImage.png", "image/png");
        }

        return File(rentalEquipmentDto.Image, "image/jpeg");
    }
}