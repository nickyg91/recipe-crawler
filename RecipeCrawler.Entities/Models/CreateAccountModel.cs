using System.ComponentModel.DataAnnotations;

namespace RecipeCrawler.Entities.Models
{
    public class CreateAccountModel
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required, MaxLength(256, ErrorMessage = "Username cannot exceed 256 characters."), MinLength(8, ErrorMessage = "Username must be at least 8 characters long.")]
        public string Username { get; set; }
        [Required, RegularExpression(@"^\S*(?=\S{6,})(?=\S*\d)(?=\S*[A-Z])(?=\S*[a-z])(?=\S*[!@#$%^&*? ])\S*$", ErrorMessage = "Password must be at least 6 characters and contain one letter, number, and special symbol.")]
        public string Password { get; set; }

        [Required, RegularExpression(@"^\S*(?=\S{6,})(?=\S*\d)(?=\S*[A-Z])(?=\S*[a-z])(?=\S*[!@#$%^&*? ])\S*$", ErrorMessage = "Password must be at least 6 characters and contain one letter, number, and special symbol.")]
        public string ConfirmPassword { get; set; }
    }
}
