using AutoMapper;
using Microsoft.AspNetCore.Http;
using TerrytLookup.Infrastructure.ExceptionHandling.Exceptions;
using TerrytLookup.Infrastructure.Models.Dto;
using TerrytLookup.Infrastructure.Models.Dto.Terryt;

namespace TerrytLookup.Infrastructure.Services.FeedDataService;

public class FeedDataService(IMapper mapper) : IFeedDataService
{
    private static int _counter;

    /// <summary>
    ///     Asynchronously processes and feeds data from the provided CSV files into the appropriate data structures.
    /// </summary>
    /// <param name="tercCsvFile">The CSV file containing TERC data, which includes information about voivodeships.</param>
    /// <param name="simcCsvFile">The CSV file containing SIMC data, which includes information about towns.</param>
    /// <param name="ulicCsvFile">The CSV file containing ULIC data, which includes information about streets.</param>
    /// <exception cref="TerrytParsingException">Thrown when an error occurs during the parsing of the CSV files.</exception>
    /// <remarks>
    ///     This method reads data from the provided CSV files concurrently, maps the data into appropriate DTOs,
    ///     and organizes the data into voivodeships, towns, and streets. It also assigns streets to towns and towns to
    ///     voivodeships producing a complete set of data.
    /// </remarks>
    public async Task FeedTerrytDataAsync(IFormFile tercCsvFile, IFormFile simcCsvFile, IFormFile ulicCsvFile)
    {
        var now = DateTime.Now;
        _counter++;

        try
        {
            var tercReader = new TerrytReader(tercCsvFile);
            var simcReader = new TerrytReader(simcCsvFile);
            var ulicReader = new TerrytReader(ulicCsvFile);

            var tercTask = tercReader.ReadAsync<TercDto>();
            var simcTask = simcReader.ReadAsync<SimcDto>();
            var ulicTask = ulicReader.ReadAsync<UlicDto>();

            await Task.WhenAll(tercTask, simcTask, ulicTask);

            var terc = await tercTask;
            var simc = await simcTask;
            var ulic = await ulicTask;

            var voivodeships = new Dictionary<int, CreateVoivodeshipDto>();
            var towns = new Dictionary<int, CreateTownDto>();
            IEnumerable<CreateStreetDto> streets = new List<CreateStreetDto>();

            Parallel.Invoke(
                () => { voivodeships = mapper.Map<Dictionary<int, CreateVoivodeshipDto>>(terc.Where(x => x.EntityType == "województwo")); },
                () => { towns = mapper.Map<Dictionary<int, CreateTownDto>>(simc); },
                () => { streets = mapper.Map<IEnumerable<CreateStreetDto>>(ulic); }
            );

            Parallel.ForEach(streets, street => AssignStreetToTown(street, towns));
            ConsolidateTowns(towns, voivodeships);
        }
        catch (Exception ex)
        {
            throw new TerrytParsingException(ex);
        }

        Console.WriteLine($"#{_counter:D3} - Elapsed time: {(DateTime.Now - now).TotalSeconds}");
    }

    /// <summary>
    ///     Assigns a street to its town.
    /// </summary>
    /// <param name="street">The <see cref="CreateStreetDto" /> object of the street to be assigned.</param>
    /// <param name="towns">
    ///     A dictionary of <see cref="CreateTownDto" />, where the key is the town's ID and the value is the
    ///     town DTO.
    /// </param>
    /// <exception cref="InvalidOperationException">
    ///     Thrown when the town's <see cref="CreateTownDto.VoivodeshipTerrytId" /> does not exists in
    ///     <paramref name="towns" />
    /// </exception>
    public static void AssignStreetToTown(CreateStreetDto street, Dictionary<int, CreateTownDto> towns)
    {
        if (!towns.TryGetValue(street.TerrytTownId, out var town))
        {
            throw new InvalidOperationException($"Street {street.Name} is not a part of any town.");
        }

        street.Town = town;
        town.Streets.Add(street);
    }

    /// <summary>
    ///     Assigns a town to its voivodeship.
    /// </summary>
    /// <param name="town">The <see cref="CreateTownDto" /> object of the town to be assigned.</param>
    /// <param name="voivodeships">
    ///     A dictionary of <see cref="CreateVoivodeshipDto" />, where the key is the voivodeship ID and the value is the
    ///     voivodeship DTO.
    /// </param>
    /// <exception cref="InvalidOperationException">
    ///     Thrown when the town's <see cref="CreateTownDto.VoivodeshipTerrytId" /> does not exists in
    ///     <paramref name="voivodeships" />
    /// </exception>
    public static void AssignTownToVoivodeship(CreateTownDto town, Dictionary<int, CreateVoivodeshipDto> voivodeships)
    {
        if (!voivodeships.TryGetValue(town.VoivodeshipTerrytId, out var voivodeship))
        {
            throw new InvalidOperationException($"Town {town.Name} is not a part of any voivodeship.");
        }

        town.Voivodeship = voivodeship;
        voivodeship.Towns.Add(town);
    }
    
    /// <summary>
    ///     Consolidates streets from sub-towns into their respective parent towns. <br />
    ///     Towns that aren't deleted are instead assigned to their voivodeship
    /// </summary>
    /// <param name="towns">
    ///     A dictionary where the key is an integer identifier for the town,
    ///     and the value is a <see cref="CreateTownDto" /> representing the town's details.
    /// </param>
    /// <param name="voivodeships">
    ///     A dictionary where the key is an integer identifier for the voivodeship,
    ///     and the value is a <see cref="CreateVoivodeshipDto" /> representing the voivodeship's details.
    /// </param>
    /// <remarks>
    ///     This method identifies parent towns that are of type Urban Municipality with a specific
    ///     MunicipalityTerrytId and TownType of City. It then processes each town in the provided
    ///     dictionary that is either a Delegation or a District of Warsaw. If a town has streets,
    ///     it checks for a corresponding parent town and copies the streets to it.
    ///     If a sub-town does not have a parent town, an <see cref="InvalidOperationException" />
    ///     is thrown.
    /// </remarks>
    /// <exception cref="InvalidOperationException">Thrown when a sub-town does not have a parent town.</exception>
    public static void ConsolidateTowns(Dictionary<int, CreateTownDto> towns, Dictionary<int, CreateVoivodeshipDto> voivodeships)
    {
        var parentTowns = towns.Where(x => x.Value is { UnitType: TownUnitType.UrbanMunicipality, MunicipalityTerrytId: 1, Type: TownType.City })
            .Select(x => x.Value)
            .ToHashSet();

        Parallel.ForEach(towns, town => {
            if (!town.Value.ShouldBeRemoved())
            {
                AssignTownToVoivodeship(town.Value, voivodeships);

                return;
            }

            if (!town.Value.Streets.IsEmpty)
            {
                var parentTown = parentTowns.FirstOrDefault(x => town.Value.IsChildOf(x)) ??
                                 throw new InvalidOperationException($"Sub-town {town.Value.Name} does not have a parent town.");

                town.Value.CopyStreetsTo(parentTown);
            }

            towns.Remove(town.Key);
        });
    }
}