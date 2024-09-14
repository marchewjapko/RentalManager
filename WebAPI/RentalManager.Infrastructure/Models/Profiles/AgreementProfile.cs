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
            .ForMember(x => x.EmployeeId, x => x.MapFrom(a => a.EmployeeId))
            .ForMember(x => x.Client, x => x.Ignore())
            .ForMember(x => x.ClientId, x => x.MapFrom(a => a.ClientId))
            .ForMember(x => x.Comment, x => x.MapFrom(a => a.Comment))
            
    }
}