namespace RentalManager.Infrastructure.DTO;

public class RentalEquipmentDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int Price { get; set; }
    public DateTime DateAdded { get; set; }
}