using RentalManager.Infrastructure.Commands;
using RentalManager.Infrastructure.DTO;

namespace RentalManager.Infrastructure.Services
{
    public interface IPaymentService
    {
        Task<PaymentDTO> AddAsync(CreatePayment createPayment, int rentalAgreementId);
        Task<PaymentDTO> GetAsync(int id);
        Task<IEnumerable<PaymentDTO>> BrowseAllAsync(int? RentalAgreementId = null, string? method = null, DateTime? from = null, DateTime? to = null);
        Task DeleteAsync(int id);
        Task<PaymentDTO> UpdateAsync(UpdatePayment updatePayment, int id);
    }
}
