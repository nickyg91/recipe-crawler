using RecipeCrawler.Core.Configuration;

namespace RecipeCrawler.Web.Configuration
{
    public class RecipeCrawlerConfiguration
    {
        public string? Redis { get; set; }
        public JwtSettingsOptions? JwtSettings { get; set; }
        public EmailConfigurationOptions? EmailConfiguration { get; set; }
    }
}
