using Microsoft.AspNetCore.Http;
using TerrytLookup.Infrastructure.Models.Dto.Terryt.Updates;

namespace TerrytLookup.Infrastructure.Services.FeedDataService;

public interface IFeedDataService
{
    Task FeedTerrytDataAsync(IFormFile tercCsvFile, IFormFile simcCsvFile, IFormFile ulicCsvFile);
    
    Task UpdateSimc(TerrytUpdateDto<SimcUpdateDto> updateDto);
}