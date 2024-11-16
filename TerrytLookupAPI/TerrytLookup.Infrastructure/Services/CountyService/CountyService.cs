using AutoMapper;
using TerrytLookup.Core.Repositories;
using TerrytLookup.Infrastructure.ExceptionHandling.Exceptions;
using TerrytLookup.Infrastructure.Models.Dto;

namespace TerrytLookup.Infrastructure.Services.CountyService;

public class CountyService(ICountyRepository countyRepository, IMapper mapper) : ICountyService
{
    public IEnumerable<CountyDto> BrowseAllAsync(string? name, int? voivodeshipId)
    {
        var towns = countyRepository.BrowseAllAsync(name, voivodeshipId);

        return mapper.Map<IEnumerable<CountyDto>>(towns);
    }

    public async Task<CountyDto> GetByIdAsync(int voivodeshipId, int countyId)
    {
        var town = await countyRepository.GetByIdAsync(voivodeshipId, countyId);

        if (town is null)
        {
            throw new CountyNotFoundException(voivodeshipId, countyId);
        }

        return mapper.Map<CountyDto>(town);
    }

    public Task<bool> ExistAnyAsync()
    {
        return countyRepository.ExistAnyAsync();
    }
}