namespace RentalManager.Infrastructure.Models.DTO;

public class EquipmentDto
{
    public int Id { get; init; }

    public required string Name { get; init; }

    public int Price { get; init; }
}