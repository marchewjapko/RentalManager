using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Mvc;
using TerrytLookup.Infrastructure.ExceptionHandling.Exceptions;
using TerrytLookup.Infrastructure.Models.Dto.Terryt.Updates;
using TerrytLookup.Infrastructure.Services.CountyService;
using TerrytLookup.Infrastructure.Services.FeedDataService;
using TerrytLookup.Infrastructure.Services.StreetService;
using TerrytLookup.Infrastructure.Services.TownService;
using TerrytLookup.Infrastructure.Services.VoivodeshipService;

namespace TerrytLookup.WebAPI.Controllers;

/// <summary>
///     This controller is meant to manipulate Terryt database data.
/// </summary>
[Route("[Controller]/[Action]")]
public class DataFeedController(
    ILogger<DataFeedController> logger,
    ITownService townService,
    IStreetService streetService,
    IVoivodeshipService voivodeshipService,
    ICountyService countyService,
    IFeedDataService feedDataService) : ControllerBase
{
    /// <summary>
    ///     Initialize Terryt registries
    /// </summary>
    /// <remarks>
    ///     Parse, map and save Terryt data from a CSV files
    /// </remarks>
    /// <param name="tercCsvFile">File containing Terc data</param>
    /// <param name="simcCsvFile">File containing Simc data</param>
    /// <param name="ulicCsvFile">File containing Ulic data</param>
    [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(void))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    [ProducesErrorResponseType(typeof(ProblemDetails))]
    [Consumes("multipart/form-data")]
    [Produces("application/json")]
    [HttpPost]
    public async Task<IActionResult> InitializeData([Required] IFormFile tercCsvFile,
        [Required] IFormFile simcCsvFile,
        [Required] IFormFile ulicCsvFile)
    {
        IFormFile[] files = [tercCsvFile, simcCsvFile, ulicCsvFile];
        foreach (var file in files)
            if (file.ContentType != "text/csv")
            {
                throw new InvalidFileContentTypeExtensionException(file.ContentType);
            }

        if (await townService.ExistAnyAsync())
        {
            throw new DatabaseNotEmptyException();
        }
        
        if (await streetService.ExistAnyAsync())
        {
            throw new DatabaseNotEmptyException();
        }
        
        if (await voivodeshipService.ExistAnyAsync())
        {
            throw new DatabaseNotEmptyException();
        }   
        
        if (await countyService.ExistAnyAsync())
        {
            throw new DatabaseNotEmptyException();
        }

        await feedDataService.FeedTerrytDataAsync(tercCsvFile, simcCsvFile, ulicCsvFile);

        logger.Log(LogLevel.Information, "Terryt data initialized.");

        return NoContent();
    }

    /// <summary>
    /// 
    /// </summary>
    [HttpPut]
    public Task<IActionResult> UpdateSimc([Required] IFormFile tercXmlFile)
    {
        var deserializer = new XmlSerializer(typeof(TerrytUpdateDto<TercUpdateDto>));
        
        var reader = tercXmlFile.OpenReadStream();
        
        var movies = deserializer.Deserialize(reader) as TerrytUpdateDto<TercUpdateDto>;
        
        reader.Close();
        
        throw new NotImplementedException();
    }
}