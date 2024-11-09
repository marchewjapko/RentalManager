﻿using AutoMapper;
using TerrytLookup.Core.Repositories;
using TerrytLookup.Infrastructure.ExceptionHandling.Exceptions;
using TerrytLookup.Infrastructure.Models.Dto;

namespace TerrytLookup.Infrastructure.Services.TownService;

public class TownService(ITownRepository townRepository, IMapper mapper) : ITownService
{
    public IEnumerable<TownDto> BrowseAllAsync(string? name, Guid? voivodeshipId)
    {
        var towns = townRepository.BrowseAllAsync(name, voivodeshipId);

        return mapper.Map<IEnumerable<TownDto>>(towns);
    }

    public async Task<TownDto> GetByIdAsync(Guid id)
    {
        var town = await townRepository.GetByIdAsync(id);

        if (town is null)
        {
            throw new TownNotFoundException(id);
        }

        return mapper.Map<TownDto>(town);
    }

    public Task<bool> ExistAnyAsync()
    {
        return townRepository.ExistAnyAsync();
    }
}