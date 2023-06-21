using RentalManager.Core.Domain;
using RentalManager.Infrastructure.Commands;

namespace RentalManager.Infrastructure.DTO.ObjectConversions;

public static class RentalEquipmentConversions
{
    public static RentalEquipment ToDomain(this CreateRentalEquipment createRentalEquipment)
    {
        return new RentalEquipment
        {
            Name = createRentalEquipment.Name,
            Price = createRentalEquipment.Price,
            DateAdded = DateTime.Now
        };
    }

    public static RentalEquipmentDto ToDto(this RentalEquipment rentalEquipment)
    {
        return new RentalEquipmentDto
        {
            Id = rentalEquipment.Id,
            Name = rentalEquipment.Name,
            Price = rentalEquipment.Price,
            DateAdded = rentalEquipment.DateAdded
        };
    }

    public static RentalEquipmentDto ToDto(this CreateRentalEquipment createRentalEquipment)
    {
        return new RentalEquipmentDto
        {
            Name = createRentalEquipment.Name,
            Price = createRentalEquipment.Price
        };
    }

    public static RentalEquipmentDto ToDto(this UpdateRentalEquipment updateRentalEquipment)
    {
        return new RentalEquipmentDto
        {
            Name = updateRentalEquipment.Name,
            Price = updateRentalEquipment.Price
        };
    }

    public static RentalEquipment ToDomain(this UpdateRentalEquipment updateRentalEquipment)
    {
        var result = new RentalEquipment
        {
            Name = updateRentalEquipment.Name,
            Price = updateRentalEquipment.Price
        };
        return result;
    }

    public static RentalEquipment ToDomain(this RentalEquipmentDto rentalEquipmentDto)
    {
        var result = new RentalEquipment
        {
            Id = rentalEquipmentDto.Id,
            Name = rentalEquipmentDto.Name,
            Price = rentalEquipmentDto.Price,
            DateAdded = rentalEquipmentDto.DateAdded
        };
        return result;
    }
}