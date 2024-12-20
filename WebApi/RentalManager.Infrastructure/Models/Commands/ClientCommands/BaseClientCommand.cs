﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RentalManager.Infrastructure.Models.Commands.ClientCommands;

public class BaseClientCommand

{
    [Required]
    [DefaultValue("John")]
    public required string FirstName { get; set; }

    [Required]
    [DefaultValue("Kowalski")]
    public required string LastName { get; set; }

    [Required]
    [DefaultValue("+48 123 123 123")]
    public required string PhoneNumber { get; set; }

    [DefaultValue("JohnKowalski@email.com")]
    public string? Email { get; set; }

    [DefaultValue("ABC 123456")]
    public string? IdCard { get; set; }

    [Required]
    [DefaultValue("Warsaw")]
    public required string City { get; set; }

    [Required]
    [DefaultValue("Woodland Ave 4/6/8")]
    public string? Street { get; set; }
}