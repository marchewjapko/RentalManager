﻿using TerrytLookup.Core.Domain;

namespace TerrytLookup.Core.Repositories;

public interface IVoivodeshipRepository
{
    Task AddRangeAsync(IEnumerable<Voivodeship> voivodeships);

    IAsyncEnumerable<Voivodeship> BrowseAllAsync();

    Task<Voivodeship?> GetByIdAsync(Guid id);

    Task<bool> ExistAnyAsync();
}