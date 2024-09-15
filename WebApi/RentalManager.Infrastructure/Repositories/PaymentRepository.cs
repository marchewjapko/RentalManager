using Microsoft.EntityFrameworkCore;
using RentalManager.Core.Domain;
using RentalManager.Core.Repositories;
using RentalManager.Global.Queries;
using RentalManager.Infrastructure.Exceptions;
using RentalManager.Infrastructure.Repositories.DbContext;

namespace RentalManager.Infrastructure.Repositories;

public class PaymentRepository(AppDbContext appDbContext) : IPaymentRepository
{
    public async Task<Payment> AddAsync(Payment payment)
    {
        var result = appDbContext.Payments.Add(payment);
        await appDbContext.SaveChangesAsync();

        return result.Entity;
    }

    public async Task DeleteAsync(int id)
    {
        var result = await appDbContext.Payments.FirstOrDefaultAsync(x => x.Id == id);

        if (result == null)
        {
            throw new PaymentNotFoundException(id);
        }

        appDbContext.Payments.Remove(result);
        await appDbContext.SaveChangesAsync();
    }

    public async Task<Payment> GetAsync(int id)
    {
        var result = await appDbContext.Payments.FirstOrDefaultAsync(x => x.Id == id);

        if (result == null)
        {
            throw new PaymentNotFoundException(id);
        }

        return result;
    }

    public async Task<IEnumerable<Payment>> BrowseAllAsync(QueryPayment queryPayment)
    {
        var result = appDbContext.Payments.Where(x => x.IsActive)
            .AsQueryable();

        if (queryPayment.AgreementId != null)
        {
            result = result.Where(x => x.AgreementId == queryPayment.AgreementId);
        }

        if (queryPayment.Method != null)
        {
            result = result.Where(x => x.Method == queryPayment.Method);
        }

        if (queryPayment.From != null)
        {
            result = result.Where(x => x.CreatedTs.Date > queryPayment.From.Value.Date);
        }

        if (queryPayment.To != null)
        {
            result = result.Where(x => x.CreatedTs.Date < queryPayment.To.Value.Date);
        }

        if (queryPayment.OnlyActive)
        {
            result = result.Where(x => x.IsActive);
        }

        return await result.ToListAsync();
    }

    public async Task<Payment> UpdateAsync(Payment payment, int id)
    {
        var paymentToUpdate = appDbContext.Payments.FirstOrDefault(x => x.Id == id);

        if (paymentToUpdate == null)
        {
            throw new PaymentNotFoundException(id);
        }

        paymentToUpdate.AgreementId = payment.AgreementId;
        paymentToUpdate.Method = payment.Method;
        paymentToUpdate.Amount = payment.Amount;
        paymentToUpdate.DateFrom = payment.DateFrom;
        paymentToUpdate.DateTo = payment.DateTo;
        paymentToUpdate.UpdatedTs = DateTime.Now;
        await appDbContext.SaveChangesAsync();

        return paymentToUpdate;
    }

    public async Task Deactivate(int id)
    {
        var result = await appDbContext.Payments.FirstOrDefaultAsync(x => x.Id == id);

        if (result == null)
        {
            throw new PaymentNotFoundException(id);
        }

        result.IsActive = false;
        await appDbContext.SaveChangesAsync();
    }
}