namespace TerrytLookup.Infrastructure.Models.Dto;

public class CountyDto
{
    public int VoivodeshipId { get; init; }
    
    public int CountyId { get; init; }

    public required string Name { get; init; }
}