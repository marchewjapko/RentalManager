using RentalManager.Core.Domain;

namespace RentalManager.Core.Repositories;

public interface IEquipmentRepository
{
    Task<Equipment> AddAsync(Equipment equipment);
    Task<Equipment> GetAsync(int id);
    Task<IEnumerable<Equipment>> GetAsync(List<int> ids);
    Task DeleteAsync(int id);
    Task<Equipment> UpdateAsync(Equipment equipment, int id);

    Task<IEnumerable<Equipment>> BrowseAllAsync(string? name = null,
        DateTime? from = null,
        DateTime? to = null);

    Task Deactivate(int id);
}