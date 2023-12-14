using System.ComponentModel.DataAnnotations.Schema;

namespace RentalManager.Core.Domain;

public abstract class DomainBase
{
    public DateTime CreatedTs { get; set; } = DateTime.Now;

    public DateTime? UpdatedTs { get; set; } = null;

    public bool IsActive { get; set; } = true;

    [ForeignKey("User")]
    public int CreatedBy { get; set; }

    public virtual User User { get; set; } = null!;
}