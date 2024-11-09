namespace TerrytLookup.Infrastructure.Models.Enums;

/// <summary>
///     Value corresponding to <b> RODZ_GMI </b> from Simc Dataset <br />
///     <see
///         href="https://eteryt.stat.gov.pl/eTeryt/rejestr_teryt/udostepnianie_danych/baza_teryt/uzytkownicy_indywidualni/pobieranie/pliki_pelne_struktury.aspx#tStructureFileSIMC">
///         eteryt.stat.gov.pl
///     </see>
/// </summary>
public enum TownUnitType
{
    /// <summary>
    ///     Gmina miejska
    /// </summary>
    UrbanMunicipality = 1,

    /// <summary>
    ///     Gmina wiejska
    /// </summary>
    RuralMunicipality = 2,

    /// <summary>
    ///     Gmina miejsko-wiejska
    /// </summary>
    UrbanRuralMunicipality = 3,

    /// <summary>
    ///     Miasto w gminie miejsko-wiejskiej
    /// </summary>
    CityInUrbanRuralMunicipality = 4,

    /// <summary>
    ///     Obszar wiejski w gminie miejsko-wiejskiej
    /// </summary>
    RuralAreaInUrbanRuralMunicipality = 5,

    /// <summary>
    ///     Dzielnica w m.st. Warszawa
    /// </summary>
    DistrictOfWarsaw = 8,

    /// <summary>
    ///     Delegatury miast: Kraków, Łódź, Poznań i Wrocław
    /// </summary>
    Delegations = 9
}