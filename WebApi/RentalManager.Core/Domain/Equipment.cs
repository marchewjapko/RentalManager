namespace RentalManager.Core.Domain;

public class Equipment : DomainBase
{
    public string Name { get; set; }

    public int Price { get; set; }

    public ICollection<Agreement> Agreements { get; set; }
}