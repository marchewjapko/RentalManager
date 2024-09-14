using AutoMapper;
using RentalManager.Infrastructure.Models.DTO;
using RentalManager.Infrastructure.Models.ExternalServices.IdentityService;

namespace RentalManager.Infrastructure.Models.Profiles;

public class UserProfiles : Profile
{
    public UserProfiles()
    {
        CreateMap<IdentityServiceUser, UserWithRolesDto>()
            .ForMember(x => x.Id, x => x.MapFrom(a => a.Pk))
            .ForMember(x => x.UserName, x => x.MapFrom(a => a.UserName))
            .ForMember(x => x.Roles, x => x.MapFrom(a => a.Groups.Select(group => group.Name)))
            .ForMember(x => x.FirstName, x => {
                object? firstName;
                x.MapFrom(a =>
                    a.Attributes.TryGetValue("first_name", out firstName)
                        ? firstName.ToString()
                        : null);
            })
            .ForMember(x => x.LastName, x => {
                object? lastName;
                x.MapFrom(a =>
                    a.Attributes.TryGetValue("last_name", out lastName)
                        ? lastName.ToString()
                        : null);
            });
    }
}