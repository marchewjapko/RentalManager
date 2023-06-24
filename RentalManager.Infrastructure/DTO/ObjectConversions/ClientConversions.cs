using System.Text.RegularExpressions;
using RentalManager.Core.Domain;
using RentalManager.Infrastructure.Commands;

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
            Street = createClient.Street,
            DateAdded = DateTime.Now
        };
    }

    public static ClientDto ToDto(this Client client)
    {
        var clientDto = new ClientDto
        {
            Id = client.Id,
            Name = client.Name,
            Surname = client.Surname,
            PhoneNumber = Regex.Replace(Regex.Replace(client.PhoneNumber, @"\s+", ""), ".{3}", "$0 ").TrimEnd(' '),
            Email = client.Email,
            IdCard = client.IdCard?.ToUpper(),
            City = client.City,
            Street = client.Street,
            DateAdded = client.DateAdded
        };
        return clientDto;
    }

    public static ClientDto ToDto(this CreateClient createClient)
    {
        var clientDto = new ClientDto
        {
            Name = createClient.Name,
            Surname = createClient.Surname,
            PhoneNumber =
                Regex.Replace(Regex.Replace(createClient.PhoneNumber, @"\s+", ""), ".{3}", "$0 ").TrimEnd(' '),
            Email = createClient.Email,
            IdCard = createClient.IdCard?.ToUpper(),
            City = createClient.City,
            Street = createClient.Street
        };
        return clientDto;
    }

    public static ClientDto ToDto(this UpdateClient updateClient)
    {
        var clientDto = new ClientDto
        {
            Name = updateClient.Name,
            Surname = updateClient.Surname,
            PhoneNumber =
                Regex.Replace(Regex.Replace(updateClient.PhoneNumber, @"\s+", ""), ".{3}", "$0 ").TrimEnd(' '),
            Email = updateClient.Email,
            IdCard = updateClient.IdCard?.ToUpper(),
            City = updateClient.City,
            Street = updateClient.Street
        };
        return clientDto;
    }

    public static Client ToDomain(this UpdateClient updateClient)
    {
        var result = new Client
        {
            Name = updateClient.Name,
            Surname = updateClient.Surname,
            PhoneNumber = Regex.Replace(updateClient.PhoneNumber, ".{3}", "$0 ").TrimEnd(' '),
            Email = updateClient.Email,
            IdCard = updateClient.IdCard?.ToUpper(),
            City = updateClient.City,
            Street = updateClient.Street
        };
        return result;
    }

    public static Client ToDomain(this ClientDto clientDto)
    {
        var result = new Client
        {
            Id = clientDto.Id,
            Name = clientDto.Name,
            Surname = clientDto.Surname,
            PhoneNumber = Regex.Replace(clientDto.PhoneNumber, ".{3}", "$0 ").TrimEnd(' '),
            Email = clientDto.Email,
            IdCard = clientDto.IdCard?.ToUpper(),
            City = clientDto.City,
            Street = clientDto.Street,
            DateAdded = clientDto.DateAdded
        };
        return result;
    }
}