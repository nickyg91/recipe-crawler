using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeCrawler.Entities
{
    public class Chef : BaseEntity
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        [NotMapped]
        public string Password { get; set; }
        public Guid? EmailVerificationGuid { get; set; }
        public bool IsEmailVerified { get; set; }
        public List<Cookbook> Cookbooks { get; set; }
    }
}
