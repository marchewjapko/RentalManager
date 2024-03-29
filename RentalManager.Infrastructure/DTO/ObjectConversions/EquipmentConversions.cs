﻿using RentalManager.Core.Domain;
using RentalManager.Infrastructure.Commands.EquipmentCommands;
using RentalManager.Infrastructure.Extensions;

namespace RentalManager.Infrastructure.DTO.ObjectConversions;

public static class EquipmentConversions
{
    public static Equipment ToDomain(this CreateEquipment createEquipment)
    {
        return new Equipment
        {
            Name = createEquipment.Name,
            Price = createEquipment.Price,
            Image = createEquipment.Image.ToByteArray()
        };
    }

    public static EquipmentDto ToDto(this Equipment equipment)
    {
        return new EquipmentDto
        {
            Id = equipment.Id,
            Name = equipment.Name,
            Price = equipment.Price,
            Image = equipment.Image
        };
    }

    public static Equipment ToDomain(this UpdateEquipment updateEquipment)
    {
        var result = new Equipment
        {
            Name = updateEquipment.Name,
            Price = updateEquipment.Price,
            Image = updateEquipment.Image.ToByteArray()
        };

        return result;
    }
}