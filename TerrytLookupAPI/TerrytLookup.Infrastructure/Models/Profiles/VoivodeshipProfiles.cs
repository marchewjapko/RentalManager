using AutoMapper;
using TerrytLookup.Infrastructure.Models.Dto;
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
            .ForMember(x => x.Towns, x => x.Ignore());

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
    }
}