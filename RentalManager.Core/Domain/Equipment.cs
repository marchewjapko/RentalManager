namespace RentalManager.Core.Domain;

public class Equipment : DomainBase
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Price { get; set; }

    public byte[]? Image { get; set; }
}