using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RentalManager.Infrastructure.Commands.RentalEquipmentCommands;

public class CreateRentalEquipment
{
    [Required]
    [RegularExpression(@"^[a-zA-ZąćęłńóśźżĄĘŁŃÓŚŹŻ 0-9\/.\-\+]*$", ErrorMessage = "Invalid street")]
    [StringLength(100)]
    [DefaultValue("Fun equipment mk III")]
    public string Name { get; set; } = null!;

    [Required]
    [DefaultValue(100)]
    [Range(0, int.MaxValue)]
    public int Price { get; set; }
}