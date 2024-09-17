using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace RentalManager.Infrastructure.Models.Commands.EquipmentCommands;

public class EquipmentBaseCommand
{
    [Required]
    [DefaultValue("Fun equipment mk III")]
    public string Name { get; set; }

    [Required]
    [DefaultValue(100)]
    public int Price { get; set; }
}