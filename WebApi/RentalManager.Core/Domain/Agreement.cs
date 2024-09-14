// ReSharper disable ClassWithVirtualMembersNeverInherited.Global

namespace RentalManager.Core.Domain;

public class Agreement : DomainBase
{
    public int EmployeeId { get; set; }

    public int ClientId { get; set; }

    public Client Client { get; set; } = null!;

    public string? Comment { get; set; }

    public int Deposit { get; set; }

    public int? TransportFromPrice { get; set; }

    public int TransportToPrice { get; set; }

    public DateTime DateAdded { get; set; }

    public virtual ICollection<Equipment> Equipment { get; set; } = null!;

    public virtual ICollection<Payment> Payments { get; set; } = null!;
}