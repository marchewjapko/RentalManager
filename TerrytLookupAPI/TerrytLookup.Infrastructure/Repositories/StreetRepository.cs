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

    public IAsyncEnumerable<Street> BrowseAllAsync(string? name, Guid? townId)
    {
        var query = context.Streets.AsNoTracking()
            .AsQueryable();

        if (name is not null)
        {
            query = query.Where(x => x.Name.Contains(name));
        }

        if (townId.HasValue)
        {
            query = query.Where(x => x.TownId == townId);
        }

        return query.Take(AppDbContext.PageSize)
            .AsAsyncEnumerable();
    }

    public Task<Street?> GetByIdAsync(Guid id)
    {
        return context.Streets.FirstOrDefaultAsync(x => x.Id == id);
    }

    public Task<bool> ExistAnyAsync()
    {
        return context.Streets.AnyAsync();
    }
}