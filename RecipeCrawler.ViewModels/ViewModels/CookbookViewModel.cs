using System.Text.Json.Serialization;

namespace RecipeCrawler.ViewModels.ViewModels;

public class CookbookViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<RecipeViewModel>? Recipes { get; set; }
    public string? CoverImageBase64 { get; set; }
}