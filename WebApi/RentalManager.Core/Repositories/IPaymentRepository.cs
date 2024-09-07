using RentalManager.Core.Domain;
using RentalManager.Global.Queries;

namespace RentalManager.Core.Repositories;

public interface IPaymentRepository
{
    Task<Payment> AddAsync(Payment payment);

    Task<Payment> GetAsync(int id);

    Task DeleteAsync(int id);

    Task<Payment> UpdateAsync(Payment payment, int id);

    Task<IEnumerable<Payment>> BrowseAllAsync(QueryPayment queryPayment);

    Task Deactivate(int id);
}