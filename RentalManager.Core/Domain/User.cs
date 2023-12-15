using Microsoft.AspNetCore.Identity;

namespace RentalManager.Core.Domain;

public class User : IdentityUser<int>
{
    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public override string UserName { get; set; } = null!;

    public Gender Gender { get; set; }

    public byte[]? Image { get; set; }

    public bool IsConfirmed { get; set; } = false;

    public DateTime CreatedTs { get; set; } = DateTime.Now;
    
    public DateTime? UpdatedTs { get; set; } = null;
}