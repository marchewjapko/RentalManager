using AutoMapper;
using TerrytLookup.Core.Domain;
using TerrytLookup.Core.Repositories;
using TerrytLookup.Infrastructure.ExceptionHandling.Exceptions;
using TerrytLookup.Infrastructure.Models.Dto;
using TerrytLookup.Infrastructure.Models.Dto.Internal.CreateDtos;

namespace TerrytLookup.Infrastructure.Services.StreetService;

public class StreetService(IStreetRepository streetRepository, IMapper mapper) : IStreetService
{
    public Task AddRange(IEnumerable<CreateStreetDto> streets)
    {
        var entities = mapper.Map<IEnumerable<Street>>(streets)
            .ToList();

        return streetRepository.AddRangeAsync(entities);
    }

    public async Task<StreetDto> GetByIdAsync(int townId, int nameId)
    {
        var street = await streetRepository.GetByIdAsync(townId, nameId);

        if (street is null)
        {
            throw new StreetNotFoundException(townId, nameId);
        }

        return mapper.Map<StreetDto>(street);
    }

    public Task<bool> ExistAnyAsync()
    {
        return streetRepository.ExistAnyAsync();
    }

    public IEnumerable<StreetDto> BrowseAllAsync(string? name, int? townId)
    {
        var streets = streetRepository.BrowseAllAsync(name, townId);

        return mapper.Map<IEnumerable<StreetDto>>(streets);
    }
}