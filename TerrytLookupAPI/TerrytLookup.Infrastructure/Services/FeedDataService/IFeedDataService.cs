using Microsoft.AspNetCore.Http;
using TerrytLookup.Infrastructure.Models.Dto;

namespace TerrytLookup.Infrastructure.Services.FeedDataService;

public interface IFeedDataService
{
    Task FeedTerrytDataAsync(IFormFile tercCsvFile, IFormFile simcCsvFile, IFormFile ulicCsvFile);
}