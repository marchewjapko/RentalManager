using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RentalManager.Infrastructure.Commands.RentalAgreementCommands;

public class UpdateRentalAgreement
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

    [DefaultValue(010)]
    public int TransportTo { get; set; }

    public DateTime DateAdded { get; set; }
}