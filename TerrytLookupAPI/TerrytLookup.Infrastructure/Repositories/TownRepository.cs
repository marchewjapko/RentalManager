using Microsoft.EntityFrameworkCore;
using TerrytLookup.Core.Domain;
using TerrytLookup.Core.Repositories;
using TerrytLookup.Infrastructure.Extensions;
using TerrytLookup.Infrastructure.Repositories.DbContext;

namespace TerrytLookup.Infrastructure.Repositories;

public class TownRepository(AppDbContext appDbContext) : ITownRepository
{
    public Task AddRangeAsync(IEnumerable<Town> towns)
    {
        if (appDbContext.Database.IsRelational())
        {
            return appDbContext.InsertRelational(towns);
        }

        return appDbContext.InsertNonRelational(towns);
    }

    public IAsyncEnumerable<Town> BrowseAllAsync(string name)
    {
        return appDbContext.Towns.AsAsyncEnumerable();
    }
}