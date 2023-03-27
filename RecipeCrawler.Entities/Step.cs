using System.Text.Json.Serialization;

namespace RecipeCrawler.Entities
{
    public class Step : BaseEntity
    {
        public string Description { get; set; }
        public int RecipeId { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        [JsonIgnore]
        public Recipe Recipe { get; set; }
        public int Order { get; set; }
    }
}
