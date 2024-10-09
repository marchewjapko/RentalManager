using AutoMapper;
using TerrytLookup.Core.Repositories;
using TerrytLookup.Infrastructure.Models.Dto;

namespace TerrytLookup.Infrastructure.Services.TownService;

public class TownService(ITownRepository townRepository, IVoivodeshipRepository voivodeshipRepository, IMapper mapper) : ITownService
{
    // public async Task AddAsync(IEnumerable<SimcDto> simcDtos)
    // {
    //     var towns = simcDtos.Where(x => x.TownId == x.ParentId)
    //         .OrderBy(x => x.Name);
    //
    //     var townEntities = mapper.Map<IEnumerable<Town>>(towns);
    //
    //     var voivodeships = await voivodeshipRepository.BrowseAllAsync().ToListAsync();
    //     
    //     foreach (var town in townEntities)
    //     {
    //         // town.VoivodeshipId = voivodeships.FirstOrDefault(x => x.TerrytVoivodeshipId == town.VoivodeshipId).Id;
    //     }
    //     
    //     await townRepository.AddRangeAsync(townEntities);
    // }
    //
    // public async ValueTask<List<SimcDto>> ParseSimcData(Stream stream)
    // {
    //     var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
    //     {
    //         Delimiter = ";",
    //         Mode = CsvMode.NoEscape
    //     };
    //
    //     try
    //     {
    //         using var reader = new StreamReader(stream);
    //         using var csv = new CsvReader(reader, csvConfig);
    //
    //         return await csv.GetRecordsAsync<SimcDto>().ToListAsync();
    //     }
    //     catch (Exception ex)
    //     {
    //         throw new TerrytParsingException("SIMC", ex);
    //     }
    // }

    public IEnumerable<CreateTownDto> BrowseAllAsync(string name)
    {
        throw new NotImplementedException();
    }
}