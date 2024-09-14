namespace RentalManager.Core.Domain;

public abstract class DomainBase
{
    public int Id { get; set; }

    public DateTime CreatedTs { get; set; } = DateTime.Now;

    public DateTime? UpdatedTs { get; set; }

    public bool IsActive { get; set; } = true;

    public int CreatedBy { get; set; }
}