using RentalManager.Core.Repositories;
using RentalManager.Infrastructure.Commands;
using RentalManager.Infrastructure.DTO;

namespace RentalManager.Infrastructure.Services
{
    public class RentalEquipmentService : IRentalEquipmentService
    {
        private readonly IRentalEquipmentRepository _rentalEquipmentRepository;
        public RentalEquipmentService(IRentalEquipmentRepository rentalEquipmentRepository)
        {
            _rentalEquipmentRepository = rentalEquipmentRepository;
        }

        public async Task<RentalEquipmentDTO> AddAsync(CreateRentalEquipment createRentalEquipment)
        {
            var result = await _rentalEquipmentRepository.AddAsync(createRentalEquipment.ToDomain());
            return result.ToDTO();
        }

        public async Task<IEnumerable<RentalEquipmentDTO>> BrowseAllAsync(string? name = null, DateTime? from = null, DateTime? to = null)
        {
            var result = await _rentalEquipmentRepository.BrowseAllAsync(name, from, to);
            return await Task.FromResult(result.Select(x => x.ToDTO()));
        }

        public async Task DeleteAsync(int id)
        {
            await _rentalEquipmentRepository.DeleteAsync(id);
        }

        public async Task<RentalEquipmentDTO> GetAsync(int id)
        {
            var result = await _rentalEquipmentRepository.GetAsync(id);
            return await Task.FromResult(result.ToDTO());
        }

        public async Task<RentalEquipmentDTO> UpdateAsync(UpdateRentalEquipment updateRentalEquipment, int id)
        {
            var result = await _rentalEquipmentRepository.UpdateAsync(updateRentalEquipment.ToDomain(), id);
            return await Task.FromResult(result.ToDTO());
        }
    }
}
