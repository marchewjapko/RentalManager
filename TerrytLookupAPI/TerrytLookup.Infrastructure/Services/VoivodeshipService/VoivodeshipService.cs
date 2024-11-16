using AutoMapper;
using TerrytLookup.Core.Domain;
using TerrytLookup.Core.Repositories;
using TerrytLookup.Infrastructure.ExceptionHandling.Exceptions;
using TerrytLookup.Infrastructure.Models.Dto;
using TerrytLookup.Infrastructure.Models.Dto.Internal.CreateDtos;

namespace TerrytLookup.Infrastructure.Services.VoivodeshipService;

public class VoivodeshipService(IVoivodeshipRepository voivodeshipRepository, IMapper mapper) : IVoivodeshipService
{
    public Task AddRange(IEnumerable<CreateVoivodeshipDto> voivodeships)
    {
        var entities = mapper.Map<IEnumerable<Voivodeship>>(voivodeships)
            .ToList();

        return voivodeshipRepository.AddRangeAsync(entities); 
    }

    public IEnumerable<VoivodeshipDto> BrowseAllAsync()
    {
        var voivodeships = voivodeshipRepository.BrowseAllAsync();

        return mapper.Map<IEnumerable<VoivodeshipDto>>(voivodeships);
    }

    public async Task<VoivodeshipDto> GetByIdAsync(int id)
    {
        var voivodeship = await voivodeshipRepository.GetByIdAsync(id);

        if (voivodeship is null)
        {
            throw new VoivodeshipNotFoundException(id);
        }

        return mapper.Map<VoivodeshipDto>(voivodeship);
    }

    public Task<bool> ExistAnyAsync()
    {
        return voivodeshipRepository.ExistAnyAsync();
    }
}