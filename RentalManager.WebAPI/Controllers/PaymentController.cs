﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

    [ProducesResponseType(typeof(PaymentDto), 200)]
    [HttpPost]
    public async Task<IActionResult> AddPayment([FromBody] CreatePayment createPayment,
        int agreementId)
    {
        var result = await _paymentService.AddAsync(createPayment, agreementId, User);

        return Json(result);
    }

    [ProducesResponseType(typeof(IEnumerable<PaymentDto>), 200)]
    [HttpGet]
    public async Task<IActionResult> BrowseAllPayments(int? agreementId = null,
        string? method = null,
        DateTime? from = null,
        DateTime? to = null)
    {
        var result = await _paymentService.BrowseAllAsync(agreementId, method, from, to);

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

    [ProducesResponseType(typeof(PaymentDto), 200)]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdatePayment([FromBody] UpdatePayment updatePayment, int id)
    {
        var result = await _paymentService.UpdateAsync(updatePayment, id);

        return Json(result);
    }

    [Route("/Payment/Deactivate/{id}")]
    [HttpGet]
    public async Task<IActionResult> DeactivateEquipment(int id)
    {
        await _paymentService.Deactivate(id);

        return NoContent();
    }
}