using AutoMapper;
using RentalManager.Core.Domain;
using RentalManager.Infrastructure.Models.Commands.EquipmentCommands;
using RentalManager.Infrastructure.Models.DTO;

namespace RentalManager.Infrastructure.Models.Profiles;

public class EquipmentProfiles : Profile
{
    public EquipmentProfiles()
    {
        CreateMap<EquipmentBaseCommand, Equipment>()
            .Include<CreateEquipment, Equipment>()
            .Include<UpdateEquipment, Equipment>()
            .ForMember(x => x.Id, x => x.Ignore())
            .ForMember(x => x.Name, x => x.MapFrom(a => a.Name))
            .ForMember(x => x.Price, x => x.MapFrom(a => a.Price))
            .ForMember(x => x.Agreements, x => x.Ignore())
            .ForMember(x => x.CreatedBy, x => x.Ignore())
            .ForMember(x => x.CreatedTs, x => x.Ignore())
            .ForMember(x => x.UpdatedTs, x => x.Ignore())
            .ForMember(x => x.IsActive, x => x.Ignore());

        CreateMap<CreateEquipment, Equipment>();
        CreateMap<UpdateEquipment, Equipment>()
            .ForMember(x => x.UpdatedTs, x => x.MapFrom(a => DateTime.Now));

        CreateMap<Equipment, EquipmentDto>()
            .ForMember(x => x.Id, x => x.MapFrom(a => a.Id))
            .ForMember(x => x.Name, x => x.MapFrom(a => a.Name))
            .ForMember(x => x.Price, x => x.MapFrom(a => a.Price));
    }
}