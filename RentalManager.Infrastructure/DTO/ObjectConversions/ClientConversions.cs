using RentalManager.Core.Domain;
using RentalManager.Infrastructure.Commands.ClientCommands;

namespace RentalManager.Infrastructure.DTO.ObjectConversions;

public static class ClientConversions
{
    public static Client ToDomain(this CreateClient createClient)
    {
        return new Client
        {
            Name = createClient.Name,
            Surname = createClient.Surname,
            PhoneNumber = createClient.PhoneNumber,
            Email = createClient.Email,
            IdCard = createClient.IdCard?.ToUpper(),
            City = createClient.City,
            Street = createClient.Street
        };
    }

    public static ClientDto ToDto(this Client client)
    {
        var clientDto = new ClientDto
        {
            Id = client.Id,
            Name = client.Name,
            Surname = client.Surname,
            PhoneNumber = client.PhoneNumber,
            Email = client.Email,
            IdCard = client.IdCard?.ToUpper(),
            City = client.City,
            Street = client.Street
        };

        return clientDto;
    }

    public static Client ToDomain(this UpdateClient updateClient)
    {
        var result = new Client
        {
            Name = updateClient.Name,
            Surname = updateClient.Surname,
            PhoneNumber = updateClient.PhoneNumber,
            Email = updateClient.Email,
            IdCard = updateClient.IdCard?.ToUpper(),
            City = updateClient.City,
            Street = updateClient.Street
        };

        return result;
    }
}