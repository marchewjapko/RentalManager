using Microsoft.AspNetCore.Mvc;
using RentalManager.Infrastructure.Commands;
using RentalManager.Infrastructure.Services;

namespace RentalManager.WebAPI.Controllers;

[ApiController]
[Route("[Controller]")]
public class PaymentController : Controller
{
    private readonly IPaymentService _paymentService;

    public PaymentController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [HttpPost]
    public async Task<IActionResult> AddPayment([FromBody] CreatePayment createPayment, int rentalAgreementId)
    {
        var result = await _paymentService.AddAsync(createPayment, rentalAgreementId);
        return Json(result);
    }

    [HttpGet]
    public async Task<IActionResult> BrowseAllPayments(int? rentalAgreementId = null, string? method = null,
        DateTime? from = null, DateTime? to = null)
    {
        var result = await _paymentService.BrowseAllAsync(rentalAgreementId, method, from, to);
        return Json(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeletePayment(int id)
    {
        await _paymentService.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetPayment(int id)
    {
        var result = await _paymentService.GetAsync(id);
        return Json(result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdatePayment([FromBody] UpdatePayment updatePayment, int id)
    {
        var result = await _paymentService.UpdateAsync(updatePayment, id);
        return Json(result);
    }
}