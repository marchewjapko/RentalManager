using RentalManager.Core.Domain;
using RentalManager.Infrastructure.Commands;

namespace RentalManager.Infrastructure.DTO.ObjectConversions;

public static class RentalAgreementConversions
{
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

    public static RentalAgreementDto ToDto(this CreateRentalAgreement createRentalAgreement, EmployeeDto employeeDto,
        ClientDto clientDto, IEnumerable<RentalEquipmentDto> rentalEquipmentDtos, IEnumerable<PaymentDto> paymentDtos)
    {
        return new RentalAgreementDto
        {
            Employee = employeeDto,
            IsActive = createRentalAgreement.IsActive,
            Client = clientDto,
            RentalEquipment = rentalEquipmentDtos,
            Comment = createRentalAgreement.Comment,
            Deposit = createRentalAgreement.Deposit,
            TransportFrom = createRentalAgreement.TransportFrom,
            TransportTo = createRentalAgreement.TransportTo,
            Payments = paymentDtos
        };
    }

    public static RentalAgreementDto ToDto(this UpdateRentalAgreement updateRentalAgreement, EmployeeDto employeeDto,
        ClientDto clientDto, IEnumerable<RentalEquipmentDto> rentalEquipmentDtos, IEnumerable<PaymentDto> paymentDtos)
    {
        return new RentalAgreementDto
        {
            Employee = employeeDto,
            IsActive = updateRentalAgreement.IsActive,
            Client = clientDto,
            RentalEquipment = rentalEquipmentDtos,
            Comment = updateRentalAgreement.Comment,
            Deposit = updateRentalAgreement.Deposit,
            TransportFrom = updateRentalAgreement.TransportFrom,
            TransportTo = updateRentalAgreement.TransportTo,
            Payments = paymentDtos
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
}