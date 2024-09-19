using System.Security.Claims;
using AutoMapper;
using RentalManager.Core.Domain;
using RentalManager.Core.Repositories;
using RentalManager.Global.Queries;
using RentalManager.Infrastructure.Extensions;
using RentalManager.Infrastructure.Models.Commands.EquipmentCommands;
using RentalManager.Infrastructure.Models.DTO;

namespace RentalManager.Infrastructure.Services.EquipmentService;

public class EquipmentService(
    IEquipmentRepository equipmentRepository,
    IMapper mapper) : IEquipmentService
{
    public async Task<EquipmentDto> AddAsync(CreateEquipmentCommand createEquipment, ClaimsPrincipal user)
    {
        var newEquipment = mapper.Map<Equipment>(createEquipment);
        newEquipment.CreatedBy = user.GetId();

        var result = await equipmentRepository.AddAsync(newEquipment);

        return mapper.Map<EquipmentDto>(result);
    }

    public async Task<IEnumerable<EquipmentDto>> BrowseAllAsync(QueryEquipment queryEquipment)
    {
        var result = await equipmentRepository.BrowseAllAsync(queryEquipment);

        return mapper.Map<IEnumerable<EquipmentDto>>(result);
    }

    public Task DeleteAsync(int id)
    {
        return equipmentRepository.DeleteAsync(id);
    }

    public async Task<EquipmentDto> GetAsync(int id)
    {
        var result = await equipmentRepository.GetAsync(id);

        return mapper.Map<EquipmentDto>(result);
    }

    public async Task<EquipmentDto> UpdateAsync(UpdateEquipmentCommand updateEquipment, int id)
    {
        var result =
            await equipmentRepository.UpdateAsync(mapper.Map<Equipment>(updateEquipment), id);

        return mapper.Map<EquipmentDto>(result);
    }

    public Task Deactivate(int id)
    {
        return equipmentRepository.Deactivate(id);
    }
}