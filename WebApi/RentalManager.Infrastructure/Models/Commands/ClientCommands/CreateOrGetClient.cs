using System.ComponentModel;

namespace RentalManager.Infrastructure.Models.Commands.ClientCommands;

public class CreateOrGetClient : ClientBaseCommand
{
    [DefaultValue(null)]
    public int? Id { get; set; }
}