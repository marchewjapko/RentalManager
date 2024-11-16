using TerrytLookup.Core.Domain;

namespace TerrytLookup.Core.Repositories;

public interface ICountyRepository
{
    Task AddRangeAsync(IList<County> counties);

    IAsyncEnumerable<County> BrowseAllAsync(string? name, int? voivodeshipId);

    Task<County?> GetByIdAsync(int voivodeshipId, int countyId);

    Task<bool> ExistAnyAsync();
}