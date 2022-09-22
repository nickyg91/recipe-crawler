using System.ComponentModel.DataAnnotations;

namespace RecipeCrawler.Entities.Models
{
    public class CreateAccountModel
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

        [Required]
        public string ConfirmPassword { get; set; }
    }
}
