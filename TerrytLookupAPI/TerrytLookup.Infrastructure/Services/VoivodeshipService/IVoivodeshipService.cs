using TerrytLookup.Infrastructure.Models.Dto;

namespace TerrytLookup.Infrastructure.Services.VoivodeshipService;

public interface IVoivodeshipService
{
    public IEnumerable<CreateVoivodeshipDto> BrowseAllAsync();
}