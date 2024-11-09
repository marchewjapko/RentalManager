using TerrytLookup.Core.Domain;

namespace TerrytLookup.Core.Repositories;

public interface ITownRepository
{
    Task AddRangeAsync(IList<Town> towns);

    IAsyncEnumerable<Town> BrowseAllAsync(string? name, Guid? voivodeshipId);

    Task<Town?> GetByIdAsync(Guid id);

    Task<bool> ExistAnyAsync();
}