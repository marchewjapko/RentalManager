using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using TerrytLookup.Core.Domain;
using TerrytLookup.Core.Repositories;
using TerrytLookup.Infrastructure.Repositories.DbContext;

namespace TerrytLookup.Infrastructure.Repositories;

public class VoivodeshipRepository(AppDbContext context) : IVoivodeshipRepository
{
    public Task AddRangeAsync(IEnumerable<Voivodeship> voivodeships)
    {
        return context.BulkInsertAsync(voivodeships, b => b.IncludeGraph = true);
    }

    public IAsyncEnumerable<Voivodeship> BrowseAllAsync()
    {
        return context.Voivodeships.AsAsyncEnumerable();
    }

    public Task<Voivodeship?> GetByIdAsync(Guid id)
    {
        return context.Voivodeships.FirstOrDefaultAsync(x => x.Id == id);
    }

    public Task<bool> ExistAnyAsync()
    {
        return context.Streets.AnyAsync();
    }
}