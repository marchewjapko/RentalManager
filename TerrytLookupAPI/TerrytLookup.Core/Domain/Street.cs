using Microsoft.EntityFrameworkCore;

namespace TerrytLookup.Core.Domain;

[Index(nameof(Name))]
public class Street
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public int TerrytId { get; set; }

    public required string Name { get; set; }

    public Guid TownId { get; set; }

    public required Town Town { get; set; }

    public DateOnly ValidFromDate { get; set; }

    public DateTimeOffset Timestamp { get; set; } = DateTimeOffset.UtcNow;
}