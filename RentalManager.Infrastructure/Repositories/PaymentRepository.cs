using Microsoft.EntityFrameworkCore;
using RentalManager.Core.Domain;
using RentalManager.Core.Repositories;
using RentalManager.Infrastructure.Exceptions;

namespace RentalManager.Infrastructure.Repositories;

public class PaymentRepository(AppDbContext appDbContext) : IPaymentRepository
{
    public async Task<Payment> AddAsync(Payment payment, int agreementId)
    {
        payment.AgreementId = agreementId;
        try
        {
            appDbContext.Payments.Add(payment);
            await appDbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("Unable to add payment\n" + ex.Message);
        }

        return await Task.FromResult(payment);
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
        var result = await Task.FromResult(appDbContext.Payments.FirstOrDefault(x => x.Id == id));

        if (result == null)
        {
            throw new PaymentNotFoundException(id);
        }

        return result;
    }

    public async Task<IEnumerable<Payment>> BrowseAllAsync(int? agreementId = null,
        string? method = null,
        DateTime? from = null,
        DateTime? to = null)
    {
        var result = appDbContext.Payments.AsQueryable();
        if (agreementId != null)
        {
            result = result.Where(x => x.AgreementId == agreementId);
        }

        if (method != null)
        {
            result = result.Where(x => x.Method == method);
        }

        if (from != null)
        {
            result = result.Where(x => x.DateAdded.Date > from.Value.Date);
        }

        if (to != null)
        {
            result = result.Where(x => x.DateAdded.Date < to.Value.Date);
        }

        return await Task.FromResult(result.AsEnumerable());
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
        await appDbContext.SaveChangesAsync();

        return await Task.FromResult(paymentToUpdate);
    }
}