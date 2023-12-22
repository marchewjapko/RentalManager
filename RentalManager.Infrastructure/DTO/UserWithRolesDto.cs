// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace RentalManager.Infrastructure.DTO;

public class UserWithRolesDto : UserDto
{
    public IList<string> Roles { get; set; } = null!;
}