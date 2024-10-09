using CsvHelper.Configuration.Attributes;

namespace TerrytLookup.Infrastructure.Models.Dto.Terryt;

public class SimcDto : IEquatable<SimcDto>
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
    public required int CountyId { get; set; }

    /// <summary>
    ///     Terryt property: <c>GMI</c>
    /// </summary>
    [Name("GMI")]
    public required int MunicipalityId { get; set; }

    /// <summary>
    ///     Terryt property: <c>RODZ_GMI</c>
    /// </summary>
    [Name("RODZ_GMI")]
    public required int UnitType { get; set; }

    /// <summary>
    ///     Terryt property: <c>RM</c>
    /// </summary>
    [Name("RM")]
    public required int TownType { get; set; }

    /// <summary>
    ///     Terryt property: <c>MZ</c>
    /// </summary>
    [Name("MZ")]
    public required bool HasCommonName { get; set; }

    /// <summary>
    ///     Terryt property: <c>NAZWA</c>
    /// </summary>
    [Name("NAZWA")]
    public required string TownName { get; set; }

    /// <summary>
    ///     Terryt property: <c>SYM</c>
    /// </summary>
    [Name("SYM")]
    public required int TownId { get; set; }

    /// <summary>
    ///     Terryt property: <c>SYMPOD</c>
    /// </summary>
    [Name("SYMPOD")]
    public required int ParentTownId { get; set; }

    /// <summary>
    ///     Terryt property: <c>STAN_NA</c>
    /// </summary>
    [Name("STAN_NA")]
    public required DateOnly ValidFromDate { get; set; }

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

        return TownId == other.TownId;
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
        return TownId;
    }
}