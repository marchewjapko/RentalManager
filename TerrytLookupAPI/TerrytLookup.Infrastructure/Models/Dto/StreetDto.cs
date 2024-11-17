namespace TerrytLookup.Infrastructure.Models.Dto;

public class StreetDto
{
    public int TownId { get; init; }
    
    public int NameId { get; init; }

    public required string Name { get; init; }
}