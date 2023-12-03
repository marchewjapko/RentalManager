using RentalManager.Core.Domain;
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
            DateAdded = DateTime.Now,
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
            DateAdded = equipment.DateAdded,
            Image = equipment.Image
        };
    }

    public static EquipmentDto ToDto(this CreateEquipment createEquipment)
    {
        return new EquipmentDto
        {
            Name = createEquipment.Name,
            Price = createEquipment.Price,
            Image = createEquipment.Image.ToByteArray()
        };
    }

    public static EquipmentDto ToDto(this UpdateEquipment updateEquipment)
    {
        return new EquipmentDto
        {
            Name = updateEquipment.Name,
            Price = updateEquipment.Price,
            Image = updateEquipment.Image.ToByteArray()
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

    public static Equipment ToDomain(this EquipmentDto equipmentDto)
    {
        var result = new Equipment
        {
            Id = equipmentDto.Id,
            Name = equipmentDto.Name,
            Price = equipmentDto.Price,
            DateAdded = equipmentDto.DateAdded,
            Image = equipmentDto.Image
        };

        return result;
    }
}