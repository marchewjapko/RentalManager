using Microsoft.EntityFrameworkCore;
using RentalManager.Core.Domain;
using RentalManager.Core.Repositories;
using RentalManager.Global.Queries;
using RentalManager.Infrastructure.ExceptionHandling.Exceptions;
using RentalManager.Infrastructure.Extensions;
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
        var result = appDbContext.Payments.AsQueryable();

        result = FilterPayments(result, queryPayment);

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

    private static IQueryable<Payment> FilterPayments(IQueryable<Payment> payments,
        QueryPayment queryPayments)
    {
        payments = payments.Filter(x => x.AgreementId, queryPayments.AgreementId, FilterOperand.Equals);
        payments = payments.Filter(x => x.Method, queryPayments.Method, FilterOperand.Contains);
        payments = payments.Filter(x => x.DateFrom, queryPayments.ValidRangeTo?.Date, FilterOperand.LessThanOrEqualTo);
        payments = payments.Filter(x => x.DateTo, queryPayments.ValidRangeFrom?.Date, FilterOperand.GreaterThanOrEqualTo);

        if (queryPayments.OnlyActive)
        {
            payments = payments.Filter(x => x.IsActive, true, FilterOperand.Equals);
        }

        return payments;
    }
}