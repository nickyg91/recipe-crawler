using RecipeCrawler.Core.Models;

namespace RecipeCrawler.Core.Services
{
    public interface IRecipeCrawlerService
    {
        Task<ParsedHtmlRecipeModel> CrawlUrl(string url);
        Task StoreRecipe(string shortenedUrl, ParsedHtmlRecipeModel recipe);
        Task<ParsedHtmlRecipeModel?> GetRecipeFromUrl(string slugifiedUrl);
    }
}
