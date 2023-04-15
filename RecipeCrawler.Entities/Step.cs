using System.Text.Json.Serialization;

namespace RecipeCrawler.Entities
{
    public class Step : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int RecipeId { get; set; }
        public ICollection<StepIngredient> StepIngredients { get; set; }
        [JsonIgnore]
        public Recipe Recipe { get; set; }
        public int Order { get; set; }
    }
}
