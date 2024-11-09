namespace TerrytLookup.Infrastructure.Models.Enums;

/// <summary>
///     Value corresponding to <b> RM </b> from Simc Dataset
/// </summary>
public enum TownType
{
    /// <summary>
    ///     Część miejscowości
    /// </summary>
    PartOfTown = 0,

    /// <summary>
    ///     Wieś
    /// </summary>
    Village = 1,

    /// <summary>
    ///     Kolonia
    /// </summary>
    Dependency = 2,

    /// <summary>
    ///     Przysiółek
    /// </summary>
    Hamlet = 3,

    /// <summary>
    ///     Osada
    /// </summary>
    Settlement = 4,

    /// <summary>
    ///     Osada leśna
    /// </summary>
    ForestSettlement = 5,

    /// <summary>
    ///     Osiedle
    /// </summary>
    Neighborhood = 6,

    /// <summary>
    ///     Schronisko turystyczne
    /// </summary>
    TouristChalet = 7,

    /// <summary>
    ///     Dzielnica m. st. Warszawy
    /// </summary>
    DistrictOfWarsaw = 95,

    /// <summary>
    ///     Miasto
    /// </summary>
    City = 96,

    /// <summary>
    ///     Delegatura
    /// </summary>
    Delegation = 98,

    /// <summary>
    ///     Część miasta
    /// </summary>
    PartOfCity = 98
}