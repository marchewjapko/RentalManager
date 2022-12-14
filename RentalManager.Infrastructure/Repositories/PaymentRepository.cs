using Microsoft.EntityFrameworkCore;
using RentalManager.Core.Domain;
using RentalManager.Core.Repositories;

namespace RentalManager.Infrastructure.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly AppDbContext _appDbContext;
        public PaymentRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<Payment> AddAsync(Payment payment)
        {
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
                throw new Exception("Unable to find payment");
            }
            _appDbContext.Payments.Remove(result);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<Payment> GetAsync(int id)
        {
            var result = await Task.FromResult(_appDbContext.Payments.FirstOrDefault(x => x.Id == id));
            if (result == null)
            {
                throw new Exception("Unable to find payment");
            }
            return result;
        }

        public async Task<IEnumerable<Payment>> BrowseAllAsync(int? RentalAgreementId = null, string? method = null, DateTime? from = null, DateTime? to = null)
        {
            var result = _appDbContext.Payments.AsQueryable();
            if(RentalAgreementId != null)
            {
                result = result.Where(x => x.RentalAgreementId == RentalAgreementId);
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
            try
            {
                var z = _appDbContext.Payments.FirstOrDefault(x => x.Id == id);
                z.RentalAgreementId = payment.RentalAgreementId;
                z.Method = payment.Method;
                z.Amount = payment.Amount;
                _appDbContext.SaveChanges();
                return await Task.FromResult(z);
            }
            catch (Exception)
            {
                throw new Exception("Unable to update payment");
            }
        }
    }
}
