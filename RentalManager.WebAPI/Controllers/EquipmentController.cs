using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentalManager.Infrastructure.Commands.EquipmentCommands;
using RentalManager.Infrastructure.DTO;
using RentalManager.Infrastructure.Services.Interfaces;

// ReSharper disable RouteTemplates.RouteParameterConstraintNotResolved

namespace RentalManager.WebAPI.Controllers;

[ApiController]
[Authorize]
[Route("[Controller]")]
public class EquipmentController : Controller
{
    private readonly IEquipmentService _equipmentService;

    public EquipmentController(IEquipmentService equipmentService)
    {
        _equipmentService = equipmentService;
    }

    [ProducesResponseType(typeof(EquipmentDto), 200)]
    [HttpPost]
    public async Task<IActionResult> AddEquipment([FromForm] CreateEquipment createEquipment)
    {
        var result = await _equipmentService.AddAsync(createEquipment, User);

        return Json(result);
    }

    [ProducesResponseType(typeof(IEnumerable<EquipmentDto>), 200)]
    [HttpGet]
    public async Task<IActionResult> BrowseAllEquipment(string? name = null,
        DateTime? from = null,
        DateTime? to = null)
    {
        var result = await _equipmentService.BrowseAllAsync(name, from, to);

        return Json(result);
    }

    [Authorize(Roles = "Administrator")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteEquipment(int id)
    {
        await _equipmentService.DeleteAsync(id);

        return NoContent();
    }

    [ProducesResponseType(typeof(EquipmentDto), 200)]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetEquipment(int id)
    {
        var clientDto = await _equipmentService.GetAsync(id);

        return Json(clientDto);
    }

    [ProducesResponseType(typeof(EquipmentDto), 200)]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateEquipment(
        [FromForm] UpdateEquipment updateEquipment,
        int id)
    {
        var result = await _equipmentService.UpdateAsync(updateEquipment, id);

        return Json(result);
    }

    [ProducesResponseType(typeof(File), 200)]
    [Route("/Equipment/Image/{id}")]
    [HttpGet]
    public async Task<IActionResult?> GetEquipmentImage(int id)
    {
        var equipment = await _equipmentService.GetAsync(id);

        if (equipment.Image is null)
        {
            return File("DefaultEquipmentImage.png", "image/png");
        }

        return File(equipment.Image, "image/jpeg");
    }

    [Route("/Equipment/Deactivate/{id}")]
    [HttpGet]
    public async Task<IActionResult> DeactivateEquipment(int id)
    {
        await _equipmentService.Deactivate(id);

        return NoContent();
    }
}