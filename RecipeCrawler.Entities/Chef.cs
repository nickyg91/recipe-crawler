namespace RecipeCrawler.Entities
{
    public class Chef : BaseEntity
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Password { get; set; }
        public List<Cookbook> Cookbooks { get; set; }
    }
}
