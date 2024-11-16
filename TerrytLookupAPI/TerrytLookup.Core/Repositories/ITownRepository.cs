using TerrytLookup.Core.Domain;

namespace TerrytLookup.Core.Repositories;

public interface ITownRepository
{
    Task AddRangeAsync(IList<Town> towns);

    IAsyncEnumerable<Town> BrowseAllAsync(string? name, int? voivodeshipId, int? countyId);

    Task<Town?> GetByIdAsync(int id);

    Task<bool> ExistAnyAsync();
}