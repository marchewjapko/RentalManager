using RentalManager.Core.Repositories;
using RentalManager.Infrastructure.Commands.RentalEquipmentCommands;
using RentalManager.Infrastructure.DTO;
using RentalManager.Infrastructure.DTO.ObjectConversions;

namespace RentalManager.Infrastructure.Services;

public class RentalEquipmentService : IRentalEquipmentService
{
    private readonly IRentalEquipmentRepository _rentalEquipmentRepository;

    public RentalEquipmentService(IRentalEquipmentRepository rentalEquipmentRepository)
    {
        _rentalEquipmentRepository = rentalEquipmentRepository;
    }

    public async Task<RentalEquipmentDto> AddAsync(CreateRentalEquipment createRentalEquipment)
    {
        if (!ValidateRentalEquipment(createRentalEquipment.ToDto())) throw new Exception("Invalid rental equipment");

        var result = await _rentalEquipmentRepository.AddAsync(createRentalEquipment.ToDomain());

        return result.ToDto();
    }

    public async Task<IEnumerable<RentalEquipmentDto>> BrowseAllAsync(string? name = null,
        DateTime? from = null,
        DateTime? to = null)
    {
        var result = await _rentalEquipmentRepository.BrowseAllAsync(name, from, to);

        return await Task.FromResult(result.Select(x => x.ToDto()));
    }

    public async Task DeleteAsync(int id)
    {
        await _rentalEquipmentRepository.DeleteAsync(id);
    }

    public async Task<RentalEquipmentDto> GetAsync(int id)
    {
        var result = await _rentalEquipmentRepository.GetAsync(id);

        return await Task.FromResult(result.ToDto());
    }

    public async Task<RentalEquipmentDto> UpdateAsync(UpdateRentalEquipment updateRentalEquipment, int id)
    {
        if (!ValidateRentalEquipment(updateRentalEquipment.ToDto())) throw new Exception("Invalid rental equipment");

        var result = await _rentalEquipmentRepository.UpdateAsync(updateRentalEquipment.ToDomain(), id);

        return await Task.FromResult(result.ToDto());
    }

    private static bool ValidateRentalEquipment(RentalEquipmentDto rentalEquipmentDto)
    {
        if (rentalEquipmentDto.Price < 0) return false;

        return true;
    }
}