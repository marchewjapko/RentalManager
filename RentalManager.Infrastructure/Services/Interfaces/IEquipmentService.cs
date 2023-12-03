﻿using RentalManager.Infrastructure.Commands.EquipmentCommands;
using RentalManager.Infrastructure.DTO;

namespace RentalManager.Infrastructure.Services;

public interface IEquipmentService
{
    Task<EquipmentDto> AddAsync(CreateEquipment createEquipment);
    Task<EquipmentDto> GetAsync(int id);

    Task<IEnumerable<EquipmentDto>> BrowseAllAsync(string? name = null,
        DateTime? from = null,
        DateTime? to = null);

    Task DeleteAsync(int id);
    Task<EquipmentDto> UpdateAsync(UpdateEquipment updateEquipment, int id);
}