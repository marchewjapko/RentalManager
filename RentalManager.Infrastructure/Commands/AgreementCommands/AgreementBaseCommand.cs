using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using RentalManager.Infrastructure.Commands.PaymentCommands;

namespace RentalManager.Infrastructure.Commands.AgreementCommands;

public class AgreementBaseCommand
{
    [Required]
    [DefaultValue(1)]
    public int EmployeeId { get; set; }

    [Required]
    [DefaultValue(true)]
    public bool IsActive { get; set; }

    [Required]
    [DefaultValue(1)]
    public int ClientId { get; set; }

    [Required]
    [DefaultValue(new[] { 1 })]
    public List<int> EquipmentIds { get; set; } = null!;

    [DefaultValue("Fun comment")]
    public string? Comment { get; set; }

    [Required]
    [DefaultValue(100)]
    public int Deposit { get; set; }

    [Required]
    [DefaultValue(100)]
    public int? TransportFromPrice { get; set; }

    [Required]
    [DefaultValue(100)]
    public int TransportToPrice { get; set; }

    [Required]
    [DefaultValue(typeof(DateTime), "2023-01-01 00:00:00")]
    public DateTime DateAdded { get; set; }

    public List<CreatePayment> Payments { get; set; } = null!;
}