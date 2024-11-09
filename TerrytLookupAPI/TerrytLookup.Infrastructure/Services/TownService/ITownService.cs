using TerrytLookup.Infrastructure.Models.Dto;

namespace TerrytLookup.Infrastructure.Services.TownService;

public interface ITownService
{
    public IEnumerable<TownDto> BrowseAllAsync(string? name, Guid? voivodeshipId);

    public Task<TownDto> GetByIdAsync(Guid id);

    Task<bool> ExistAnyAsync();
}