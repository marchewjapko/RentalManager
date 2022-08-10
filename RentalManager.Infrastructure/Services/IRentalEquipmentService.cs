using RentalManager.Infrastructure.Commands;
using RentalManager.Infrastructure.DTO;

namespace RentalManager.Infrastructure.Services
{
    public interface IRentalEquipmentService
    {
        Task<RentalEquipmentDTO> AddAsync(CreateRentalEquipment createRentalEquipment);
        Task<RentalEquipmentDTO> GetAsync(int id);
        Task<IEnumerable<RentalEquipmentDTO>> BrowseAllAsync(string? name = null, DateTime? from = null, DateTime? to = null);
        Task DeleteAsync(int id);
        Task<RentalEquipmentDTO> UpdateAsync(UpdateRentalEquipment updateRentalEquipment, int id);
    }
}
