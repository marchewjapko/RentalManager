namespace RentalManager.Core.Domain;

public class Agreement : DomainBase
{
    public int UserId { get; set; }

    public int ClientId { get; set; }

    public Client Client { get; set; } = null!;

    public string? Comment { get; set; }

    public int Deposit { get; set; }

    public int? TransportFromPrice { get; set; }

    public int TransportToPrice { get; set; }

    public DateTime DateAdded { get; set; }

    public ICollection<Equipment> Equipments { get; set; } = null!;

    public ICollection<Payment> Payments { get; set; } = null!;
}