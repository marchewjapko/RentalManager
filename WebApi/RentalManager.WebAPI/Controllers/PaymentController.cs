using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentalManager.Global.Queries;
using RentalManager.Infrastructure.Models.Commands.PaymentCommands;
using RentalManager.Infrastructure.Models.DTO;
using RentalManager.Infrastructure.Services.PaymentService;

// ReSharper disable RouteTemplates.RouteParameterConstraintNotResolved

namespace RentalManager.WebAPI.Controllers;

[ApiController]
[Authorize]
[Route("[Controller]")]
public class PaymentController(IPaymentService paymentService) : Controller
{
    [ProducesResponseType(typeof(PaymentDto), 200)]
    [HttpPost]
    public async Task<IActionResult> AddPayment([FromBody] CreatePaymentCommand createPayment)
    {
        var result = await paymentService.AddAsync(createPayment, User);

        return Ok(result);
    }

    [ProducesResponseType(typeof(IEnumerable<PaymentDto>), 200)]
    [HttpGet]
    public async Task<IActionResult> BrowseAllPayments([FromQuery] QueryPayment queryPayment)
    {
        var result = await paymentService.BrowseAllAsync(queryPayment);

        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeletePayment(int id)
    {
        await paymentService.DeleteAsync(id);

        return NoContent();
    }

    [ProducesResponseType(typeof(PaymentDto), 200)]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetPayment(int id)
    {
        var result = await paymentService.GetAsync(id);

        return Ok(result);
    }

    [ProducesResponseType(typeof(PaymentDto), 200)]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdatePayment([FromBody] UpdatePaymentCommand updatePayment, int id)
    {
        var result = await paymentService.UpdateAsync(updatePayment, id);

        return Ok(result);
    }

    [Route("Deactivate/{id}")]
    [HttpPatch]
    public async Task<IActionResult> DeactivatePayment(int id)
    {
        await paymentService.Deactivate(id);

        return NoContent();
    }
}