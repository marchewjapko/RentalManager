namespace TerrytLookup.Infrastructure.Models.Dto;

public class StreetDto
{
    public int TownId { get; set; }
    
    public int NameId { get; set; }

    public required string Name { get; set; }
}