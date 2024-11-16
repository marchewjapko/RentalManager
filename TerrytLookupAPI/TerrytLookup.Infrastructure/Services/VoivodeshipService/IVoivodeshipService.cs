using TerrytLookup.Infrastructure.Models.Dto;
using TerrytLookup.Infrastructure.Models.Dto.Internal.CreateDtos;

namespace TerrytLookup.Infrastructure.Services.VoivodeshipService;

public interface IVoivodeshipService
{
    public Task AddRange(IEnumerable<CreateVoivodeshipDto> voivodeships);

    public IEnumerable<VoivodeshipDto> BrowseAllAsync();

    public Task<VoivodeshipDto> GetByIdAsync(int id);

    Task<bool> ExistAnyAsync();
}