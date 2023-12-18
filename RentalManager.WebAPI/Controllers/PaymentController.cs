using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentalManager.Global.Queries;
using RentalManager.Infrastructure.Commands.PaymentCommands;
using RentalManager.Infrastructure.DTO;
using RentalManager.Infrastructure.Services.Interfaces;

// ReSharper disable RouteTemplates.RouteParameterConstraintNotResolved

namespace RentalManager.WebAPI.Controllers;

[ApiController]
[Authorize]
[Route("[Controller]")]
public class PaymentController : Controller
{
    private readonly IPaymentService _paymentService;

    public PaymentController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [HttpPost]
    public async Task<IActionResult> AddPayment([FromBody] CreatePayment createPayment)
    {
        await _paymentService.AddAsync(createPayment, User);

        return Ok();
    }

    [ProducesResponseType(typeof(IEnumerable<PaymentDto>), 200)]
    [HttpGet]
    public async Task<IActionResult> BrowseAllPayments([FromQuery] QueryPayment queryPayment)
    {
        var result = await _paymentService.BrowseAllAsync(queryPayment);

        return Json(result);
    }

    [Authorize(Roles = "Administrator")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeletePayment(int id)
    {
        await _paymentService.DeleteAsync(id);

        return NoContent();
    }

    [ProducesResponseType(typeof(PaymentDto), 200)]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetPayment(int id)
    {
        var result = await _paymentService.GetAsync(id);

        return Json(result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdatePayment([FromBody] UpdatePayment updatePayment, int id)
    {
        await _paymentService.UpdateAsync(updatePayment, id);

        return Ok();
    }

    [Route("Deactivate/{id}")]
    [HttpPatch]
    public async Task<IActionResult> DeactivatePayment(int id)
    {
        await _paymentService.Deactivate(id);

        return NoContent();
    }
}