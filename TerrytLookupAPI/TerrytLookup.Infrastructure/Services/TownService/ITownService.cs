using TerrytLookup.Infrastructure.Models.Dto;

namespace TerrytLookup.Infrastructure.Services.TownService;

public interface ITownService
{
    // public Task AddAsync(IEnumerable<SimcDto> simcDtos);
    //
    // public ValueTask<List<SimcDto>> ParseSimcData(Stream stream);

    public IEnumerable<CreateTownDto> BrowseAllAsync(string name);
}