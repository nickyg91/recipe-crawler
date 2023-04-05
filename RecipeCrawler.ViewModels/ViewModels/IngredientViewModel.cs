using RecipeCrawler.Entities;

namespace RecipeCrawler.ViewModels.ViewModels;

public class IngredientViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public byte Amount { get; set; }
    public MeasurementsEnum Measurement { get; set; }
    public int StepId { get; set; }
    public int RecipeId { get; set; }
}