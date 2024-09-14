using System.Dynamic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentalManager.Infrastructure.Models.DTO;
using RentalManager.Infrastructure.Services.UserService;

namespace RentalManager.WebAPI.Controllers;

[ApiController]
[Authorize]
[Route("[Controller]")]
public class UserController(IUserService userService) : Controller
{
    [ProducesResponseType(typeof(IEnumerable<UserWithRolesDto>), 200)]
    [HttpGet]
    public async Task<IActionResult> BrowseAllUsers()
    {
        var result = await userService.BrowseAllAsync();

        return Json(result);
    }

    [ProducesResponseType(typeof(UserDto), 200)]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetUser(int id)
    {
        var clientDto = await userService.GetAsync(id);

        return Json(clientDto);
    }

    [ProducesResponseType(typeof(UserWithRolesDto), 200)]
    [Route("/[Controller]/WhoAmI")]
    [HttpGet]
    public IActionResult WhoAmI()
    {
        var me = new UserWithRolesDto
        {
            Id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "id")!
                .Value),
            FirstName = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "first_name")!.Value,
            LastName = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "last_name")!.Value,
            UserName = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "preferred_username")!
                .Value,
            Roles = HttpContext.User.Claims.Where(x => x.Type == "groups")
                .Select(x => x.Value)
                .ToList()
        };

        return Ok(me);
    }

    [Route("/[Controller]/GetMyClaims")]
    [HttpGet]
    public IActionResult GetMyClaims()
    {
        return Json(HttpContext.User.Claims.Select(x => {
            dynamic result = new ExpandoObject();
            result.Type = x.Type;
            result.Value = x.Value;

            return result;
        }));
    }
}