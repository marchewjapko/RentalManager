using System.Security.Claims;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RentalManager.Core.Domain;
using RentalManager.Core.Repositories;
using RentalManager.Global.Queries;
using RentalManager.Global.Requests;
using RentalManager.Infrastructure.Commands.UserCommands;
using RentalManager.Infrastructure.DTO;
using RentalManager.Infrastructure.DTO.ObjectConversions;
using RentalManager.Infrastructure.Exceptions;
using RentalManager.Infrastructure.Services.Interfaces;

namespace RentalManager.Infrastructure.Services.Implementation;

public class UserService(IUserRepository userRepository,
    UserManager<User> userManager,
    SignInManager<User> signInManager
) : IUserService
{
    public async Task<UserDto> AddAsync(CreateUser createUser)
    {
        var result = await userManager.CreateAsync(createUser.ToDomain(), createUser.Password);

        if (!result.Succeeded)
        {
            var validationFailure = result.Errors
                .Select(error => new ValidationFailure(error.Code, error.Description))
                .ToList();

            throw new ValidationException(validationFailure);
        }

        var newUser =
            await userManager.Users.FirstOrDefaultAsync(x => x.UserName == createUser.UserName);

        return newUser!.ToDto();
    }

    public async Task<IEnumerable<UserDto>> BrowseAllAsync(QueryUser queryUser)
    {
        var result = await userRepository.BrowseAllAsync(queryUser);

        return await Task.FromResult(result.Select(x => x.ToDto()));
    }

    public async Task<byte[]?> GetUserImage(int userId)
    {
        var result = await userRepository.GetAsync(userId);

        return result.Image;
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

    public async Task<UserDto> UpdateAsync(UpdateUser updateUser, int id)
    {
        var result = await userRepository.UpdateAsync(updateUser.ToDomain(), id);

        return result.ToDto();
    }

    public async Task SignIn(SignInRequest signInRequest)
    {
        signInManager.AuthenticationScheme = IdentityConstants.ApplicationScheme;

        var user =
            await userManager.Users.FirstOrDefaultAsync(x => x.UserName == signInRequest.UserName);

        if (user is null)
        {
            throw new SignInFailedException(signInRequest.UserName);
        }

        if (!user.IsConfirmed)
        {
            throw new UserNotConfirmedException(signInRequest.UserName);
        }

        if (user.PasswordValidTo is not null)
        {
            throw new PasswordChangeRequiredException(signInRequest.UserName);
        }

        var result =
            await signInManager.PasswordSignInAsync(signInRequest.UserName, signInRequest.Password,
                signInRequest.IsPersistent,
                true);

        if (!result.Succeeded)
        {
            throw new SignInFailedException(signInRequest.UserName);
        }
    }

    public async Task SignOut()
    {
        await signInManager.SignOutAsync();
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

        if (user.PasswordValidTo is not null && user.PasswordValidTo < DateTime.Now)
        {
            throw new TemporaryPasswordExpiredException(changePasswordRequest.UserName);
        }

        await userManager.ChangePasswordAsync(user, changePasswordRequest.OldPassword,
            changePasswordRequest.NewPassword);
        await userRepository.ClearPasswordExpiration(user);
        await userManager.UpdateSecurityStampAsync(user);
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