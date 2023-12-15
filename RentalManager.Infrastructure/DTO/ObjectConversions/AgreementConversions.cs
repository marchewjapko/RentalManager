using RentalManager.Core.Domain;
using RentalManager.Infrastructure.Commands.AgreementCommands;
using Payment = RentalManager.Global.Requests.GetAgreementDocument.Payment;

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

    public static Agreement ToDomain(this UpdateAgreement updateAgreement)
    {
        return new Agreement
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
    }

    public static Global.Requests.GetAgreementDocument.Agreement ToDocumentRequest(
        this AgreementDto agreement)
    {
        return new Global.Requests.GetAgreementDocument.Agreement
        {
            Client = new Global.Requests.GetAgreementDocument.Client
            {
                Name = agreement.Client.Name,
                Surname = agreement.Client.Surname,
                Address = agreement.Client.City + " " + agreement.Client.Street,
                PhoneNumber = agreement.Client.PhoneNumber,
                IdCard = agreement.Client.IdCard
            },
            Equipments = agreement.Equipment.Select(x =>
                    new Global.Requests.GetAgreementDocument.Equipment
                    {
                        Name = x.Name,
                        Price = x.Price
                    })
                .ToList(),
            Payments = agreement.Payments.Select(x => new Payment
                {
                    Start = x.From,
                    End = x.To,
                    Value = x.Amount
                })
                .ToList(),
            TransportFrom = agreement.TransportFrom,
            TransportTo = agreement.TransportTo,
            Deposit = agreement.Deposit
        };
    }
}