using Microsoft.EntityFrameworkCore;

namespace TerrytLookup.Core.Domain;

[Index(nameof(Name), IsUnique = true)]
public class Voivodeship
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public int TerrytId { get; set; }

    public required string Name { get; set; }

    public DateOnly ValidFromDate { get; set; }

    public DateTimeOffset Timestamp { get; set; } = DateTimeOffset.UtcNow;

    public required ICollection<Town> Towns { get; set; }
}