namespace TerrytLookup.Infrastructure.Models.Dto.Internal.CreateDtos;

public class CreateStreetDto
{
    public required int TerrytTownId { get; init; }

    public required int TerrytNameId { get; init; }

    public required string Name { get; init; }

    public CreateTownDto? Town { get; set; }

    public required DateOnly ValidFromDate { get; set; }
}