namespace RentalManager.Core.Domain;

public class RentalEquipment
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Price { get; set; }

    public DateTime DateAdded { get; set; }

    public ICollection<RentalAgreement> RentalAgreements { get; set; } = null!;
}