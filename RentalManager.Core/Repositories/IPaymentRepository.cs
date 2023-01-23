using RentalManager.Core.Domain;

namespace RentalManager.Core.Repositories
{
    public interface IPaymentRepository
    {
        Task<Payment> AddAsync(Payment rentalPayment, int rentalAgreementId);
        Task<Payment> GetAsync(int id);
        Task DeleteAsync(int id);
        Task<Payment> UpdateAsync(Payment rentalPayment, int id);
        Task<IEnumerable<Payment>> BrowseAllAsync(int? RentalAgreementId = null, string? method = null, DateTime? from = null, DateTime? to = null);
    }
}
