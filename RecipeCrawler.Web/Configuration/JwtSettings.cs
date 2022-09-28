namespace RecipeCrawler.Web.Configuration
{
    public class JwtSettings
    {
        public const string JwtSettingsOptions = "JwtSettings";
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string AuthorityUrl { get; set; }
    }
}
