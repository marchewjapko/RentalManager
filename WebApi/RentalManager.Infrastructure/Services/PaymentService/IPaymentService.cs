﻿using System.Security.Claims;
using RentalManager.Global.Queries;
using RentalManager.Infrastructure.Models.Commands.PaymentCommands;
using RentalManager.Infrastructure.Models.DTO;

namespace RentalManager.Infrastructure.Services.PaymentService;

public interface IPaymentService
{
    Task<PaymentDto> AddAsync(CreatePaymentCommand createPayment,
        ClaimsPrincipal user);

    Task<PaymentDto> GetAsync(int id);

    Task<IEnumerable<PaymentDto>> BrowseAllAsync(QueryPayment queryPayment);

    Task DeleteAsync(int id);
    Task<PaymentDto> UpdateAsync(UpdatePaymentCommand updatePayment, int id);

    Task Deactivate(int id);
}