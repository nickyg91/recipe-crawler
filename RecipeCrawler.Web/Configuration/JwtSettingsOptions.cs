﻿namespace RecipeCrawler.Web.Configuration
{
    public class JwtSettingsOptions
    {
        public const string JwtSettingsSection = "JwtSettings";
        public string Key { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public string AuthorityUrl { get; set; } = string.Empty;
    }
}
