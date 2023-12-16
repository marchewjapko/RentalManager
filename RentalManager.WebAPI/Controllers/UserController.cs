﻿using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RentalManager.Global.Queries;
using RentalManager.Global.Requests;
using RentalManager.Infrastructure.Commands.UserCommands;
using RentalManager.Infrastructure.DTO;
using RentalManager.Infrastructure.Services.Interfaces;

// ReSharper disable RouteTemplates.RouteParameterConstraintNotResolved

namespace RentalManager.WebAPI.Controllers;

[ApiController]
[Route("[Controller]")]
public class UserController
    (IUserService userService) : Controller
{
    [ProducesResponseType(typeof(UserDto), 200)]
    [ProducesResponseType(typeof(ProblemDetails), 500)]
    [HttpPost]
    public async Task<IActionResult> AddUser([FromForm] CreateUser createUser)
    {
        await userService.AddAsync(createUser);

        return Ok();
    }

    [Authorize]
    [ProducesResponseType(typeof(IEnumerable<UserDto>), 200)]
    [HttpGet]
    public async Task<IActionResult> BrowseAllUsers([FromQuery] QueryUser queryUser)
    {
        var result = await userService.BrowseAllAsync(queryUser);

        return Json(result);
    }

    [Authorize(Roles = "Administrator")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        await userService.DeleteAsync(id);

        return NoContent();
    }

    [Authorize]
    [ProducesResponseType(typeof(UserDto), 200)]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetUser(int id)
    {
        var clientDto = await userService.GetAsync(id);

        return Json(clientDto);
    }

    [Authorize]
    [ProducesResponseType(typeof(UserDto), 200)]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateUser([FromForm] UpdateUser updateUser,
        int id)
    {
        await userService.UpdateAsync(updateUser, id);

        return Ok();
    }

    [Authorize]
    [Route("/User/Image/{id}")]
    [HttpGet]
    public async Task<IActionResult?> GetUserImage(int id)
    {
        var userDto = await userService.GetAsync(id);

        if (userDto.Image is null)
        {
            return File("~/DefaultUserImageMan.png", "image/png");
        }

        return File(userDto.Image, "image/jpeg");
    }

    [Route("/User/login")]
    [HttpPost]
    public async Task<Results<Ok<AccessTokenResponse>, EmptyHttpResult, ProblemHttpResult>> Login(
        [FromQuery] LoginRequest login)
    {
        await userService.Login(login);

        return TypedResults.Empty;
    }

    [Authorize]
    [Route("/User/WhoAmI")]
    [HttpPost]
    public async Task<IActionResult> WhoAmI()
    {
        var result = await userService.GetCurrentUser(User);

        return Json(result);
    }

    [Route("/User/ChangePassword")]
    [HttpPost]
    public async Task<IActionResult> ChangePassword(
        [FromForm] ChangePasswordRequest changePasswordRequest)
    {
        await userService.ChangePassword(changePasswordRequest);

        return Ok();
    }

    [Authorize(Roles = "Administrator")]
    [Route("/User/ResetPassword")]
    [HttpPut]
    public async Task<IActionResult> ResetPassword(
        [FromForm] ResetPasswordRequest resetPasswordRequest)
    {
        await userService.ResetPassword(resetPasswordRequest);

        return Ok();
    }
}