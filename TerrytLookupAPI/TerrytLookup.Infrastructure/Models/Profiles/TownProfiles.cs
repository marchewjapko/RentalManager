﻿using AutoMapper;
using TerrytLookup.Infrastructure.Models.Dto;
using TerrytLookup.Infrastructure.Models.Dto.Terryt;

namespace TerrytLookup.Infrastructure.Models.Profiles;

public class TownProfiles : Profile
{
    public TownProfiles()
    {
        CreateMap<SimcDto, CreateTownDto>()
            .ForMember(x => x.TerrytId,
                x => x.MapFrom(a => a.TownId))
            .ForMember(x => x.VoivodeshipTerrytId, x => x.MapFrom(a => a.VoivodeshipId))
            .ForMember(x => x.Name, x => x.MapFrom(a => a.TownName))
            .ForMember(x => x.UnitType, x => x.MapFrom(a => a.UnitType))
            .ForMember(x => x.Type, x => x.MapFrom(a => a.TownType))
            .ForMember(x => x.Streets, x => x.Ignore())
            .ForMember(x => x.CountyTerrytId, x => x.MapFrom(a => a.CountyId))
            .ForMember(x => x.MunicipalityTerrytId, x => x.MapFrom(a => a.MunicipalityId));

        CreateMap<IEnumerable<SimcDto>, Dictionary<int, CreateTownDto>>()
            .ConvertUsing((src, dest, context) => {
                var dictionary = new Dictionary<int, CreateTownDto>();
                foreach (var source in src)
                {
                    var destination = context.Mapper.Map<CreateTownDto>(source);
                    dictionary[source.TownId] = destination;
                }

                return dictionary;
            });
    }
}