using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentalManager.Global.Queries;
using RentalManager.Infrastructure.Commands.AgreementCommands;
using RentalManager.Infrastructure.DTO;
using RentalManager.Infrastructure.Services.Interfaces;

// ReSharper disable RouteTemplates.RouteParameterConstraintNotResolved

namespace RentalManager.WebAPI.Controllers;

[ApiController]
[Authorize]
[Route("[Controller]")]
public class AgreementController : Controller
{
    private readonly IAgreementService _agreementService;

    public AgreementController(IAgreementService agreementService)
    {
        _agreementService = agreementService;
    }

    [ProducesResponseType(typeof(AgreementDto), 200)]
    [HttpPost]
    public async Task<IActionResult> AddAgreement([FromBody] CreateAgreement createAgreement)
    {
        await _agreementService.AddAsync(createAgreement, User);

        return Ok();
    }

    [ProducesResponseType(typeof(IEnumerable<AgreementDto>), 200)]
    [HttpGet]
    public async Task<IActionResult> BrowseAllAgreements(
        [FromQuery] QueryAgreements queryAgreements)
    {
        var result = await _agreementService.BrowseAllAsync(queryAgreements);

        return Json(result);
    }

    [Authorize(Roles = "Administrator")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAgreement(int id)
    {
        await _agreementService.DeleteAsync(id);

        return NoContent();
    }

    [ProducesResponseType(typeof(AgreementDto), 200)]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetAgreement(int id)
    {
        var result = await _agreementService.GetAsync(id);

        return Json(result);
    }

    [ProducesResponseType(typeof(AgreementDto), 200)]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateAgreement(
        [FromBody] UpdateAgreement updateAgreement,
        int id)
    {
        await _agreementService.UpdateAsync(updateAgreement, id);

        return Ok();
    }

    [Route("/Agreement/Deactivate/{id}")]
    [HttpGet]
    public async Task<IActionResult> DeactivateEquipment(int id)
    {
        await _agreementService.Deactivate(id);

        return NoContent();
    }
}