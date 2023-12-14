using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using RentalManager.Core.Domain;
using RentalManager.Core.Repositories;
using RentalManager.Infrastructure.Commands.EquipmentCommands;
using RentalManager.Infrastructure.DTO;
using RentalManager.Infrastructure.DTO.ObjectConversions;
using RentalManager.Infrastructure.Services.Interfaces;

namespace RentalManager.Infrastructure.Services.Implementation;

public class EquipmentService(IEquipmentRepository equipmentRepository,
    UserManager<User> userManager) : IEquipmentService
{
    public async Task<EquipmentDto> AddAsync(CreateEquipment createEquipment, ClaimsPrincipal user)
    {
        var newEquipment = createEquipment.ToDomain();
        newEquipment.User = (await userManager.GetUserAsync(user))!;

        var result = await equipmentRepository.AddAsync(newEquipment);

        return result.ToDto();
    }

    public async Task<IEnumerable<EquipmentDto>> BrowseAllAsync(string? name = null,
        DateTime? from = null,
        DateTime? to = null)
    {
        var result = await equipmentRepository.BrowseAllAsync(name, from, to);

        return await Task.FromResult(result.Select(x => x.ToDto()));
    }

    public async Task DeleteAsync(int id)
    {
        await equipmentRepository.DeleteAsync(id);
    }

    public async Task<EquipmentDto> GetAsync(int id)
    {
        var result = await equipmentRepository.GetAsync(id);

        return await Task.FromResult(result.ToDto());
    }

    public async Task<EquipmentDto> UpdateAsync(UpdateEquipment updateEquipment, int id)
    {
        var result = await equipmentRepository.UpdateAsync(updateEquipment.ToDomain(), id);

        return await Task.FromResult(result.ToDto());
    }

    public async Task Deactivate(int id)
    {
        await equipmentRepository.Deactivate(id);
    }
}