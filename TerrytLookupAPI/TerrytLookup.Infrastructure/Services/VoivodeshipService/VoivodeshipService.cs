using AutoMapper;
using TerrytLookup.Core.Repositories;
using TerrytLookup.Infrastructure.Models.Dto;

namespace TerrytLookup.Infrastructure.Services.VoivodeshipService;

public class VoivodeshipService(IVoivodeshipRepository voivodeshipRepository, IMapper mapper) : IVoivodeshipService
{
    public IEnumerable<CreateVoivodeshipDto> BrowseAllAsync()
    {
        var voivodeships = voivodeshipRepository.BrowseAllAsync();

        return mapper.Map<IEnumerable<CreateVoivodeshipDto>>(voivodeships);
    }
}