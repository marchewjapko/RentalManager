using Microsoft.EntityFrameworkCore;

namespace TerrytLookup.Core.Domain;

[Index(nameof(Name))]
public class Street : BaseEntity
{
    public int NameId { get; set; }
    
    public required string Name { get; set; }
    
    public int TownId { get; set; }
    
    public required Town Town { get; set; }
}