using Microsoft.EntityFrameworkCore;
using TerrytLookup.Core.Domain;
using TerrytLookup.Core.Repositories;
using TerrytLookup.Infrastructure.Extensions;
using TerrytLookup.Infrastructure.Repositories.DbContext;

namespace TerrytLookup.Infrastructure.Repositories;

public class VoivodeshipRepository(AppDbContext appDbContext) : IVoivodeshipRepository
{
    public Task AddRangeAsync(IEnumerable<Voivodeship> voivodeships)
    {
        if (appDbContext.Database.IsRelational())
        {
            return appDbContext.InsertRelational(voivodeships);
        }

        return appDbContext.InsertNonRelational(voivodeships);
    }

    public IAsyncEnumerable<Voivodeship> BrowseAllAsync()
    {
        return appDbContext.Voivodeships.AsAsyncEnumerable();
    }

    public Task<Voivodeship> GetByIdAsync(Guid id)
    {
        return appDbContext.Voivodeships.FirstOrDefaultAsync(x => x.Id == id);
    }
}