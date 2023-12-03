namespace RentalManager.Infrastructure.DTO;

public class EquipmentDto
{
    public int Id { get; init; }

    public string Name { get; init; } = null!;

    public int Price { get; init; }

    public byte[]? Image { get; set; }

    public DateTime DateAdded { get; init; }
}