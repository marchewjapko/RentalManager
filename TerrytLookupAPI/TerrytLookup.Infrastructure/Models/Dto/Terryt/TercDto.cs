using CsvHelper.Configuration.Attributes;

namespace TerrytLookup.Infrastructure.Models.Dto.Terryt;

public class TercDto : IEquatable<TercDto>
{
    /// <summary>
    ///     Terryt property: <c>WOJ</c>
    /// </summary>
    [Name("WOJ")]
    public required int VoivodeshipId { get; set; }

    /// <summary>
    ///     Terryt property: <c>POW</c>
    /// </summary>
    [Name("POW")]
    public int? CountyId { get; set; }

    /// <summary>
    ///     Terryt property: <c>GMI</c>
    /// </summary>
    [Name("GMI")]
    public int? MunicipalityId { get; set; }

    /// <summary>
    ///     Terryt property: <c>RODZ</c>
    /// </summary>
    [Name("RODZ")]
    public int? EntityTypeId { get; set; }

    /// <summary>
    ///     Terryt property: <c>NAZWA</c>
    /// </summary>
    [Name("NAZWA")]
    public required string Name { get; set; }

    /// <summary>
    ///     Terryt property: <c>NAZWA_DOD</c>
    /// </summary>
    [Name("NAZWA_DOD")]
    public required string EntityType { get; set; }

    /// <summary>
    ///     Terryt property: <c>STAN_NA</c>
    /// </summary>
    [Name("STAN_NA")]
    public required DateOnly ValidFromDate { get; set; }

    public bool Equals(TercDto? other)
    {
        if (other is null)
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return VoivodeshipId == other.VoivodeshipId && CountyId == other.CountyId && MunicipalityId == other.MunicipalityId;
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

        if (obj.GetType() != typeof(TercDto))
        {
            return false;
        }

        return Equals((TercDto)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(VoivodeshipId, CountyId, MunicipalityId);
    }
}