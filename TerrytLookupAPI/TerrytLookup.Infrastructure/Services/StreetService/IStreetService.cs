using TerrytLookup.Infrastructure.Models.Dto;
using TerrytLookup.Infrastructure.Models.Dto.CreateDtos;

namespace TerrytLookup.Infrastructure.Services.StreetService;

public interface IStreetService
{
    public Task AddRange(IEnumerable<CreateStreetDto> streets);

    public Task<StreetDto> GetByIdAsync(Guid id);

    Task<bool> ExistAnyAsync();

    public IEnumerable<StreetDto> BrowseAllAsync(string? name, Guid? townId);
}