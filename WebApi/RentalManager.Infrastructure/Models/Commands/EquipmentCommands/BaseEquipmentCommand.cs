using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RentalManager.Infrastructure.Models.Commands.EquipmentCommands;

public class BaseEquipmentCommand
{
    [Required]
    [DefaultValue("Fun equipment mk III")]
    public string Name { get; set; }

    [Required]
    [DefaultValue(100)]
    public int Price { get; set; }
}