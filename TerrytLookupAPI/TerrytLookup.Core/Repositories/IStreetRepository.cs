using TerrytLookup.Core.Domain;

namespace TerrytLookup.Core.Repositories;

public interface IStreetRepository
{
    Task AddRangeAsync(IList<Street> towns);

    IAsyncEnumerable<Street> BrowseAllAsync(string? name, Guid? townId);

    Task<Street?> GetByIdAsync(Guid id);

    Task<bool> ExistAnyAsync();
}