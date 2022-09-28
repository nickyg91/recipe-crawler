﻿using System.ComponentModel.DataAnnotations;

namespace RecipeCrawler.Entities.Models
{
    public class LoginModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
