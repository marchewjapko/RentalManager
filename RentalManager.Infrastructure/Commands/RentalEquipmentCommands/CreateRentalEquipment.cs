using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace RentalManager.Infrastructure.Commands.RentalEquipmentCommands;

public class CreateRentalEquipment
{
    [Required]
    [DefaultValue("Fun equipment mk III")]
    public string Name { get; set; } = null!;

    [Required]
    [DefaultValue(100)]
    public int Price { get; set; }
    
    public IFormFile? Image { get; set; }
}