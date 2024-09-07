using RentalManager.Core.Domain;
using RentalManager.Infrastructure.Commands.UserCommands;
using RentalManager.Infrastructure.Extensions;

namespace RentalManager.Infrastructure.DTO.ObjectConversions;

public static class UserConversions
{
    public static User ToDomain(this CreateUser createUser)
    {
        return new User
        {
            Name = createUser.Name,
            UserName = createUser.UserName,
            Surname = createUser.Surname,
            CreatedTs = DateTime.Now,
            Image = createUser.Image.ToByteArray()
        };
    }

    public static UserDto ToDto(this User user)
    {
        return new UserDto
        {
            Id = user.Id,
            Name = user.Name,
            UserName = user.UserName,
            Surname = user.Surname,
        };
    }

    public static User ToDomain(this UpdateUser updateUser)
    {
        return new User
        {
            Name = updateUser.Name,
            Surname = updateUser.Surname,
            Image = updateUser.Image.ToByteArray()
        };
    }

    public static UserWithRolesDto ToUserWithRoles(this User user)
    {
        return new UserWithRolesDto
        {
            Id = user.Id,
            Name = user.Name,
            UserName = user.UserName,
            Surname = user.Surname,
        };
    }
}