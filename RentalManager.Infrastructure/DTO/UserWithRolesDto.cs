using Microsoft.AspNetCore.Identity;

namespace RentalManager.Infrastructure.DTO;

public class UserWithRolesDto : UserDto
{
    public IList<string> Roles { get; set; } = null!;
}