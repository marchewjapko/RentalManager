using RentalManager.Core.Domain;
using RentalManager.Global.Queries;

namespace RentalManager.Core.Repositories;

public interface IEquipmentRepository
{
    Task<Equipment> AddAsync(Equipment equipment);

    Task<Equipment> GetAsync(int id);

    Task<IEnumerable<Equipment>> GetAsync(List<int> ids);

    Task DeleteAsync(int id);

    Task<Equipment> UpdateAsync(Equipment equipment, int id);

    Task<IEnumerable<Equipment>> BrowseAllAsync(QueryEquipment queryEquipment);

    Task Deactivate(int id);
}