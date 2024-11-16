using AutoMapper;
using TerrytLookup.Core.Domain;
using TerrytLookup.Infrastructure.Models.Dto;
using TerrytLookup.Infrastructure.Models.Dto.Internal.CreateDtos;
using TerrytLookup.Infrastructure.Models.Dto.Terryt;

namespace TerrytLookup.Infrastructure.Models.Profiles;

public class VoivodeshipProfiles : Profile
{
    public VoivodeshipProfiles()
    {
        CreateMap<TercDto, CreateVoivodeshipDto>()
            .ForMember(x => x.TerrytId,
                x => x.MapFrom(a => a.VoivodeshipId))
            .ForMember(x => x.Name, x => x.MapFrom(a => a.Name))
            .ForMember(x => x.Counties, x => x.Ignore())
            .ForMember(x => x.ValidFromDate, x => x.MapFrom(a => a.ValidFromDate));

        CreateMap<IEnumerable<TercDto>, Dictionary<int, CreateVoivodeshipDto>>()
            .ConvertUsing((src, _, context) =>
                src.ToDictionary(x => x.VoivodeshipId, x => context.Mapper.Map<CreateVoivodeshipDto>(x)));

        CreateMap<CreateVoivodeshipDto, Voivodeship>()
            .ForMember(x => x.Id, x => x.MapFrom(a => a.TerrytId))
            .ForMember(x => x.Name, x => x.MapFrom(a => a.Name))
            .ForMember(x => x.ValidFromDate, x => x.MapFrom(a => a.ValidFromDate))
            .ForMember(x => x.Counties, x => x.MapFrom(a => a.Counties));

        CreateMap<Voivodeship, VoivodeshipDto>()
            .ForMember(x => x.Id, x => x.MapFrom(a => a.Id))
            .ForMember(x => x.Name, x => x.MapFrom(a => a.Name));
    }
}