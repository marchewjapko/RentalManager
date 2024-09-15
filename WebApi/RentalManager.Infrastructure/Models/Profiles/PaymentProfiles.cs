using AutoMapper;
using RentalManager.Core.Domain;
using RentalManager.Infrastructure.Models.Commands.PaymentCommands;
using RentalManager.Infrastructure.Models.DTO;

namespace RentalManager.Infrastructure.Models.Profiles;

public class PaymentProfiles : Profile
{
    public PaymentProfiles()
    {
        CreateMap<PaymentBaseCommand, Payment>()
            .Include<CreatePayment, Payment>()
            .Include<UpdatePayment, Payment>()
            .ForPath(x => x.Id, x => x.Ignore())
            .ForPath(x => x.AgreementId, x => x.Ignore())
            .ForMember(x => x.Agreement, x => x.Ignore())
            .ForMember(x => x.Method, x => x.MapFrom(a => a.Method))
            .ForMember(x => x.Amount, x => x.MapFrom(a => a.Amount))
            .ForMember(x => x.DateFrom, x => x.MapFrom(a => a.DateFrom))
            .ForMember(x => x.DateTo, x => x.MapFrom(a => a.DateTo));

        CreateMap<CreatePayment, Payment>()
            .ForMember(x => x.AgreementId, x => x.MapFrom(a => a.AgreementId));

        CreateMap<UpdatePayment, Payment>()
            .ForMember(x => x.UpdatedTs, x => x.MapFrom(a => DateTime.Now));

        CreateMap<Payment, PaymentDto>()
            .ForMember(x => x.Id, x => x.MapFrom(a => a.Id))
            .ForMember(x => x.Method, x => x.MapFrom(a => a.Method))
            .ForMember(x => x.Amount, x => x.MapFrom(a => a.Amount))
            .ForMember(x => x.DateFrom, x => x.MapFrom(a => a.DateFrom))
            .ForMember(x => x.DateTo, x => x.MapFrom(a => a.DateTo));
    }
}