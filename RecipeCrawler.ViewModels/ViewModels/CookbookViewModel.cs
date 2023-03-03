namespace RecipeCrawler.ViewModels.ViewModels;

public class CookbookViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public byte[]? CoverImage { get; set; }
    public List<RecipeViewModel>? Recipes { get; set; }
    public string CoverImageBase64 => CoverImage != null ? Convert.ToBase64String(CoverImage) : string.Empty;
}