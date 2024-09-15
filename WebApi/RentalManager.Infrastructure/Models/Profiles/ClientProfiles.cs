using AutoMapper;
using RentalManager.Core.Domain;
using RentalManager.Infrastructure.Models.Commands.ClientCommands;
using RentalManager.Infrastructure.Models.DTO;

namespace RentalManager.Infrastructure.Models.Profiles;

public class ClientProfiles : Profile
{
    public ClientProfiles()
    {
        CreateMap<ClientBaseCommand, Client>()
            .Include<CreateClient, Client>()
            .Include<UpdateClient, Client>()
            .ForMember(x => x.Id, x => x.Ignore())
            .ForMember(x => x.FirstName, x => x.MapFrom(a => a.FirstName))
            .ForMember(x => x.LastName, x => x.MapFrom(a => a.LastName))
            .ForMember(x => x.PhoneNumber, x => x.MapFrom(a => a.PhoneNumber))
            .ForMember(x => x.Email, x => x.MapFrom(a => a.Email))
            .ForMember(x => x.IdCard, x => x.MapFrom(a => a.IdCard))
            .ForMember(x => x.City, x => x.MapFrom(a => a.City))
            .ForMember(x => x.Street, x => x.MapFrom(a => a.Street));

        CreateMap<CreateClient, Client>();
        CreateMap<UpdateClient, Client>()
            .ForMember(x => x.UpdatedTs, x => x.MapFrom(a => DateTime.Now));

        CreateMap<Client, ClientDto>()
            .ForMember(x => x.Id, x => x.MapFrom(a => a.Id))
            .ForMember(x => x.FirstName, x => x.MapFrom(a => a.FirstName))
            .ForMember(x => x.LastName, x => x.MapFrom(a => a.LastName))
            .ForMember(x => x.PhoneNumber, x => x.MapFrom(a => a.PhoneNumber))
            .ForMember(x => x.Email, x => x.MapFrom(a => a.Email))
            .ForMember(x => x.IdCard, x => x.MapFrom(a => a.IdCard))
            .ForMember(x => x.City, x => x.MapFrom(a => a.City))
            .ForMember(x => x.Street, x => x.MapFrom(a => a.Street));

        CreateMap<ClientDto, Client>()
            .ForMember(x => x.Id, x => x.MapFrom(a => a.Id))
            .ForMember(x => x.FirstName, x => x.MapFrom(a => a.FirstName))
            .ForMember(x => x.LastName, x => x.MapFrom(a => a.LastName))
            .ForMember(x => x.PhoneNumber, x => x.MapFrom(a => a.PhoneNumber))
            .ForMember(x => x.Email, x => x.MapFrom(a => a.Email))
            .ForMember(x => x.IdCard, x => x.MapFrom(a => a.IdCard))
            .ForMember(x => x.City, x => x.MapFrom(a => a.City))
            .ForMember(x => x.Street, x => x.MapFrom(a => a.Street));

        CreateMap<CreateOrGetClient, Client>()
            .ForMember(x => x.Id, x => x.MapFrom(a => a.Id ?? 0))
            .ForMember(x => x.FirstName, x => x.MapFrom(a => a.FirstName))
            .ForMember(x => x.LastName, x => x.MapFrom(a => a.LastName))
            .ForMember(x => x.PhoneNumber, x => x.MapFrom(a => a.PhoneNumber))
            .ForMember(x => x.Email, x => x.MapFrom(a => a.Email))
            .ForMember(x => x.IdCard, x => x.MapFrom(a => a.IdCard))
            .ForMember(x => x.City, x => x.MapFrom(a => a.City))
            .ForMember(x => x.Street, x => x.MapFrom(a => a.Street));
    }
}