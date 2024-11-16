using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using TerrytLookup.Core.Domain;
using TerrytLookup.Core.Repositories;
using TerrytLookup.Infrastructure.Repositories.DbContext;

namespace TerrytLookup.Infrastructure.Repositories;

public class StreetRepository(AppDbContext context) : IStreetRepository
{
    public Task AddRangeAsync(IList<Street> towns)
    {
        return context.BulkInsertAsync(towns);
    }

    public IAsyncEnumerable<Street> BrowseAllAsync(string? name, int? townId)
    {
        var query = context.Streets.AsNoTracking()
            .AsQueryable();

        if (name is not null)
        {
            query = query.Where(x => x.Name.Contains(name));
        }

        if (townId.HasValue)
        {
            query = query.Where(x => x.Town.Id == townId || (x.Town.ParentTown != null && x.Town.ParentTown.Id == townId));
        }
        
        query = query.GroupBy(x => x.Name)
            .OrderBy(x => x.Key)
            .Select(x => x.First());

        return query
            .Take(AppDbContext.PageSize)
            .AsAsyncEnumerable();
    }

    public Task<Street?> GetByIdAsync(int townId, int nameId)
    {
        return context.Streets.FirstOrDefaultAsync(x => x.TownId == townId && x.NameId == nameId);
    }

    public Task<bool> ExistAnyAsync()
    {
        return context.Streets.AnyAsync();
    }
}