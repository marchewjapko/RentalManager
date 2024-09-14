// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace RentalManager.Infrastructure.Models.DTO;

public class UserWithRolesDto : UserDto
{
    public IList<string> Roles { get; set; } = null!;
}