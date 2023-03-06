﻿namespace RentalManager.Core.Domain;

public class Client
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string? Email { get; set; }
    public string IdCard { get; set; } = null!;
    public string City { get; set; } = null!;
    public string Street { get; set; } = null!;
    public DateTime DateAdded { get; set; }
}