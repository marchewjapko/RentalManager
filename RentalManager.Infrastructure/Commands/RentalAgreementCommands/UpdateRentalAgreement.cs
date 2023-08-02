using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RentalManager.Infrastructure.Commands.RentalAgreementCommands;

public class UpdateRentalAgreement
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

    [DefaultValue(010)]
    [Range(0, int.MaxValue)]
    public int TransportTo { get; set; }

    public DateTime DateAdded { get; set; }
}