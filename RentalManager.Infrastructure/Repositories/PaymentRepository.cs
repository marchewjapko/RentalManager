using Microsoft.EntityFrameworkCore;
using RentalManager.Core.Domain;
using RentalManager.Core.Repositories;
using RentalManager.Infrastructure.Exceptions;

namespace RentalManager.Infrastructure.Repositories;

public class PaymentRepository : IPaymentRepository
{
    private readonly AppDbContext _appDbContext;

    public PaymentRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<Payment> AddAsync(Payment payment, int agreementId)
    {
        payment.AgreementId = agreementId;
        try
        {
            _appDbContext.Payments.Add(payment);
            await _appDbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("Unable to add payment\n" + ex.Message);
        }

        return await Task.FromResult(payment);
    }

    public async Task DeleteAsync(int id)
    {
        var result = await _appDbContext.Payments.FirstOrDefaultAsync(x => x.Id == id);

        if (result == null)
        {
            throw new PaymentNotFoundException(id);
        }

        _appDbContext.Payments.Remove(result);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task<Payment> GetAsync(int id)
    {
        var result = await Task.FromResult(_appDbContext.Payments.FirstOrDefault(x => x.Id == id));

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
        var result = _appDbContext.Payments.AsQueryable();
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
        var z = _appDbContext.Payments.FirstOrDefault(x => x.Id == id);

        if (z == null)
        {
            throw new PaymentNotFoundException(id);
        }

        z.AgreementId = payment.AgreementId;
        z.Method = payment.Method;
        z.Amount = payment.Amount;
        z.DateFrom = payment.DateFrom;
        z.DateTo = payment.DateTo;
        await _appDbContext.SaveChangesAsync();

        return await Task.FromResult(z);
    }
}