using System.Security.Claims;
using RentalManager.Infrastructure.Commands.UserCommands;
using RentalManager.Infrastructure.DTO;
using RentalManager.Infrastructure.Requests;

namespace RentalManager.Infrastructure.Services.Interfaces;

public interface IUserService
{
    Task<UserDto> AddAsync(CreateUser createUser);
    Task<UserDto> GetAsync(int id);
    Task DeleteAsync(int id);
    Task<UserDto> UpdateAsync(UpdateUser updateUser, int id);

    Task<IEnumerable<UserDto>> BrowseAllAsync(string? name = null,
        DateTime? from = null,
        DateTime? to = null);

    Task Login(LoginRequest loginRequest);

    Task<UserWithRolesDto> GetCurrentUser(ClaimsPrincipal claimsPrincipal);

    Task ChangePassword(ChangePasswordRequest changePasswordRequest);

    Task ResetPassword(ResetPasswordRequest resetPasswordRequest);
}