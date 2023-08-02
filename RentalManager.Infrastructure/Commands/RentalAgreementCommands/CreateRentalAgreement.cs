using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using RentalManager.Infrastructure.Commands.PaymentCommands;

namespace RentalManager.Infrastructure.Commands.RentalAgreementCommands;

public class CreateRentalAgreement
{
    [DefaultValue(1)]
    [Range(0, int.MaxValue)]
    public int EmployeeId { get; set; }

    [DefaultValue(true)] public bool IsActive { get; set; }

    [DefaultValue(1)]
    [Range(0, int.MaxValue)]
    public int ClientId { get; set; }

    [MinLength(1)]
    [DefaultValue(new[] { 1 })]
    public List<int> RentalEquipmentIds { get; set; } = null!;

    [StringLength(500)]
    [DefaultValue("Fun comment")]
    public string? Comment { get; set; }

    [DefaultValue(100)]
    [Range(0, int.MaxValue)]
    public int Deposit { get; set; }

    [DefaultValue(100)]
    [Range(0, int.MaxValue)]
    public int? TransportFrom { get; set; }

    [DefaultValue(100)]
    [Range(0, int.MaxValue)]
    public int TransportTo { get; set; }

    public DateTime DateAdded { get; set; }
    public List<CreatePayment> Payments { get; set; } = null!;
}