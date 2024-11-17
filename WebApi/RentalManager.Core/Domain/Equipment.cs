using System.ComponentModel.DataAnnotations;

namespace RentalManager.Core.Domain;

public class Equipment : DomainBase
{
    [MaxLength(100)]
    public required string Name { get; set; }

    public int Price { get; set; }

    public ICollection<Agreement> Agreements { get; set; } = [];
}