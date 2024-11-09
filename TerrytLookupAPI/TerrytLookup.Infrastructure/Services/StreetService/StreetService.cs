using AutoMapper;
using TerrytLookup.Core.Domain;
using TerrytLookup.Core.Repositories;
using TerrytLookup.Infrastructure.ExceptionHandling.Exceptions;
using TerrytLookup.Infrastructure.Models.Dto;
using TerrytLookup.Infrastructure.Models.Dto.CreateDtos;

namespace TerrytLookup.Infrastructure.Services.StreetService;

public class StreetService(IStreetRepository streetRepository, IMapper mapper) : IStreetService
{
    public Task AddRange(IEnumerable<CreateStreetDto> streets)
    {
        var entities = mapper.Map<IEnumerable<Street>>(streets)
            .ToList();

        return streetRepository.AddRangeAsync(entities);
    }

    public async Task<StreetDto> GetByIdAsync(Guid id)
    {
        var street = await streetRepository.GetByIdAsync(id);

        if (street is null)
        {
            throw new StreetNotFoundException(id);
        }

        return mapper.Map<StreetDto>(street);
    }

    public Task<bool> ExistAnyAsync()
    {
        return streetRepository.ExistAnyAsync();
    }

    public IEnumerable<StreetDto> BrowseAllAsync(string? name, Guid? townId)
    {
        var streets = streetRepository.BrowseAllAsync(name, townId);

        return mapper.Map<IEnumerable<StreetDto>>(streets);
    }
}