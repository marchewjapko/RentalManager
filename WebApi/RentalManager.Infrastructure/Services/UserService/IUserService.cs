using RentalManager.Infrastructure.Models.DTO;

namespace RentalManager.Infrastructure.Services.UserService;

public interface IUserService
{
    Task<UserDto> GetAsync(int id);

    Task<IEnumerable<UserWithRolesDto>> BrowseAllAsync();
}