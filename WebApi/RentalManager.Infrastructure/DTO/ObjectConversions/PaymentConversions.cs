using RentalManager.Core.Domain;
using RentalManager.Infrastructure.Commands.PaymentCommands;

namespace RentalManager.Infrastructure.DTO.ObjectConversions;

public static class PaymentConversions
{
    public static Payment ToDomain(this CreatePayment createPayment)
    {
        return new Payment
        {
            Method = createPayment.Method,
            Amount = createPayment.Amount,
            DateFrom = createPayment.DateTimeFrom,
            DateTo = createPayment.DateTimeTo,
            AgreementId = createPayment.AgreementId
        };
    }

    public static PaymentDto ToDto(this Payment payment)
    {
        return new PaymentDto
        {
            Id = payment.Id,
            Method = payment.Method,
            Amount = payment.Amount,
            From = payment.DateFrom,
            To = payment.DateTo
        };
    }

    public static Payment ToDomain(this UpdatePayment updatePayment)
    {
        return new Payment
        {
            Method = updatePayment.Method,
            Amount = updatePayment.Amount,
            DateFrom = updatePayment.DateTimeFrom,
            DateTo = updatePayment.DateTimeTo
        };
    }
}