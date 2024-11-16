using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using TerrytLookup.Core.Domain;
using TerrytLookup.Core.Repositories;
using TerrytLookup.Infrastructure.Repositories.DbContext;

namespace TerrytLookup.Infrastructure.Repositories;

public class CountyRepository(AppDbContext context) : ICountyRepository
{
    public Task AddRangeAsync(IList<County> counties)
    {
        return context.BulkInsertAsync(counties);
    }

    public IAsyncEnumerable<County> BrowseAllAsync(string? name, int? voivodeshipId)
    {
        var query = context.Counties.AsNoTracking()
            .AsQueryable();

        if (name is not null)
        {
            query = query.Where(c => c.Name.Contains(name));
        }

        if (voivodeshipId is not null)
        {
            query = query.Where(x => x.Voivodeship.Id == voivodeshipId);
        }
        
        return query
            .Take(AppDbContext.PageSize)
            .AsAsyncEnumerable();
    }

    public Task<County?> GetByIdAsync(int voivodeshipId, int countyId)
    {
        return context.Counties.FirstOrDefaultAsync(x => x.VoivodeshipId == voivodeshipId && x.CountyId == countyId);
    }

    public Task<bool> ExistAnyAsync()
    {
        return context.Counties.AnyAsync();
    }
}