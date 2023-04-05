namespace RecipeCrawler.Entities;

public class StepIngredient : BaseEntity
{
    public int Id { get; set; }
    public Ingredient Ingredient { get; set; }
    public int IngredientId { get; set; }
    public Step Step { get; set; }
    public int StepId { get; set; }
}