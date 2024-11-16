using TerrytLookup.Core.Domain;

namespace TerrytLookup.Core.Repositories;

public interface IStreetRepository
{
    Task AddRangeAsync(IList<Street> towns);

    IAsyncEnumerable<Street> BrowseAllAsync(string? name, int? townId);

    Task<Street?> GetByIdAsync(int townId, int nameId);

    Task<bool> ExistAnyAsync();
}