using TerrytLookup.Infrastructure.Models.Dto;

namespace TerrytLookup.Infrastructure.Services.CountyService;

public interface ICountyService
{
    public IEnumerable<CountyDto> BrowseAllAsync(string? name, int? voivodeshipId);

    public Task<CountyDto> GetByIdAsync(int voivodeshipId, int countyId);

    Task<bool> ExistAnyAsync();
}