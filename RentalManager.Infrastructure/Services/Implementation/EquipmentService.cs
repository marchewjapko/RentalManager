using RentalManager.Core.Repositories;
using RentalManager.Infrastructure.Commands.EquipmentCommands;
using RentalManager.Infrastructure.DTO;
using RentalManager.Infrastructure.DTO.ObjectConversions;

namespace RentalManager.Infrastructure.Services.Implementation;

public class EquipmentService : IEquipmentService
{
    private readonly IEquipmentRepository _equipmentRepository;

    public EquipmentService(IEquipmentRepository equipmentRepository)
    {
        _equipmentRepository = equipmentRepository;
    }

    public async Task<EquipmentDto> AddAsync(CreateEquipment createEquipment)
    {
        if (!ValidateRentalEquipment(createEquipment.ToDto()))
        {
            throw new Exception("Invalid rental equipment");
        }

        var result = await _equipmentRepository.AddAsync(createEquipment.ToDomain());

        return result.ToDto();
    }

    public async Task<IEnumerable<EquipmentDto>> BrowseAllAsync(string? name = null,
        DateTime? from = null,
        DateTime? to = null)
    {
        var result = await _equipmentRepository.BrowseAllAsync(name, from, to);

        return await Task.FromResult(result.Select(x => x.ToDto()));
    }

    public async Task DeleteAsync(int id)
    {
        await _equipmentRepository.DeleteAsync(id);
    }

    public async Task<EquipmentDto> GetAsync(int id)
    {
        var result = await _equipmentRepository.GetAsync(id);

        return await Task.FromResult(result.ToDto());
    }

    public async Task<EquipmentDto> UpdateAsync(UpdateEquipment updateEquipment, int id)
    {
        if (!ValidateRentalEquipment(updateEquipment.ToDto()))
        {
            throw new Exception("Invalid rental equipment");
        }

        var result = await _equipmentRepository.UpdateAsync(updateEquipment.ToDomain(), id);

        return await Task.FromResult(result.ToDto());
    }

    private static bool ValidateRentalEquipment(EquipmentDto equipmentDto)
    {
        if (equipmentDto.Price < 0)
        {
            return false;
        }

        return true;
    }
}