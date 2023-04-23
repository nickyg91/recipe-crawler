using System.Text.Json.Serialization;

namespace RecipeCrawler.Entities
{
    public class Recipe : BaseEntity
    {
        public string Name { get; set; }
        public string? CrawledHtml { get; set; }
        public ICollection<Step> Steps { get; set; } = new List<Step>();
        public int CookbookId { get; set; }
        [JsonIgnore]
        public Cookbook? Cookbook { get; set; }
    }
}
