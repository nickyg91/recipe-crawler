namespace RecipeCrawler.Entities
{
    public class Ingredient : BaseEntity
    {
        public string Name { get; set; }
        public MeasurementsEnum Measurement { get; set; }
        public byte Amount { get; set; }
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
        public ICollection<StepIngredient> StepIngredients { get; set; }
    }
}
