using AutoMapper;
using TerrytLookup.Core.Domain;
using TerrytLookup.Infrastructure.Models.Dto;
using TerrytLookup.Infrastructure.Models.Dto.Internal.CreateDtos;
using TerrytLookup.Infrastructure.Models.Dto.Terryt;

namespace TerrytLookup.Infrastructure.Models.Profiles;

public class TownProfiles : Profile
{
    public TownProfiles()
    {
        CreateMap<SimcDto, CreateTownDto>()
            .ForMember(x => x.TerrytId, x => x.MapFrom(a => a.Id))
            .ForMember(x => x.Name, x => x.MapFrom(a => a.Name))
            .ForMember(x => x.CountyTerrytId,
                x => x.MapFrom((source, _, _) => (source.VoivodeshipId, source.CountyId)))
            .ForMember(x => x.ParentTownTerrytId, x => x.MapFrom(a => a.ParentId))
            .ForMember(x => x.ValidFromDate, x => x.MapFrom(a => a.ValidFromDate))
            .ForMember(x => x.ParentTown, x => x.Ignore())
            .ForMember(x => x.Streets, x => x.Ignore())
            .ForMember(x => x.County, x => x.Ignore());

        CreateMap<IEnumerable<SimcDto>, Dictionary<int, CreateTownDto>>()
            .ConvertUsing((src, _, context) =>
                src.ToDictionary(x => x.Id, x => context.Mapper.Map<CreateTownDto>(x)));

        CreateMap<CreateTownDto, Town>()
            //.ForMember(x => x.Id, x => x.Ignore())
            .ForMember(x => x.Id, x => x.MapFrom(a => a.TerrytId))
            .ForMember(x => x.Name, x => x.MapFrom(a => a.Name))
            .ForMember(x => x.ValidFromDate, x => x.MapFrom(a => a.ValidFromDate))
            .ForMember(x => x.Streets, x => x.MapFrom(a => a.Streets))
            .ForMember(x => x.ParentTown, x => x.MapFrom(a => a.ParentTown))
            .ForMember(x => x.County, x => x.Ignore());
        
        CreateMap<Town, TownDto>()
            .ForMember(x => x.Id, x => x.MapFrom(a => a.Id))
            .ForMember(x => x.Name, x => x.MapFrom(a => a.Name));
    }
}