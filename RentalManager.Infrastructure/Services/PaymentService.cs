using RentalManager.Core.Repositories;
using RentalManager.Infrastructure.Commands.PaymentCommands;
using RentalManager.Infrastructure.DTO;
using RentalManager.Infrastructure.DTO.ObjectConversions;

namespace RentalManager.Infrastructure.Services;

public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository _paymentRepository;

    public PaymentService(IPaymentRepository paymentRepository)
    {
        _paymentRepository = paymentRepository;
    }

    public async Task<PaymentDto> AddAsync(CreatePayment createPayment, int rentalAgreementId)
    {
        var result = await _paymentRepository.AddAsync(createPayment.ToDomain(), rentalAgreementId);
        return result.ToDto();
    }

    public async Task<IEnumerable<PaymentDto>> BrowseAllAsync(int? rentalAgreementId = null, string? method = null,
        DateTime? from = null, DateTime? to = null)
    {
        var result = await _paymentRepository.BrowseAllAsync(rentalAgreementId, method, from, to);
        return await Task.FromResult(result.Select(x => x.ToDto()));
    }

    public async Task DeleteAsync(int id)
    {
        await _paymentRepository.DeleteAsync(id);
    }

    public async Task<PaymentDto> GetAsync(int id)
    {
        var result = await _paymentRepository.GetAsync(id);
        return await Task.FromResult(result.ToDto());
    }

    public async Task<PaymentDto> UpdateAsync(UpdatePayment updatePayment, int id)
    {
        var result = await _paymentRepository.UpdateAsync(updatePayment.ToDomain(), id);
        return await Task.FromResult(result.ToDto());
    }
}