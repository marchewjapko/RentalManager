using AutoMapper;
using Microsoft.AspNetCore.Http;
using TerrytLookup.Infrastructure.ExceptionHandling.Exceptions;
using TerrytLookup.Infrastructure.Models.Dto.Internal.CreateDtos;
using TerrytLookup.Infrastructure.Models.Dto.Terryt;
using TerrytLookup.Infrastructure.Models.Dto.Terryt.Updates;
using TerrytLookup.Infrastructure.Services.VoivodeshipService;

namespace TerrytLookup.Infrastructure.Services.FeedDataService;

public class FeedDataService(IMapper mapper, IVoivodeshipService voivodeshipService) : IFeedDataService
{
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
            var counties = new Dictionary<(int voivodeshipId, int countyId), CreateCountyDto>();
            var towns = new Dictionary<int, CreateTownDto>();
            IEnumerable<CreateStreetDto> streets = new List<CreateStreetDto>();

            Parallel.Invoke(
                () => { voivodeships = mapper.Map<Dictionary<int, CreateVoivodeshipDto>>(terc.Where(x => x.IsVoivodeship())); },
                () => { counties = mapper.Map<Dictionary<(int voivodeshipId, int countyId), CreateCountyDto>>(terc.Where(x => x.IsCounty())); },
                () => { towns = mapper.Map<Dictionary<int, CreateTownDto>>(simc); },
                () => { streets = mapper.Map<IEnumerable<CreateStreetDto>>(ulic); }
            );

            Parallel.Invoke(
                () => Parallel.ForEach(streets, street => AssignStreetToTown(street, towns)),
                () => Parallel.ForEach(counties, county => AssignCountyToVoivodeship(county.Value, voivodeships))
            );

            Parallel.ForEach(towns, town => {
                AssignTownToCounty(town.Value, counties);
                AssignParentToTown(town.Value, towns);
            });

            await voivodeshipService.AddRange(voivodeships.Select(x => x.Value));
        }
        catch (Exception ex)
        {
            throw new TerrytParsingException(ex);
        }
    }

    public Task UpdateSimc(TerrytUpdateDto<SimcUpdateDto> updateDto)
    {
        throw new NotImplementedException();
    }

    public static void AssignStreetToTown(CreateStreetDto street, Dictionary<int, CreateTownDto> towns)
    {
        if (!towns.TryGetValue(street.TerrytTownId, out var town))
        {
            throw new InvalidOperationException($"Street {street.Name} is not a part of any town.");
        }

        street.Town = town;
        town.Streets.Add(street);
    }

    public static void AssignCountyToVoivodeship(CreateCountyDto county, Dictionary<int, CreateVoivodeshipDto> voivodeships)
    {
        if (!voivodeships.TryGetValue(county.TerrytId.voivodeshipId, out var voivodeship))
        {
            throw new InvalidOperationException($"County {county.Name} is not a part of any voivodeship.");
        }

        county.Voivodeship = voivodeship;
        voivodeship.Counties.Add(county);
    }

    public static void AssignTownToCounty(CreateTownDto town, Dictionary<(int voivodeshipId, int countyId), CreateCountyDto> counties)
    {
        if (!counties.TryGetValue(town.CountyTerrytId, out var county))
        {
            throw new InvalidOperationException($"Town {town.Name} is not a part of any county.");
        }

        town.County = county;
        county.Towns.Add(town);
    }

    public static void AssignParentToTown(CreateTownDto town, Dictionary<int, CreateTownDto> towns)
    {
        if (town.ParentTownTerrytId == town.TerrytId)
        {
            return;
        }

        if (!towns.TryGetValue(town.ParentTownTerrytId, out var parentTown))
        {
            throw new InvalidOperationException($"Town {town.Name} does not have a parent town.");
        }

        town.ParentTown = parentTown;
    }
}