namespace RecipeCrawler.Core.Models
{
    public class PagedRecipeModel
    {
        public long TotalItems { get; set; }
        public List<ParsedHtmlRecipeModel>? Recipes { get; set; }
    }
}
