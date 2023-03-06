using System.Text.RegularExpressions;
using RentalManager.Core.Domain;
using RentalManager.Infrastructure.Commands;

namespace RentalManager.Infrastructure.DTO;

public static class ObjectConversion
{
    private static string ToUpperCheckForNull(this string str)
    {
        return !string.IsNullOrEmpty(str) ? str.ToUpper() : str;
    }

    #region Client Conversions

    public static Client ToDomain(this CreateClient createClient)
    {
        return new Client
        {
            Name = createClient.Name,
            Surname = createClient.Surname,
            PhoneNumber = createClient.PhoneNumber,
            Email = createClient.Email,
            IdCard = ToUpperCheckForNull(createClient.IdCard),
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
            IdCard = ToUpperCheckForNull(client.IdCard),
            City = client.City,
            Street = client.Street,
            DateAdded = client.DateAdded
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
            IdCard = ToUpperCheckForNull(updateClient.IdCard),
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
            IdCard = ToUpperCheckForNull(clientDto.IdCard),
            City = clientDto.City,
            Street = clientDto.Street,
            DateAdded = clientDto.DateAdded
        };
        return result;
    }

    #endregion

    #region Employee Conversions

    public static Employee ToDomain(this CreateEmployee createEmployee)
    {
        return new Employee
        {
            Name = createEmployee.Name,
            Surname = createEmployee.Surname,
            DateAdded = DateTime.Now
        };
    }

    public static EmployeeDto ToDto(this Employee employee)
    {
        return new EmployeeDto
        {
            Id = employee.Id,
            Name = employee.Name,
            Surname = employee.Surname,
            DateAdded = employee.DateAdded
        };
    }

    public static Employee ToDomain(this UpdateEmployee updateEmployee)
    {
        var result = new Employee
        {
            Name = updateEmployee.Name,
            Surname = updateEmployee.Surname
        };
        return result;
    }

    #endregion

    #region RentalEquipment Conversions

    public static RentalEquipment ToDomain(this CreateRentalEquipment createRentalEquipment)
    {
        return new RentalEquipment
        {
            Name = createRentalEquipment.Name,
            Price = createRentalEquipment.Price,
            DateAdded = DateTime.Now
        };
    }

    public static RentalEquipmentDto ToDto(this RentalEquipment rentalEquipment)
    {
        return new RentalEquipmentDto
        {
            Id = rentalEquipment.Id,
            Name = rentalEquipment.Name,
            Price = rentalEquipment.Price,
            DateAdded = rentalEquipment.DateAdded
        };
    }

    public static RentalEquipment ToDomain(this UpdateRentalEquipment updateRentalEquipment)
    {
        var result = new RentalEquipment
        {
            Name = updateRentalEquipment.Name,
            Price = updateRentalEquipment.Price
        };
        return result;
    }

    public static RentalEquipment ToDomain(this RentalEquipmentDto rentalEquipmentDto)
    {
        var result = new RentalEquipment
        {
            Id = rentalEquipmentDto.Id,
            Name = rentalEquipmentDto.Name,
            Price = rentalEquipmentDto.Price,
            DateAdded = rentalEquipmentDto.DateAdded
        };
        return result;
    }

    #endregion

    #region RentalAgreement Conversions

    public static RentalAgreement ToDomain(this CreateRentalAgreement createRentalAgreement)
    {
        return new RentalAgreement
        {
            EmployeeId = createRentalAgreement.EmployeeId,
            IsActive = createRentalAgreement.IsActive,
            ClientId = createRentalAgreement.ClientId,
            Comment = createRentalAgreement.Comment,
            Deposit = createRentalAgreement.Deposit,
            TransportFrom = createRentalAgreement.TransportFrom,
            TransportTo = createRentalAgreement.TransportTo,
            Payments = createRentalAgreement.Payments.Select(x => x.ToDomain()).ToList(),
            DateAdded = createRentalAgreement.DateAdded
        };
    }

    public static RentalAgreementDto ToDto(this RentalAgreement rentalAgreement)
    {
        return new RentalAgreementDto
        {
            Id = rentalAgreement.Id,
            Employee = rentalAgreement.Employee.ToDto(),
            IsActive = rentalAgreement.IsActive,
            Client = rentalAgreement.Client.ToDto(),
            RentalEquipment = rentalAgreement.RentalEquipment.Select(x => x.ToDto()),
            Comment = rentalAgreement.Comment,
            Deposit = rentalAgreement.Deposit,
            TransportFrom = rentalAgreement.TransportFrom,
            TransportTo = rentalAgreement.TransportTo,
            Payments = rentalAgreement.Payments.Select(x => x.ToDto()),
            DateAdded = rentalAgreement.DateAdded
        };
    }

    public static RentalAgreement ToDomain(this UpdateRentalAgreement updateRentalAgreement)
    {
        var result = new RentalAgreement
        {
            EmployeeId = updateRentalAgreement.EmployeeId,
            IsActive = updateRentalAgreement.IsActive,
            ClientId = updateRentalAgreement.ClientId,
            Comment = updateRentalAgreement.Comment,
            Deposit = updateRentalAgreement.Deposit,
            TransportFrom = updateRentalAgreement.TransportFrom,
            TransportTo = updateRentalAgreement.TransportTo,
            DateAdded = updateRentalAgreement.DateAdded
        };
        return result;
    }

    #endregion

    #region Payment Conversions

    public static Payment ToDomain(this CreatePayment createPayment)
    {
        return new Payment
        {
            Method = createPayment.Method,
            Amount = createPayment.Amount,
            DateAdded = DateTime.Now,
            From = createPayment.From,
            To = createPayment.To
        };
    }

    public static PaymentDto ToDto(this Payment payment)
    {
        return new PaymentDto
        {
            Id = payment.Id,
            Method = payment.Method,
            Amount = payment.Amount,
            DateAdded = payment.DateAdded,
            From = payment.From,
            To = payment.To
        };
    }

    public static Payment ToDomain(this UpdatePayment updatePayment)
    {
        return new Payment
        {
            Method = updatePayment.Method,
            Amount = updatePayment.Amount,
            From = updatePayment.From,
            To = updatePayment.To
        };
    }

    #endregion
}