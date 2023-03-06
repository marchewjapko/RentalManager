using Microsoft.AspNetCore.Mvc;
using RentalManager.Infrastructure.Commands;
using RentalManager.Infrastructure.Services;

namespace RentalManager.WebAPI.Controllers;

[ApiController]
[Route("[Controller]")]
public class RentalAgreementController : Controller
{
    private readonly IRentalAgreementService _rentalAgreementService;

    public RentalAgreementController(IRentalAgreementService rentalAgreementService)
    {
        _rentalAgreementService = rentalAgreementService;
    }

    [HttpPost]
    public async Task<IActionResult> AddRentalAgreement([FromBody] CreateRentalAgreement createRentalAgreement)
    {
        var result = await _rentalAgreementService.AddAsync(createRentalAgreement);
        return Json(result);
    }

    [HttpGet]
    public async Task<IActionResult> BrowseAllRentalAgreements(
        int? clientId = null,
        string? surname = null,
        string? phoneNumber = null,
        string? city = null,
        string? street = null,
        int? rentalEquipmentId = null,
        string? rentalEquipmentName = null,
        int? employeeId = null,
        bool onlyUnpaid = false,
        DateTime? from = null,
        DateTime? to = null)
    {
        var result = await _rentalAgreementService.BrowseAllAsync(
            clientId,
            surname,
            phoneNumber,
            city,
            street,
            rentalEquipmentId,
            rentalEquipmentName,
            employeeId,
            onlyUnpaid,
            from,
            to);
        return Json(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteRentalAgreement(int id)
    {
        await _rentalAgreementService.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetRentalAgreement(int id)
    {
        var rentalAgreementDto = await _rentalAgreementService.GetAsync(id);
        return Json(rentalAgreementDto);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateRentalAgreement([FromBody] UpdateRentalAgreement updateRentalAgreement,
        int id)
    {
        var result = await _rentalAgreementService.UpdateAsync(updateRentalAgreement, id);
        return Json(result);
    }
}