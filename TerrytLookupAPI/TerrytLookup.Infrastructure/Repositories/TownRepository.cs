﻿using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using TerrytLookup.Core.Domain;
using TerrytLookup.Core.Repositories;
using TerrytLookup.Infrastructure.Repositories.DbContext;

namespace TerrytLookup.Infrastructure.Repositories;

public class TownRepository(AppDbContext context) : ITownRepository
{
    public Task AddRangeAsync(IList<Town> towns)
    {
        return context.BulkInsertAsync(towns);
    }

    public IAsyncEnumerable<Town> BrowseAllAsync(string? name, Guid? voivodeshipId)
    {
        var query = context.Towns.AsNoTracking()
            .AsQueryable();

        if (name is not null)
        {
            query = query.Where(town => town.Name.Contains(name));
        }

        if (voivodeshipId.HasValue)
        {
            query = query.Where(x => x.VoivodeshipId == voivodeshipId);
        }

        return query.Take(AppDbContext.PageSize)
            .AsAsyncEnumerable();
    }

    public Task<Town?> GetByIdAsync(Guid id)
    {
        return context.Towns.FirstOrDefaultAsync(x => x.Id == id);
    }

    public Task<bool> ExistAnyAsync()
    {
        return context.Streets.AnyAsync();
    }
}