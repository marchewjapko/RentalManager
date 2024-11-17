using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RentalManager.Infrastructure.Models.Commands.AgreementCommands;

public class BaseAgreementCommand
{
    [Required]
    [DefaultValue(1)]
    public int UserId { get; init; }

    [Required]
    [DefaultValue(true)]
    public bool IsActive { get; init; } = true;

    [Required]
    [DefaultValue(1)]
    public int ClientId { get; init; }

    [Required]
    [DefaultValue(new[] { 1 })]
    public required ICollection<int> EquipmentsIds { get; init; }

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
}