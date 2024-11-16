using TerrytLookup.Infrastructure.Models.Dto;

namespace TerrytLookup.Infrastructure.Services.TownService;

public interface ITownService
{
    public IEnumerable<TownDto> BrowseAllAsync(string? name, int? voivodeshipId, int? countyId);

    public Task<TownDto> GetByIdAsync(int id);

    Task<bool> ExistAnyAsync();
}