namespace RecipeCrawler.Core.Configuration;

public class EmailConfigurationOptions
{
    public const string EmailConfigurationSection = "EmailConfiguration";
    public string SmtpUrl { get; set; }
    public int Port { get; set; }
}