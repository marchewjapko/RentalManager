using System.ComponentModel;

namespace RentalManager.Infrastructure.Models.Commands.EquipmentCommands;

public class CreateOrGetEquipment : EquipmentBaseCommand
{
    [DefaultValue(null)]
    public int? Id { get; set; }
}