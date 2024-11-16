using Microsoft.EntityFrameworkCore;

namespace TerrytLookup.Core.Domain;

[Index(nameof(Name))]
[Index(nameof(VoivodeshipId), nameof(CountyId), IsUnique = true)]
public class County : BaseEntity
{
    public int VoivodeshipId { get; set; }

    public int CountyId { get; set; }

    public required string Name { get; set; }

    public required Voivodeship Voivodeship { get; set; }

    public required ICollection<Town> Towns { get; set; }
}