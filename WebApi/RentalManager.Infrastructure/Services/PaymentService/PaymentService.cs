﻿using System.Security.Claims;
using AutoMapper;
using RentalManager.Core.Domain;
using RentalManager.Core.Repositories;
using RentalManager.Global.Queries;
using RentalManager.Infrastructure.Extensions;
using RentalManager.Infrastructure.Models.Commands.PaymentCommands;
using RentalManager.Infrastructure.Models.DTO;

namespace RentalManager.Infrastructure.Services.PaymentService;

public class PaymentService(IPaymentRepository paymentRepository, IMapper mapper)
    : IPaymentService
{
    public async Task<PaymentDto> AddAsync(CreatePayment createPayment, ClaimsPrincipal user)
    {
        var newPayment = mapper.Map<Payment>(createPayment);
        newPayment.CreatedBy = user.GetId();

        var result = await paymentRepository.AddAsync(newPayment);

        return mapper.Map<PaymentDto>(result);
    }

    public async Task<IEnumerable<PaymentDto>> BrowseAllAsync(QueryPayment queryPayment)
    {
        var result = await paymentRepository.BrowseAllAsync(queryPayment);

        return mapper.Map<IEnumerable<PaymentDto>>(result);
    }

    public async Task DeleteAsync(int id)
    {
        await paymentRepository.DeleteAsync(id);
    }

    public async Task<PaymentDto> GetAsync(int id)
    {
        var result = await paymentRepository.GetAsync(id);

        return mapper.Map<PaymentDto>(result);
    }

    public async Task<PaymentDto> UpdateAsync(UpdatePayment updatePayment, int id)
    {
        var domainPayment = mapper.Map<Payment>(updatePayment);

        var result = await paymentRepository.UpdateAsync(domainPayment, id);

        return mapper.Map<PaymentDto>(result);
    }

    public async Task Deactivate(int id)
    {
        await paymentRepository.Deactivate(id);
    }
}