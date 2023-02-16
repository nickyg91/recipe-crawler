using System.Text.Json;
using System.Text.Json.Serialization;

namespace RecipeCrawler.Entities
{
    public class Cookbook : BaseEntity
    {
        public string Name { get; set; }
        [JsonIgnore]
        public Chef Chef { get; set; }
        public int ChefId { get; set; }
        public ICollection<Recipe> Recipes { get; set; }
        public byte[]? CoverImage { get; set; }
        public string CoverImageBase64 => CoverImage != null ? Convert.ToBase64String(CoverImage) : string.Empty;
    }
}
