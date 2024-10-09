using Microsoft.AspNetCore.Mvc;
using TerrytLookup.Infrastructure.Models.Dto;
using TerrytLookup.Infrastructure.Services.VoivodeshipService;

namespace TerrytLookup.WebAPI.Controllers;

/// <summary>
///     Terc data controller
/// </summary>
[Route("[Controller]")]
public class TercController(ILogger<TercController> logger, IVoivodeshipService service) : ControllerBase
{
    /// <summary>
    ///     Get all voivodeships
    /// </summary>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CreateVoivodeshipDto>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    [HttpGet]
    public IActionResult FeedTercData()
    {
        var result = service.BrowseAllAsync();

        return Ok(result);
    }
}