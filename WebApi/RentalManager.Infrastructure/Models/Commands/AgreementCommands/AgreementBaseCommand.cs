using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using RentalManager.Infrastructure.Models.Commands.ClientCommands;
using RentalManager.Infrastructure.Models.Commands.EquipmentCommands;

namespace RentalManager.Infrastructure.Models.Commands.AgreementCommands;

public class AgreementBaseCommand
{
    [Required]
    [DefaultValue(1)]
    public int UserId { get; init; }

    [Required]
    [DefaultValue(true)]
    public bool IsActive { get; init; }

    [Required]
    public CreateOrGetClient Client { get; init; }

    [Required]
    public List<CreateOrGetEquipment> Equipments { get; init; } = null!;

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