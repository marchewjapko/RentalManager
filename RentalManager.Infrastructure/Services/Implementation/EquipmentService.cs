using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using RentalManager.Core.Domain;
using RentalManager.Core.Repositories;
using RentalManager.Global.Queries;
using RentalManager.Infrastructure.Commands.EquipmentCommands;
using RentalManager.Infrastructure.DTO;
using RentalManager.Infrastructure.DTO.ObjectConversions;
using RentalManager.Infrastructure.Services.Interfaces;

namespace RentalManager.Infrastructure.Services.Implementation;

public class EquipmentService(IEquipmentRepository equipmentRepository,
    UserManager<User> userManager) : IEquipmentService
{
    public async Task AddAsync(CreateEquipment createEquipment, ClaimsPrincipal user)
    {
        var newEquipment = createEquipment.ToDomain();
        newEquipment.User = (await userManager.GetUserAsync(user))!;

        await equipmentRepository.AddAsync(newEquipment);
    }

    public async Task<IEnumerable<EquipmentDto>> BrowseAllAsync(QueryEquipment queryEquipment)
    {
        var result = await equipmentRepository.BrowseAllAsync(queryEquipment);

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

    public async Task UpdateAsync(UpdateEquipment updateEquipment, int id)
    {
        await equipmentRepository.UpdateAsync(updateEquipment.ToDomain(), id);
    }

    public async Task Deactivate(int id)
    {
        await equipmentRepository.Deactivate(id);
    }
}