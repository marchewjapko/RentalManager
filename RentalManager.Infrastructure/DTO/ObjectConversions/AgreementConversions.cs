using RentalManager.Core.Domain;
using RentalManager.Infrastructure.Commands.AgreementCommands;

namespace RentalManager.Infrastructure.DTO.ObjectConversions;

public static class AgreementConversions
{
    public static Agreement ToDomain(this CreateAgreement createAgreement)
    {
        return new Agreement
        {
            EmployeeId = createAgreement.EmployeeId,
            IsActive = createAgreement.IsActive,
            ClientId = createAgreement.ClientId,
            Comment = createAgreement.Comment,
            Deposit = createAgreement.Deposit,
            TransportFromPrice = createAgreement.TransportFromPrice,
            TransportToPrice = createAgreement.TransportToPrice,
            Payments = createAgreement.Payments.Select(x => x.ToDomain())
                .ToList(),
            DateAdded = createAgreement.DateAdded
        };
    }

    public static AgreementDto ToDto(this Agreement agreement)
    {
        return new AgreementDto
        {
            Id = agreement.Id,
            Employee = agreement.Employee.ToDto(),
            IsActive = agreement.IsActive,
            Client = agreement.Client.ToDto(),
            Equipment = agreement.Equipment.Select(x => x.ToDto()),
            Comment = agreement.Comment,
            Deposit = agreement.Deposit,
            TransportFrom = agreement.TransportFromPrice,
            TransportTo = agreement.TransportToPrice,
            Payments = agreement.Payments.Select(x => x.ToDto()),
            DateAdded = agreement.DateAdded
        };
    }

    public static AgreementDto ToDto(this CreateAgreement createAgreement,
        EmployeeDto employeeDto,
        ClientDto clientDto,
        IEnumerable<EquipmentDto> equipmentDtos,
        IEnumerable<PaymentDto> paymentDtos)
    {
        return new AgreementDto
        {
            Employee = employeeDto,
            IsActive = createAgreement.IsActive,
            Client = clientDto,
            Equipment = equipmentDtos,
            Comment = createAgreement.Comment,
            Deposit = createAgreement.Deposit,
            TransportFrom = createAgreement.TransportFromPrice,
            TransportTo = createAgreement.TransportToPrice,
            Payments = paymentDtos
        };
    }

    public static AgreementDto ToDto(this UpdateAgreement updateAgreement,
        EmployeeDto employeeDto,
        ClientDto clientDto,
        IEnumerable<EquipmentDto> equipmentDtos,
        IEnumerable<PaymentDto> paymentDtos)
    {
        return new AgreementDto
        {
            Employee = employeeDto,
            IsActive = updateAgreement.IsActive,
            Client = clientDto,
            Equipment = equipmentDtos,
            Comment = updateAgreement.Comment,
            Deposit = updateAgreement.Deposit,
            TransportFrom = updateAgreement.TransportFromPrice,
            TransportTo = updateAgreement.TransportToPrice,
            Payments = paymentDtos
        };
    }

    public static Agreement ToDomain(this UpdateAgreement updateAgreement)
    {
        var result = new Agreement
        {
            EmployeeId = updateAgreement.EmployeeId,
            IsActive = updateAgreement.IsActive,
            ClientId = updateAgreement.ClientId,
            Comment = updateAgreement.Comment,
            Deposit = updateAgreement.Deposit,
            TransportFromPrice = updateAgreement.TransportFromPrice,
            TransportToPrice = updateAgreement.TransportToPrice,
            DateAdded = updateAgreement.DateAdded
        };

        return result;
    }
}