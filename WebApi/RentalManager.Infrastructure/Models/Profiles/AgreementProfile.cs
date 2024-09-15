using AutoMapper;
using RentalManager.Core.Domain;
using RentalManager.Infrastructure.Models.Commands.AgreementCommands;
using RentalManager.Infrastructure.Models.DTO;

namespace RentalManager.Infrastructure.Models.Profiles;

public class AgreementProfile : Profile
{
    public AgreementProfile()
    {
        CreateMap<AgreementBaseCommand, Agreement>()
            .Include<CreateAgreement, Agreement>()
            .Include<UpdateAgreement, Agreement>()
            .ForMember(x => x.UserId, x => x.MapFrom(a => a.UserId))
            .ForMember(x => x.ClientId, x => x.MapFrom(a => a.Client.Id))
            .ForMember(x => x.Client, x => x.MapFrom(a => a.Client))
            .ForMember(x => x.Comment, x => x.MapFrom(a => a.Comment))
            .ForMember(x => x.Deposit, x => x.MapFrom(a => a.Deposit))
            .ForMember(x => x.TransportFromPrice, x => x.MapFrom(a => a.TransportFromPrice))
            .ForMember(x => x.TransportToPrice, x => x.MapFrom(a => a.TransportToPrice))
            .ForMember(x => x.DateAdded, x => x.MapFrom(a => a.DateAdded))
            .ForMember(x => x.Equipments, x => x.MapFrom(a => new List<Equipment>()));

        CreateMap<CreateAgreement, Agreement>()
            .ForMember(x => x.Payments, x => x.MapFrom(a => a.Payments));

        CreateMap<UpdateAgreement, Agreement>()
            .ForMember(x => x.UpdatedTs, x => x.MapFrom(a => DateTime.Now));

        CreateMap<Agreement, AgreementDto>()
            .ForMember(x => x.Id, x => x.MapFrom(a => a.Id))
            .ForMember(x => x.User, x => x.MapFrom(a => new UserDto
            {
                Id = a.UserId
            }))
            .ForMember(x => x.IsActive, x => x.MapFrom(a => a.IsActive))
            .ForMember(x => x.Equipments, x => x.MapFrom(a => a.Equipments))
            .ForMember(x => x.Payments, x => x.MapFrom(a => a.Payments))
            .ForMember(x => x.Comment, x => x.MapFrom(a => a.Comment))
            .ForMember(x => x.Deposit, x => x.MapFrom(a => a.Deposit))
            .ForMember(x => x.TransportFrom, x => x.MapFrom(a => a.TransportFromPrice))
            .ForMember(x => x.TransportTo, x => x.MapFrom(a => a.TransportToPrice))
            .ForMember(x => x.DateAdded, x => x.MapFrom(a => a.DateAdded));
    }
}