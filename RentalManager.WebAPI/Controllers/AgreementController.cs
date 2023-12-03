using Microsoft.AspNetCore.Mvc;
using RentalManager.Infrastructure.Commands.AgreementCommands;
using RentalManager.Infrastructure.DTO;
using RentalManager.Infrastructure.Services;

namespace RentalManager.WebAPI.Controllers;

[ApiController]
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
        var result = await _agreementService.AddAsync(createAgreement);

        return Json(result);
    }

    [ProducesResponseType(typeof(IEnumerable<AgreementDto>), 200)]
    [HttpGet]
    public async Task<IActionResult> BrowseAllAgreements(
        int? clientId = null,
        string? surname = null,
        string? phoneNumber = null,
        string? city = null,
        string? street = null,
        int? equipmentId = null,
        string? equipmentName = null,
        int? employeeId = null,
        bool onlyUnpaid = false,
        DateTime? from = null,
        DateTime? to = null)
    {
        var result = await _agreementService.BrowseAllAsync(
            clientId,
            surname,
            phoneNumber,
            city,
            street,
            equipmentId,
            equipmentName,
            employeeId,
            onlyUnpaid,
            from,
            to);

        return Json(result);
    }

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
        var result = await _agreementService.UpdateAsync(updateAgreement, id);

        return Json(result);
    }
}