using Microsoft.EntityFrameworkCore;
using RentalManager.Core.Domain;
using RentalManager.Core.Repositories;

namespace RentalManager.Infrastructure.Repositories
{
    public class RentalAgreementRepository : IRentalAgreementRepository
    {
        private readonly AppDbContext _appDbContext;
        public RentalAgreementRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<RentalAgreement> AddAsync(RentalAgreement rentalAgreement)
        {
            try
            {
                rentalAgreement.Client = _appDbContext.Clients.FirstOrDefault(x => x.Id == rentalAgreement.ClientId);
                rentalAgreement.Employee = _appDbContext.Employees.FirstOrDefault(x => x.Id == rentalAgreement.EmployeeId);
                _appDbContext.RentalAgreements.Add(rentalAgreement);
                await _appDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to add rental agreement\n" + ex.Message);
            }
            return await Task.FromResult(rentalAgreement);
        }

        public async Task DeleteAsync(int id)
        {
            var result = await _appDbContext.RentalAgreements.FirstOrDefaultAsync(x => x.Id == id);
            if (result == null)
            {
                throw new Exception("Unable to find rental agreement");
            }
            _appDbContext.RentalAgreements.Remove(result);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<RentalAgreement> GetAsync(int id)
        {
            var result = await Task.FromResult(_appDbContext.RentalAgreements.Include(x => x.RentalEquipment).Include(x => x.Client).Include(x => x.Employee).Include(x => x.Payments).FirstOrDefault(x => x.Id == id));
            if (result == null)
            {
                throw new Exception("Unable to find rental agreement");
            }
            return result;
        }

        public async Task<IEnumerable<RentalAgreement>> BrowseAllAsync(
            int? clientId = null,
            string? surname = null,
            string? phoneNumber = null,
            string? city = null,
            string? street = null,
            int? rentalEquipmentId = null,
            bool onlyUnpaid = false,
            DateTime? from = null,
            DateTime? to = null)
        {
            var result = _appDbContext.RentalAgreements.Include(x => x.RentalEquipment).Include(x => x.Client).Include(x => x.Employee).Include(x => x.Payments).AsQueryable();
            if (clientId != null)
            {
                result = result.Where(x => x.ClientId == clientId);
            }
            if(surname != null)
            {
                result = result.Where(x => x.Client.Surname.Contains(surname));
            }
            if (phoneNumber != null)
            {
                result = result.Where(x => x.Client.PhoneNumber.Contains(phoneNumber));
            }
            if (city != null)
            {
                result = result.Where(x => x.Client.City.Contains(city));
            }
            if (street != null)
            {
                result = result.Where(x => x.Client.Street.Contains(street));
            }
            if (rentalEquipmentId != null)
            {
                result = result.Where(x => x.RentalEquipment.Any(a => a.Id == rentalEquipmentId));
            }
            if (onlyUnpaid)
            {
                result = result.Where(x => x.Payments.OrderByDescending(x => x.To).First().To < DateTime.Now.Date);
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

        public async Task<RentalAgreement> UpdateAsync(RentalAgreement rentalAgreement, int id)
        {
            try
            {
                var x = _appDbContext.RentalAgreements.Include(x => x.RentalEquipment).Include(x => x.Client).Include(x => x.Employee).Include(x => x.Payments).FirstOrDefault(x => x.Id == id);
                x.IsActive = rentalAgreement.IsActive;
                x.EmployeeId = rentalAgreement.EmployeeId;
                x.Employee = _appDbContext.Employees.FirstOrDefault(x => x.Id == rentalAgreement.EmployeeId);
                x.ClientId = rentalAgreement.ClientId;
                x.Client = _appDbContext.Clients.FirstOrDefault(x => x.Id == rentalAgreement.ClientId);
                x.Comment = rentalAgreement.Comment;
                x.Deposit = rentalAgreement.Deposit;
                x.RentalEquipment = rentalAgreement.RentalEquipment;
                x.TransportFrom = rentalAgreement.TransportFrom;
                x.TransportTo = rentalAgreement.TransportTo;
                x.DateAdded = rentalAgreement.DateAdded;
                await _appDbContext.SaveChangesAsync();
                return await Task.FromResult(x);
            }
            catch (Exception)
            {
                throw new Exception("Unable to update agreement");
            }
        }
    }
}
