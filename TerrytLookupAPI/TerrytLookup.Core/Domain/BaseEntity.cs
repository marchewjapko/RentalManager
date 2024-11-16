namespace TerrytLookup.Core.Domain;

public class BaseEntity
{
    public DateOnly ValidFromDate { get; set; }
    
    public DateTimeOffset CreateTimestamp { get; set; } = DateTimeOffset.UtcNow;
    
    public DateTimeOffset? UpdateTimestamp { get; set; }
}