using Microsoft.AspNetCore.Mvc;
using TerrytLookup.Infrastructure.Models.Dto;
using TerrytLookup.Infrastructure.Services.TownService;

namespace TerrytLookup.WebAPI.Controllers;

/// <summary>
///     Controller for managing town-related operations.
/// </summary>
[Route("[Controller]")]
public class TownController(ITownService townService) : ControllerBase
{
    /// <summary>
    ///     Retrieves a list of all towns, optionally filtered by name and town ID.
    /// </summary>
    /// <param name="name">The optional name of the town to filter by.</param>
    /// <param name="voivodeshipId">The optional ID of the voivodeship to filter by.</param>
    /// <returns>A list of <see cref="TownDto" /> representing the towns.</returns>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TownDto>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    [HttpGet]
    public IActionResult BrowseAllVoivodeships(string? name, Guid? voivodeshipId)
    {
        var result = townService.BrowseAllAsync(name, voivodeshipId);

        return Ok(result);
    }

    /// <summary>
    ///     Retrieves a specific town by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the town.</param>
    /// <returns>The <see cref="TownDto" /> representing the town.</returns>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TownDto))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetTownById(Guid id)
    {
        var result = await townService.GetByIdAsync(id);

        return Ok(result);
    }
}