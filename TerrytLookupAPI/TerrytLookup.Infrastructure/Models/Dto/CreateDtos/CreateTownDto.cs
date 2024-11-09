using System.Collections.Concurrent;
using TerrytLookup.Infrastructure.Models.Enums;

namespace TerrytLookup.Infrastructure.Models.Dto.CreateDtos;

public class CreateTownDto : IEquatable<CreateTownDto>
{
    public required int TerrytId { get; init; }

    public required string Name { get; init; }

    /// <summary>
    ///     Terryt property: <c>RODZ_GMI</c>
    /// </summary>
    public required TownUnitType UnitType { get; init; }

    /// <summary>
    ///     Terryt property: <c>RM</c>
    /// </summary>
    public required TownType Type { get; init; }

    /// <summary>
    ///     Terryt property: <c>WOJ</c>
    /// </summary>
    public required int VoivodeshipTerrytId { get; init; }

    /// <summary>
    ///     Terryt property: <c>POW</c>
    /// </summary>
    public required int CountyTerrytId { get; init; }

    /// <summary>
    ///     Terryt property: <c>GMI</c>
    /// </summary>
    public required int MunicipalityTerrytId { get; init; }

    public required DateOnly ValidFromDate { get; set; }

    public ConcurrentBag<CreateStreetDto> Streets { get; init; } = [];

    public CreateVoivodeshipDto? Voivodeship { get; set; }

    public bool Equals(CreateTownDto? other)
    {
        if (other is null)
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return TerrytId == other.TerrytId;
    }

    public bool IsChildOf(CreateTownDto town)
    {
        if (VoivodeshipTerrytId != town.VoivodeshipTerrytId)
        {
            return false;
        }

        if (CountyTerrytId != town.CountyTerrytId)
        {
            return false;
        }

        return town.MunicipalityTerrytId == 1;
    }

    public bool ShouldBeRemoved()
    {
        return UnitType is TownUnitType.Delegations or TownUnitType.DistrictOfWarsaw;
    }

    public void CopyStreetsTo(CreateTownDto town)
    {
        foreach (var street in Streets) town.Streets.Add(street);
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

        return Equals((CreateTownDto)obj);
    }

    public override int GetHashCode()
    {
        return TerrytId;
    }
}