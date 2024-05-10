﻿using System.ComponentModel.DataAnnotations;

namespace CityReporter.Models.DTOs
{
    public class LoginDto
    {
        [Required]
        [MinLength(5)]
        public string Email { get; set; }
        [Required]
        [MinLength(5)]
        public string Password { get; set; }
    }
}
