using CsvHelper.Configuration.Attributes;

namespace TerrytLookup.Infrastructure.Models.Dto.Terryt;

public class SimcDto : IEquatable<SimcDto>
{
    /// <summary>
    ///     Terryt property: <c>WOJ</c>
    /// </summary>
    [Name("WOJ")]
    public required int VoivodeshipId { get; init; }    
    
    /// <summary>
    ///     Terryt property: <c>POW</c>
    /// </summary>
    [Name("POW")]
    public required int CountyId { get; init; }
    
    /// <summary>
    ///     Terryt property: <c>NAZWA</c>
    /// </summary>
    [Name("NAZWA")]
    public required string Name { get; init; }

    /// <summary>
    ///     Terryt property: <c>SYM</c>
    /// </summary>
    [Name("SYM")]
    public required int Id { get; init; }

    /// <summary>
    ///     Terryt property: <c>SYMSTAT</c>
    /// </summary>
    [Name("SYMSTAT")]
    public required int ParentId { get; init; }

    /// <summary>
    ///     Terryt property: <c>STAN_NA</c>
    /// </summary>
    [Name("STAN_NA")]
    public required DateOnly ValidFromDate { get; init; }

    public bool Equals(SimcDto? other)
    {
        if (other is null)
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return Id == other.Id;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null)
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        if (obj.GetType() != GetType())
        {
            return false;
        }

        return Equals((SimcDto)obj);
    }

    public override int GetHashCode()
    {
        return Id;
    }
}