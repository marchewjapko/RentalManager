using RentalManager.Infrastructure.Commands.RentalEquipmentCommands;
using RentalManager.Infrastructure.DTO;

namespace RentalManager.Infrastructure.Services;

public interface IRentalEquipmentService
{
    Task<RentalEquipmentDto> AddAsync(CreateRentalEquipment createRentalEquipment);
    Task<RentalEquipmentDto> GetAsync(int id);

    Task<IEnumerable<RentalEquipmentDto>> BrowseAllAsync(string? name = null,
        DateTime? from = null,
        DateTime? to = null);

    Task DeleteAsync(int id);
    Task<RentalEquipmentDto> UpdateAsync(UpdateRentalEquipment updateRentalEquipment, int id);
}