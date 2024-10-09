using TerrytLookup.Core.Domain;

namespace TerrytLookup.Core.Repositories;

public interface ITownRepository
{
    Task AddRangeAsync(IEnumerable<Town> towns);

    IAsyncEnumerable<Town> BrowseAllAsync(string name);
}