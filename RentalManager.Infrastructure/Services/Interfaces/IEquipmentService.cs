using System.Security.Claims;
using RentalManager.Global.Queries;
using RentalManager.Infrastructure.Commands.EquipmentCommands;
using RentalManager.Infrastructure.DTO;

namespace RentalManager.Infrastructure.Services.Interfaces;

public interface IEquipmentService
{
    Task AddAsync(CreateEquipment createEquipment, ClaimsPrincipal user);
    Task<EquipmentDto> GetAsync(int id);

    Task<IEnumerable<EquipmentDto>> BrowseAllAsync(QueryEquipment queryEquipment);

    Task DeleteAsync(int id);
    Task UpdateAsync(UpdateEquipment updateEquipment, int id);

    Task Deactivate(int id);
}