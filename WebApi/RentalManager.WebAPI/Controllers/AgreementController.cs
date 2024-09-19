using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentalManager.Global.Queries;
using RentalManager.Infrastructure.Models.Commands.AgreementCommands;
using RentalManager.Infrastructure.Models.DTO;
using RentalManager.Infrastructure.Services.AgreementService;

namespace RentalManager.WebAPI.Controllers;

[ApiController]
//[Authorize]
[Route("[Controller]")]
public class AgreementController(IAgreementService agreementService)
    : Controller
{
    [ProducesResponseType(typeof(AgreementDto), 200)]
    [HttpPost]
    public async Task<IActionResult> AddAgreement([FromBody] CreateAgreementCommand createAgreement)
    {
        var result = await agreementService.AddAsync(createAgreement, User);

        return Json(result);
    }

    [ProducesResponseType(typeof(IEnumerable<AgreementDto>), 200)]
    [HttpGet]
    public async Task<IActionResult> BrowseAllAgreements(
        [FromQuery] QueryAgreements queryAgreements)
    {
        var result = await agreementService.BrowseAllAsync(queryAgreements);

        return Json(result);
    }

    [Authorize(Roles = "Administrator")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAgreement(int id)
    {
        await agreementService.DeleteAsync(id);

        return Ok();
    }

    [ProducesResponseType(typeof(AgreementDto), 200)]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetAgreement(int id)
    {
        var result = await agreementService.GetAsync(id);

        return Json(result);
    }

    [ProducesResponseType(typeof(AgreementDto), 200)]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateAgreement(
        [FromBody] UpdateAgreementCommand updateAgreement,
        int id)
    {
        var result = await agreementService.UpdateAsync(updateAgreement, id, User);

        return Json(result);
    }

    [Route("Deactivate/{id}")]
    [HttpPatch]
    public async Task<IActionResult> DeactivateAgreement(int id)
    {
        await agreementService.Deactivate(id);

        return Ok();
    }
}