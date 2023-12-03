using RentalManager.Core.Domain;

namespace RentalManager.Core.Repositories;

public interface IPaymentRepository
{
    Task<Payment> AddAsync(Payment payment, int agreementId);
    Task<Payment> GetAsync(int id);
    Task DeleteAsync(int id);
    Task<Payment> UpdateAsync(Payment payment, int id);

    Task<IEnumerable<Payment>> BrowseAllAsync(int? agreementId = null,
        string? method = null,
        DateTime? from = null,
        DateTime? to = null);
}