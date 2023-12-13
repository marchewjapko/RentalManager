using Microsoft.AspNetCore.Identity;

namespace RentalManager.Core.Domain;

public class Employee : IdentityUser<int>
{
    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public override string UserName { get; set; } = null!;

    public Gender Gender { get; set; }

    public byte[]? Image { get; set; }

    public DateTime DateAdded { get; set; }
}