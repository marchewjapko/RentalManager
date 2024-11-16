using System.Collections.Concurrent;

namespace TerrytLookup.Infrastructure.Models.Dto.Internal.CreateDtos;

public class CreateCountyDto
{
    public required (int voivodeshipId, int countyId) TerrytId { get; init; }

    public required string Name { get; init; }
    
    public ConcurrentBag<CreateTownDto> Towns { get; init; } = [];

    public required DateOnly ValidFromDate { get; set; }
    
    public CreateVoivodeshipDto? Voivodeship { get; set; }
}