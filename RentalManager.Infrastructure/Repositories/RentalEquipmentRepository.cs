using Microsoft.EntityFrameworkCore;
using RentalManager.Core.Domain;
using RentalManager.Core.Repositories;

namespace RentalManager.Infrastructure.Repositories
{
    public class RentalEquipmentRepository : IRentalEquipmentRepository
    {
        private readonly AppDbContext _appDbContext;
        public RentalEquipmentRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<RentalEquipment> AddAsync(RentalEquipment rentalEquipment)
        {
            try
            {
                _appDbContext.RentalEquipment.Add(rentalEquipment);
                await _appDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to add rental equipment\n" + ex.Message);
            }
            return await Task.FromResult(rentalEquipment);
        }

        public async Task DeleteAsync(int id)
        {
            var result = await _appDbContext.RentalEquipment.FirstOrDefaultAsync(x => x.Id == id);
            if (result == null)
            {
                throw new Exception("Unable to find rental equipment");
            }
            _appDbContext.RentalEquipment.Remove(result);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<RentalEquipment> GetAsync(int id)
        {
            var result = await Task.FromResult(_appDbContext.RentalEquipment.FirstOrDefault(x => x.Id == id));
            if (result == null)
            {
                throw new Exception("Unable to find rental equipment");
            }
            return result;
        }

        public async Task<IEnumerable<RentalEquipment>> GetAsync(List<int> ids)
        {
            var result = await Task.FromResult(_appDbContext.RentalEquipment.Where(x => ids.Any(a => a == x.Id)));
            if (result == null)
            {
                throw new Exception("Unable to find rental equipment");
            }
            return result;
        }

        public async Task<IEnumerable<RentalEquipment>> BrowseAllAsync(string? name = null, DateTime? from = null, DateTime? to = null)
        {
            var result = _appDbContext.RentalEquipment.AsQueryable();
            if (name != null)
            {
                result = result.Where(x => x.Name.Contains(name));
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

        public async Task<RentalEquipment> UpdateAsync(RentalEquipment rentalEquipment, int id)
        {
            try
            {
                var z = _appDbContext.RentalEquipment.FirstOrDefault(x => x.Id == id);
                z.Name = rentalEquipment.Name;
                z.MonthlyPrice = rentalEquipment.MonthlyPrice;
                _appDbContext.SaveChanges();
                return await Task.FromResult(z);
            }
            catch (Exception)
            {
                throw new Exception("Unable to update rental equipment");
            }
        }
    }
}
