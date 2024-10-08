﻿using AutoMapper;
using RentalManager.Core.Domain;
using RentalManager.Infrastructure.Models.Commands.PaymentCommands;
using RentalManager.Infrastructure.Models.DTO;

namespace RentalManager.Infrastructure.Models.Profiles;

public class PaymentProfiles : Profile
{
    public PaymentProfiles()
    {
        CreateMap<BasePaymentCommand, Payment>()
            .Include<CreatePaymentCommand, Payment>()
            .Include<UpdatePaymentCommand, Payment>()
            .ForPath(x => x.Id, x => x.Ignore())
            .ForPath(x => x.AgreementId, x => x.Ignore())
            .ForMember(x => x.Agreement, x => x.Ignore())
            .ForMember(x => x.Method, x => x.MapFrom(a => a.Method))
            .ForMember(x => x.Amount, x => x.MapFrom(a => a.Amount))
            .ForMember(x => x.DateFrom, x => x.MapFrom(a => a.DateFrom))
            .ForMember(x => x.DateTo, x => x.MapFrom(a => a.DateTo))
            .ForMember(x => x.CreatedBy, x => x.Ignore())
            .ForMember(x => x.CreatedTs, x => x.Ignore())
            .ForMember(x => x.UpdatedTs, x => x.Ignore())
            .ForMember(x => x.IsActive, x => x.Ignore());

        CreateMap<CreatePaymentCommand, Payment>()
            .ForMember(x => x.AgreementId, x => x.MapFrom(a => a.AgreementId));

        CreateMap<UpdatePaymentCommand, Payment>()
            .ForMember(x => x.UpdatedTs, x => x.MapFrom(a => DateTime.Now));

        CreateMap<Payment, PaymentDto>()
            .ForMember(x => x.Id, x => x.MapFrom(a => a.Id))
            .ForMember(x => x.Method, x => x.MapFrom(a => a.Method))
            .ForMember(x => x.Amount, x => x.MapFrom(a => a.Amount))
            .ForMember(x => x.DateFrom, x => x.MapFrom(a => a.DateFrom))
            .ForMember(x => x.DateTo, x => x.MapFrom(a => a.DateTo));
    }
}