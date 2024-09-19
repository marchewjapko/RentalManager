using System.Security.Claims;
using RentalManager.Global.Queries;
using RentalManager.Infrastructure.Models.Commands.EquipmentCommands;
using RentalManager.Infrastructure.Models.DTO;

namespace RentalManager.Infrastructure.Services.EquipmentService;

public interface IEquipmentService
{
    Task<EquipmentDto> AddAsync(CreateEquipmentCommand createEquipment, ClaimsPrincipal user);

    Task<EquipmentDto> GetAsync(int id);

    Task<IEnumerable<EquipmentDto>> BrowseAllAsync(QueryEquipment queryEquipment);

    Task DeleteAsync(int id);
    Task<EquipmentDto> UpdateAsync(UpdateEquipmentCommand updateEquipment, int id);

    Task Deactivate(int id);
}