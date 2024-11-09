using Microsoft.AspNetCore.Mvc;
using TerrytLookup.Infrastructure.Models.Dto;
using TerrytLookup.Infrastructure.Services.StreetService;

namespace TerrytLookup.WebAPI.Controllers;

/// <summary>
///     Controller for managing street-related operations.
/// </summary>
[Route("[Controller]")]
public class StreetController(IStreetService streetService) : ControllerBase
{
    /// <summary>
    ///     Retrieves a list of all streets, optionally filtered by name and town ID.
    /// </summary>
    /// <param name="name">The optional name of the street to filter by.</param>
    /// <param name="townId">The optional ID of the town to filter by.</param>
    /// <returns>A list of <see cref="StreetDto" /> representing the streets.</returns>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<StreetDto>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    [HttpGet]
    public IActionResult BrowseAllStreets(string? name, Guid? townId)
    {
        var result = streetService.BrowseAllAsync(name, townId);

        return Ok(result);
    }

    /// <summary>
    ///     Retrieves a specific street by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the street.</param>
    /// <returns>The <see cref="StreetDto" /> representing the street.</returns>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StreetDto))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetTownById(Guid id)
    {
        var result = await streetService.GetByIdAsync(id);

        return Ok(result);
    }
}