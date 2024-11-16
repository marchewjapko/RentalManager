using AutoMapper;
using TerrytLookup.Core.Domain;
using TerrytLookup.Infrastructure.Models.Dto;
using TerrytLookup.Infrastructure.Models.Dto.Internal.CreateDtos;
using TerrytLookup.Infrastructure.Models.Dto.Terryt;

namespace TerrytLookup.Infrastructure.Models.Profiles;

public class StreetProfiles : Profile
{
    public StreetProfiles()
    {
        CreateMap<UlicDto, CreateStreetDto>()
            .ForMember(x => x.TerrytTownId, x => x.MapFrom(a => a.TownId))
            .ForMember(x => x.TerrytNameId, x => x.MapFrom(a => a.StreetNameId))
            .ForMember(x => x.Name, x => x.MapFrom((a, _) => {
                string?[] nameParts = [a.StreetPrefix, a.StreetNameSecondPart, a.StreetNameFirstPart];

                return string.Join(" ", nameParts.Where(part => !string.IsNullOrEmpty(part)));
            }))
            .ForMember(x => x.Town, x => x.Ignore())
            .ForMember(x => x.ValidFromDate, x => x.MapFrom(a => a.ValidFromDate));

        CreateMap<CreateStreetDto, Street>()
            //.ForMember(x => x.Id, x => x.Ignore())
            .ForMember(x => x.NameId, x => x.MapFrom(a => a.TerrytNameId))
            .ForMember(x => x.Name, x => x.MapFrom(a => a.Name))
            .ForMember(x => x.Town, x => x.Ignore())
            .ForMember(x => x.ValidFromDate, x => x.MapFrom(a => a.ValidFromDate));

        CreateMap<Street, StreetDto>()
            .ForMember(x => x.TownId, x => x.MapFrom(a => a.TownId))
            .ForMember(x => x.NameId, x => x.MapFrom(a => a.NameId))
            .ForMember(x => x.Name, x => x.MapFrom(a => a.Name));
    }
}