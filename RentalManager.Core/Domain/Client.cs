﻿using System.ComponentModel.DataAnnotations;

namespace RentalManager.Core.Domain
{
    public class Client
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Surname { get; set; }
        [Required]
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? IdCard { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
