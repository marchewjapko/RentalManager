﻿using System.Security.Claims;
using RentalManager.Global.Queries;
using RentalManager.Global.Requests;
using RentalManager.Infrastructure.Commands.UserCommands;
using RentalManager.Infrastructure.DTO;

namespace RentalManager.Infrastructure.Services.Interfaces;

public interface IUserService
{
    Task AddAsync(CreateUser createUser);
    Task<UserDto> GetAsync(int id);
    Task DeleteAsync(int id);
    Task UpdateAsync(UpdateUser updateUser, int id);

    Task<IEnumerable<UserDto>> BrowseAllAsync(QueryUser queryUser);

    Task Login(LoginRequest loginRequest);

    Task<UserWithRolesDto> GetCurrentUser(ClaimsPrincipal claimsPrincipal);

    Task ChangePassword(ChangePasswordRequest changePasswordRequest);

    Task ResetPassword(ResetPasswordRequest resetPasswordRequest);
}