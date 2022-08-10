using RentalManager.Core.Repositories;
using RentalManager.Infrastructure.Commands;
using RentalManager.Infrastructure.DTO;

namespace RentalManager.Infrastructure.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        public PaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<PaymentDTO> AddAsync(CreatePayment createPayment)
        {
            var result = await _paymentRepository.AddAsync(createPayment.ToDomain());
            return result.ToDTO();
        }

        public async Task<IEnumerable<PaymentDTO>> BrowseAllAsync(int? RentalAgreementId = null, string? method = null, DateTime? from = null, DateTime? to = null)
        {
            var result = await _paymentRepository.BrowseAllAsync(RentalAgreementId, method, from, to);
            return await Task.FromResult(result.Select(x => x.ToDTO()));
        }

        public async Task DeleteAsync(int id)
        {
            await _paymentRepository.DeleteAsync(id);
        }

        public async Task<PaymentDTO> GetAsync(int id)
        {
            var result = await _paymentRepository.GetAsync(id);
            return await Task.FromResult(result.ToDTO());
        }

        public async Task<PaymentDTO> UpdateAsync(UpdatePayment updatePayment, int id)
        {
            var result = await _paymentRepository.UpdateAsync(updatePayment.ToDomain(), id);
            return await Task.FromResult(result.ToDTO());
        }
    }
}
