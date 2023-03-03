using System.Text.Json.Serialization;

namespace RecipeCrawler.Entities
{
    public class Recipe : BaseEntity
    {
        public string Name { get; set; }
        public string? CrawledHtml { get; set; }
        public List<Step> Steps { get; set; } = new List<Step>();
        public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
        public int CookbookId { get; set; }
        [JsonIgnore]
        public Cookbook? Cookbook { get; set; }
    }
}
