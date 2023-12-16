using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using RentalManager.Infrastructure.Commands.PaymentCommands;
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global

namespace RentalManager.Infrastructure.Commands.AgreementCommands;

public class AgreementBaseCommand
{
    [Required]
    [DefaultValue(1)]
    public int EmployeeId { get; init; }

    [Required]
    [DefaultValue(true)]
    public bool IsActive { get; init; }

    [Required]
    [DefaultValue(1)]
    public int ClientId { get; init; }

    [Required]
#pragma warning disable CA1861
    [DefaultValue(new[] { 1 })]
#pragma warning restore CA1861
    public List<int> EquipmentIds { get; init; } = null!;

    [DefaultValue("Fun comment")]
    public string? Comment { get; init; }

    [Required]
    [DefaultValue(100)]
    public int Deposit { get; init; }

    [Required]
    [DefaultValue(100)]
    public int? TransportFromPrice { get; init; }

    [Required]
    [DefaultValue(100)]
    public int TransportToPrice { get; init; }

    [Required]
    [DefaultValue(typeof(DateTime), "2023-01-01 00:00:00")]
    public DateTime DateAdded { get; init; }

    public List<CreatePayment> Payments { get; init; } = null!;
}