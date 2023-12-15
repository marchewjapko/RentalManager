using System.Security.Claims;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RentalManager.Core.Domain;
using RentalManager.Core.Repositories;
using RentalManager.Global.Queries;
using RentalManager.Infrastructure.Commands.UserCommands;
using RentalManager.Infrastructure.DTO;
using RentalManager.Infrastructure.DTO.ObjectConversions;
using RentalManager.Infrastructure.Exceptions;
using RentalManager.Infrastructure.Requests;
using RentalManager.Infrastructure.Services.Interfaces;

namespace RentalManager.Infrastructure.Services.Implementation;

public class UserService(IUserRepository userRepository,
    UserManager<User> userManager,
    SignInManager<User> signInManager) : IUserService
{
    public async Task AddAsync(CreateUser createUser)
    {
        var result = await userManager.CreateAsync(createUser.ToDomain(), createUser.Password);

        if (!result.Succeeded)
        {
            var validationFailure = new List<ValidationFailure>();
            foreach (var error in result.Errors)
                validationFailure.Add(new ValidationFailure(error.Code, error.Description));

            throw new ValidationException(validationFailure);
        }
    }

    public async Task<IEnumerable<UserDto>> BrowseAllAsync(QueryUser queryUser)
    {
        var result = await userRepository.BrowseAllAsync(queryUser);

        return await Task.FromResult(result.Select(x => x.ToDto()));
    }

    public async Task DeleteAsync(int id)
    {
        await userRepository.DeleteAsync(id);
    }

    public async Task<UserDto> GetAsync(int id)
    {
        var result = await userRepository.GetAsync(id);

        return await Task.FromResult(result.ToDto());
    }

    public async Task UpdateAsync(UpdateUser updateUser, int id)
    {
        await userRepository.UpdateAsync(updateUser.ToDomain(), id);
    }

    public async Task Login(LoginRequest loginRequest)
    {
        var useCookieScheme = loginRequest.UseCookies || loginRequest.UseSessionCookies;
        var isPersistent = loginRequest.UseCookies && loginRequest.UseSessionCookies != true;
        signInManager.AuthenticationScheme = useCookieScheme
            ? IdentityConstants.ApplicationScheme
            : IdentityConstants.BearerScheme;

        var user =
            await userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginRequest.UserName);

        if (user is null)
        {
            throw new LoginFailedException(loginRequest.UserName);
        }

        if (!user.IsConfirmed)
        {
            throw new UserNotConfirmedException(loginRequest.UserName);
        }

        var result =
            await signInManager.PasswordSignInAsync(loginRequest.UserName, loginRequest.Password,
                isPersistent,
                true);

        if (!result.Succeeded)
        {
            throw new LoginFailedException(loginRequest.UserName);
        }
    }

    public async Task ChangePassword(ChangePasswordRequest changePasswordRequest)
    {
        var user =
            await userManager.Users.FirstOrDefaultAsync(x =>
                x.UserName == changePasswordRequest.UserName);

        if (user is null)
        {
            throw new PasswordChangeFailedException(changePasswordRequest.UserName);
        }

        await userManager.ChangePasswordAsync(user, changePasswordRequest.OldPassword,
            changePasswordRequest.NewPassword);
    }

    public async Task ResetPassword(ResetPasswordRequest resetPasswordRequest)
    {
        await userRepository.ResetPassword(resetPasswordRequest);
    }

    public async Task<UserWithRolesDto> GetCurrentUser(ClaimsPrincipal claimsPrincipal)
    {
        var user = await userManager.GetUserAsync(claimsPrincipal);

        var result = user!.ToUserWithRoles();

        result.Roles = await userManager.GetRolesAsync(user!);
        
        return await Task.FromResult(result);
    }
}