﻿using RentalManager.Infrastructure.Commands.PaymentCommands;
using RentalManager.Infrastructure.DTO;

namespace RentalManager.Infrastructure.Services;

public interface IPaymentService
{
    Task<PaymentDto> AddAsync(CreatePayment createPayment, int rentalAgreementId);
    Task<PaymentDto> GetAsync(int id);

    Task<IEnumerable<PaymentDto>> BrowseAllAsync(int? rentalAgreementId = null,
        string? method = null,
        DateTime? from = null,
        DateTime? to = null);

    Task DeleteAsync(int id);
    Task<PaymentDto> UpdateAsync(UpdatePayment updatePayment, int id);
}