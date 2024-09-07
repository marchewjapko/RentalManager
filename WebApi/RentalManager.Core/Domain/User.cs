using Microsoft.AspNetCore.Identity;

namespace RentalManager.Core.Domain;

public class User : IdentityUser<int>
{
    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

#pragma warning disable CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
    public override string UserName { get; set; } = null!;
#pragma warning restore CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).

    public byte[]? Image { get; set; }

    public bool IsConfirmed { get; set; }

    public DateTime CreatedTs { get; set; } = DateTime.Now;

    public DateTime? UpdatedTs { get; set; }

    public DateTime? PasswordValidTo { get; set; }
}