using RentalManager.Core.Domain;

namespace RentalManager.Core.Repositories
{
    public interface IRentalEquipmentRepository
    {
        Task<RentalEquipment> AddAsync(RentalEquipment rentalEquipment);
        Task<RentalEquipment> GetAsync(int id);
        Task<IEnumerable<RentalEquipment>> GetAsync(List<int> ids);
        Task DeleteAsync(int id);
        Task<RentalEquipment> UpdateAsync(RentalEquipment rentalEquipment, int id);
        Task<IEnumerable<RentalEquipment>> BrowseAllAsync(string? name = null, DateTime? from = null, DateTime? to = null);
    }
}
