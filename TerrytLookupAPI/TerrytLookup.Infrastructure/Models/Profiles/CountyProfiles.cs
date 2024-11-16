using AutoMapper;
using TerrytLookup.Core.Domain;
using TerrytLookup.Infrastructure.Models.Dto;
using TerrytLookup.Infrastructure.Models.Dto.Internal.CreateDtos;
using TerrytLookup.Infrastructure.Models.Dto.Terryt;

namespace TerrytLookup.Infrastructure.Models.Profiles;

public class CountyProfiles : Profile
{
    public CountyProfiles()
    {
        CreateMap<TercDto, CreateCountyDto>()
            .ForMember(x => x.TerrytId,
                x => x.MapFrom((source, _, _) => (source.VoivodeshipId, source.CountyId!.Value)))
            .ForMember(x => x.Name, x => x.MapFrom(a => a.Name))
            .ForMember(x => x.ValidFromDate, x => x.MapFrom(a => a.ValidFromDate))
            .ForMember(x => x.Towns, x => x.Ignore())
            .ForMember(x => x.Voivodeship, x => x.Ignore());

        CreateMap<IEnumerable<TercDto>, Dictionary<(int, int), CreateCountyDto>>()
            .ConvertUsing((src, _, context) =>
                src.ToDictionary(x => (x.VoivodeshipId, x.CountyId!.Value), x => context.Mapper.Map<CreateCountyDto>(x)));

        CreateMap<CreateCountyDto, County>()
            .ForMember(x => x.VoivodeshipId, x => x.MapFrom(a => a.TerrytId.voivodeshipId))
            .ForMember(x => x.CountyId, x => x.MapFrom(a => a.TerrytId.countyId))
            .ForMember(x => x.Name, x => x.MapFrom(a => a.Name))
            .ForMember(x => x.ValidFromDate, x => x.MapFrom(a => a.ValidFromDate))
            .ForMember(x => x.Towns, x => x.MapFrom(a => a.Towns))
            .ForMember(x => x.Voivodeship, x => x.Ignore());

        CreateMap<County, CountyDto>()
            .ForMember(x => x.VoivodeshipId, x => x.MapFrom(a => a.VoivodeshipId))
            .ForMember(x => x.CountyId, x => x.MapFrom(a => a.CountyId))
            .ForMember(x => x.Name, x => x.MapFrom(a => a.Name));
    }
}