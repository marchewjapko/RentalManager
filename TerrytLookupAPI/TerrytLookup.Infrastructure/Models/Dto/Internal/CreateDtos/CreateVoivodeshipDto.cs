using System.Collections.Concurrent;

namespace TerrytLookup.Infrastructure.Models.Dto.Internal.CreateDtos;

public class CreateVoivodeshipDto
{
    public required int TerrytId { get; init; }

    public required string Name { get; init; }

    public ConcurrentBag<CreateCountyDto> Counties { get; init; } = [];

    public required DateOnly ValidFromDate { get; set; }
}