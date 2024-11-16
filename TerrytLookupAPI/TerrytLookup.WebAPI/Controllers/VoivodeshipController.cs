using Microsoft.AspNetCore.Mvc;
using TerrytLookup.Infrastructure.Models.Dto;
using TerrytLookup.Infrastructure.Services.VoivodeshipService;

namespace TerrytLookup.WebAPI.Controllers;

/// <summary>
///     Controller for managing voivodeship-related operations.
/// </summary>
[Route("[Controller]")]
public class VoivodeshipController(IVoivodeshipService voivodeshipService) : ControllerBase
{
    /// <summary>
    ///     Retrieves all voivodeships.
    /// </summary>
    /// <returns>A list of <see cref="VoivodeshipDto" /> objects.</returns>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<VoivodeshipDto>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    [HttpGet]
    public IActionResult BrowseAllVoivodeships()
    {
        var result = voivodeshipService.BrowseAllAsync();

        return Ok(result);
    }

    /// <summary>
    ///     Retrieves a specific voivodeship by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the voivodeship.</param>
    /// <returns>The <see cref="VoivodeshipDto" /> representing the voivodeship.</returns>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VoivodeshipDto))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetVoivodeshipById(int id)
    {
        var result = await voivodeshipService.GetByIdAsync(id);

        return Ok(result);
    }
}