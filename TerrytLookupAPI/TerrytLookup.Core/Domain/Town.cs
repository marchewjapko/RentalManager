using Microsoft.EntityFrameworkCore;

namespace TerrytLookup.Core.Domain;

[Index(nameof(Name))]
public class Town
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public int TerrytId { get; set; }

    public required string Name { get; set; }

    public Guid VoivodeshipId { get; set; }

    public required Voivodeship Voivodeship { get; set; }

    public DateOnly ValidFromDate { get; set; }

    public DateTimeOffset Timestamp { get; set; } = DateTimeOffset.UtcNow;
}