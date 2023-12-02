namespace RentalManager.Infrastructure.DTO;

public class RentalEquipmentDto
{
    public int Id { get; init; }

    public string Name { get; init; } = null!;

    public int Price { get; init; }

    public DateTime DateAdded { get; init; }
}