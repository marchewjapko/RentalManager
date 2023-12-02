using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using RentalManager.Infrastructure.Commands.PaymentCommands;

namespace RentalManager.Infrastructure.Commands.RentalAgreementCommands;

public class CreateRentalAgreement
{
    [DefaultValue(1)]
    public int EmployeeId { get; set; }

    [DefaultValue(true)]
    public bool IsActive { get; set; }

    [DefaultValue(1)]
    public int ClientId { get; set; }
    
    [DefaultValue(new[] { 1 })]
    public List<int> RentalEquipmentIds { get; set; } = null!;
    
    [DefaultValue("Fun comment")]
    public string? Comment { get; set; }

    [DefaultValue(100)]
    public int Deposit { get; set; }

    [DefaultValue(100)]
    public int? TransportFrom { get; set; }

    [DefaultValue(100)]
    public int TransportTo { get; set; }

    public DateTime DateAdded { get; set; }

    public List<CreatePayment> Payments { get; set; } = null!;
}