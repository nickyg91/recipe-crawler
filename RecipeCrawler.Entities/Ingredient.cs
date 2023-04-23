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

        public override bool Equals(object? obj)
        {
            if (obj == null)
            {
                return false;
            }
            var ingredient = obj as Ingredient;
            return ingredient.Id == Id && ingredient.Name == Name && ingredient.Amount == Amount &&
                   ingredient.Measurement == Measurement;
        }
    }
}
