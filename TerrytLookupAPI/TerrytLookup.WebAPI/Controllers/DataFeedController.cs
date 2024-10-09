using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using TerrytLookup.Infrastructure.ExceptionHandling.Exceptions;
using TerrytLookup.Infrastructure.Services.FeedDataService;
using TerrytLookup.Infrastructure.Services.TownService;

namespace TerrytLookup.WebAPI.Controllers;

/// <summary>
///     Simc data controller
/// </summary>
[Route("[Controller]")]
public class DataFeedController(ILogger<TercController> logger, ITownService service, IFeedDataService testService) : ControllerBase
{
    /// <summary>
    ///     Feed Terryt data, nom nom nom
    /// </summary>
    /// <remarks>
    ///     Parse, map and save Terryt data from a CSV files
    /// </remarks>
    /// <param name="tercCsvFile">File containing TERC data</param>
    /// <param name="simcCsvFile">File containing SIMC data</param>
    /// <param name="ulicCsvFile">File containing ULIC data</param>
    [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(void))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    [ProducesErrorResponseType(typeof(ProblemDetails))]
    [Consumes("multipart/form-data")]
    [Produces("application/json")]
    [HttpPost]
    public async Task<IActionResult> FeedTercData([Required] IFormFile tercCsvFile,
        [Required] IFormFile simcCsvFile,
        [Required] IFormFile ulicCsvFile)
    {
        if (tercCsvFile.ContentType != "text/csv")
        {
            throw new InvalidFileContentTypeExtension(tercCsvFile.ContentType);
        }

        if (simcCsvFile.ContentType != "text/csv")
        {
            throw new InvalidFileContentTypeExtension(simcCsvFile.ContentType);
        }

        if (ulicCsvFile.ContentType != "text/csv")
        {
            throw new InvalidFileContentTypeExtension(tercCsvFile.ContentType);
        }

        await testService.FeedTerrytDataAsync(tercCsvFile, simcCsvFile, ulicCsvFile);

        return NoContent();
    }
}