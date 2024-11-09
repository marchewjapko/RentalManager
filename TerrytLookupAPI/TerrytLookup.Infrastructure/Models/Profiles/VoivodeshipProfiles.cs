using AutoMapper;
using TerrytLookup.Core.Domain;
using TerrytLookup.Infrastructure.Models.Dto;
using TerrytLookup.Infrastructure.Models.Dto.CreateDtos;
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
            .ForMember(x => x.Towns, x => x.Ignore())
            .ForMember(x => x.ValidFromDate, x => x.MapFrom(a => a.ValidFromDate));

        CreateMap<IEnumerable<TercDto>, Dictionary<int, CreateVoivodeshipDto>>()
            .ConvertUsing((src, dest, context) => {
                var dictionary = new Dictionary<int, CreateVoivodeshipDto>();
                foreach (var source in src)
                {
                    var destination = context.Mapper.Map<CreateVoivodeshipDto>(source);
                    dictionary[source.VoivodeshipId] = destination;
                }

                return dictionary;
            });

        CreateMap<CreateVoivodeshipDto, Voivodeship>()
            .ForMember(x => x.Id, x => x.Ignore())
            .ForMember(x => x.TerrytId, x => x.MapFrom(a => a.TerrytId))
            .ForMember(x => x.Name, x => x.MapFrom(a => a.Name))
            .ForMember(x => x.ValidFromDate, x => x.MapFrom(a => a.ValidFromDate))
            .ForMember(x => x.Timestamp, x => x.Ignore())
            .ForMember(x => x.Towns, x => x.MapFrom(a => a.Towns));

        CreateMap<Voivodeship, VoivodeshipDto>()
            .ForMember(x => x.Id, x => x.MapFrom(a => a.Id))
            .ForMember(x => x.Name, x => x.MapFrom(a => a.Name));
    }
}