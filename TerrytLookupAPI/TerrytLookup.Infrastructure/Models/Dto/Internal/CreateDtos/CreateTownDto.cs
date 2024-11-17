using System.Collections.Concurrent;

namespace TerrytLookup.Infrastructure.Models.Dto.Internal.CreateDtos;

public sealed class CreateTownDto : IEquatable<CreateTownDto>
{
    public required int TerrytId { get; init; }

    public required string Name { get; init; }

    public required (int voivodeshipId, int countyId) CountyTerrytId { get; init; }

    public required int ParentTownTerrytId { get; init; }

    public required DateOnly ValidFromDate { get; init; }

    public CreateTownDto? ParentTown { get; set; }

    public ConcurrentBag<CreateStreetDto> Streets { get; init; } = [];

    public CreateCountyDto? County { get; set; }

    public void CopyStreetsTo(CreateTownDto town)
    {
        foreach (var street in Streets) town.Streets.Add(street);
    }

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