using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentalManager.Global.Queries;
using RentalManager.Infrastructure.Commands.EquipmentCommands;
using RentalManager.Infrastructure.DTO;
using RentalManager.Infrastructure.Services.Interfaces;

// ReSharper disable RouteTemplates.RouteParameterConstraintNotResolved

namespace RentalManager.WebAPI.Controllers;

[ApiController]
[Authorize]
[Route("[Controller]")]
public class EquipmentController(IEquipmentService equipmentService) : Controller
{
    [ProducesResponseType(typeof(EquipmentDto), 200)]
    [HttpPost]
    public async Task<IActionResult> AddEquipment([FromForm] CreateEquipment createEquipment)
    {
        var result = await equipmentService.AddAsync(createEquipment, User);

        return Json(result);
    }

    [ProducesResponseType(typeof(IEnumerable<EquipmentDto>), 200)]
    [HttpGet]
    public async Task<IActionResult> BrowseAllEquipment([FromQuery] QueryEquipment queryEquipment)
    {
        var result = await equipmentService.BrowseAllAsync(queryEquipment);

        return Json(result);
    }

    [Authorize(Roles = "Administrator")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteEquipment(int id)
    {
        await equipmentService.DeleteAsync(id);

        return NoContent();
    }

    [ProducesResponseType(typeof(EquipmentDto), 200)]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetEquipment(int id)
    {
        var clientDto = await equipmentService.GetAsync(id);

        return Json(clientDto);
    }

    [ProducesResponseType(typeof(EquipmentDto), 200)]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateEquipment(
        [FromForm] UpdateEquipment updateEquipment,
        int id)
    {
        var result = await equipmentService.UpdateAsync(updateEquipment, id);

        return Json(result);
    }

    [Route("Image/{id}")]
    [HttpGet]
    public async Task<IActionResult?> GetEquipmentImage(int id)
    {
        var equipment = await equipmentService.GetAsync(id);

        if (equipment.Image is null)
        {
            return File("DefaultEquipmentImage.png", "image/png");
        }

        return File(equipment.Image, "image/jpeg");
    }

    [Route("Deactivate/{id}")]
    [HttpPatch]
    public async Task<IActionResult> DeactivateEquipment(int id)
    {
        await equipmentService.Deactivate(id);

        return NoContent();
    }
}