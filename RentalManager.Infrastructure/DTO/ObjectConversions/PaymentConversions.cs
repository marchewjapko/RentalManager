using RentalManager.Core.Domain;
using RentalManager.Infrastructure.Commands;

namespace RentalManager.Infrastructure.DTO.ObjectConversions;

public static class PaymentConversions
{
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
}