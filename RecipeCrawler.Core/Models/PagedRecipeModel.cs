namespace RecipeCrawler.Core.Models
{
    public class PagedRecipeModel
    {
        public long TotalItems { get; set; }
        public IAsyncEnumerable<ParsedHtmlRecipeModel>? Recipes { get; set; }
    }
}
