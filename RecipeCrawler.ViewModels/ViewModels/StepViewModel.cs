namespace RecipeCrawler.ViewModels.ViewModels;

public class StepViewModel
{
    public int Id { get; set; }
    public int RecipeId { get; set; }
    public string Description { get; set; }
    public List<IngredientViewModel> Ingredients { get; set; }
}