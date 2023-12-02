using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using RentalManager.Infrastructure.DTO;

namespace RentalManager.Infrastructure.Commands.EmployeeCommands;

public class UpdateEmployee
{
    [Required]
    [DefaultValue("John")]
    public string Name { get; set; } = null!;

    [Required]
    [DefaultValue("Kowalski")]
    public string Surname { get; set; } = null!;
    
    public IFormFile? Image { get; set; }
    
    [Required]
    [DefaultValue(GenderDto.Man)]
    public GenderDto Gender { get; set; }
}