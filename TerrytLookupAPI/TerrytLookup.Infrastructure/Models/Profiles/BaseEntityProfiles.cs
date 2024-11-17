using AutoMapper;
using TerrytLookup.Core.Domain;

namespace TerrytLookup.Infrastructure.Models.Profiles;

public class BaseEntityProfiles : Profile
{
    public BaseEntityProfiles()
    {
        CreateMap<object, BaseEntity>()
            .ForMember(x => x.CreateTimestamp, opt => opt.Ignore())
            .ForMember(x => x.UpdateTimestamp, opt => opt.Ignore());
    }
}