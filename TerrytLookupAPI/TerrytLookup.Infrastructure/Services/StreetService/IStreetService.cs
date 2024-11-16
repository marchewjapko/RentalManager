using TerrytLookup.Infrastructure.Models.Dto;
using TerrytLookup.Infrastructure.Models.Dto.Internal.CreateDtos;

namespace TerrytLookup.Infrastructure.Services.StreetService;

public interface IStreetService
{
    public Task AddRange(IEnumerable<CreateStreetDto> streets);

    public Task<StreetDto> GetByIdAsync(int townId, int nameId);

    Task<bool> ExistAnyAsync();

    public IEnumerable<StreetDto> BrowseAllAsync(string? name, int? townId);
}