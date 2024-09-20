using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentalManager.Global.Queries;
using RentalManager.Infrastructure.Models.Commands.EquipmentCommands;
using RentalManager.Infrastructure.Models.DTO;
using RentalManager.Infrastructure.Services.EquipmentService;

namespace RentalManager.WebAPI.Controllers;

[ApiController]
[Authorize]
[Route("[Controller]")]
public class EquipmentController(IEquipmentService equipmentService) : Controller
{
    [ProducesResponseType(typeof(EquipmentDto), 200)]
    [HttpPost]
    public async Task<IActionResult> AddEquipment([FromForm] CreateEquipmentCommand createEquipment)
    {
        var result = await equipmentService.AddAsync(createEquipment, User);

        return Ok(result);
    }

    [ProducesResponseType(typeof(IEnumerable<EquipmentDto>), 200)]
    [HttpGet]
    public async Task<IActionResult> BrowseAllEquipment([FromQuery] QueryEquipment queryEquipment)
    {
        var result = await equipmentService.BrowseAllAsync(queryEquipment);

        return Ok(result);
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

        return Ok(clientDto);
    }

    [ProducesResponseType(typeof(EquipmentDto), 200)]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateEquipment(
        [FromForm] UpdateEquipmentCommand updateEquipment,
        int id)
    {
        var result = await equipmentService.UpdateAsync(updateEquipment, id);

        return Ok(result);
    }

    [Route("Deactivate/{id}")]
    [HttpPatch]
    public async Task<IActionResult> DeactivateEquipment(int id)
    {
        await equipmentService.Deactivate(id);

        return NoContent();
    }
}