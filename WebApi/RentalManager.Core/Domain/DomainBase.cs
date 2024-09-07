using System.ComponentModel.DataAnnotations.Schema;

// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable UnusedMember.Global

namespace RentalManager.Core.Domain;

public abstract class DomainBase
{
    public DateTime CreatedTs { get; set; } = DateTime.Now;

    public DateTime? UpdatedTs { get; set; }

    public bool IsActive { get; set; } = true;

    [ForeignKey("User")]
    public int CreatedBy { get; set; }

    public User User { get; set; } = null!;
}