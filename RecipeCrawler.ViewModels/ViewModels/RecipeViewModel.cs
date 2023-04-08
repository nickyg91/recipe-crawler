using System.ComponentModel.DataAnnotations;

namespace RecipeCrawler.ViewModels.ViewModels;

public class RecipeViewModel
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    public string? CrawledHtml { get; set; }
    public List<StepViewModel>? Steps { get; set; }
    public List<IngredientViewModel>? Ingredients { get; set; }
    public int CookbookId { get; set; }
}