﻿using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using RentalManager.Core.Domain;
using RentalManager.Core.Repositories;
using RentalManager.Infrastructure.Commands.PaymentCommands;
using RentalManager.Infrastructure.DTO;
using RentalManager.Infrastructure.DTO.ObjectConversions;
using RentalManager.Infrastructure.Services.Interfaces;

namespace RentalManager.Infrastructure.Services.Implementation;

public class PaymentService
    (IPaymentRepository paymentRepository, UserManager<User> userManager) : IPaymentService
{
    public async Task<PaymentDto> AddAsync(CreatePayment createPayment,
        int rentalAgreementId,
        ClaimsPrincipal user)
    {
        var newPayment = createPayment.ToDomain();
        newPayment.User = (await userManager.GetUserAsync(user))!;

        var result = await paymentRepository.AddAsync(newPayment, rentalAgreementId);

        return result.ToDto();
    }

    public async Task<IEnumerable<PaymentDto>> BrowseAllAsync(int? rentalAgreementId = null,
        string? method = null,
        DateTime? from = null,
        DateTime? to = null)
    {
        var result = await paymentRepository.BrowseAllAsync(rentalAgreementId, method, from, to);

        return await Task.FromResult(result.Select(x => x.ToDto()));
    }

    public async Task DeleteAsync(int id)
    {
        await paymentRepository.DeleteAsync(id);
    }

    public async Task<PaymentDto> GetAsync(int id)
    {
        var result = await paymentRepository.GetAsync(id);

        return await Task.FromResult(result.ToDto());
    }

    public async Task<PaymentDto> UpdateAsync(UpdatePayment updatePayment, int id)
    {
        var result = await paymentRepository.UpdateAsync(updatePayment.ToDomain(), id);

        return await Task.FromResult(result.ToDto());
    }

    public async Task Deactivate(int id)
    {
        await paymentRepository.Deactivate(id);
    }
}