using RecipeCrawler.Core.Models;

namespace RecipeCrawler.Core.Services
{
    public interface IRecipeCrawlerService
    {
        Task<ParsedHtmlRecipeModel> CrawlUrl(string url);
        Task<string> StoreRecipe(string shortenedUrl, ParsedHtmlRecipeModel recipe);
        Task<ParsedHtmlRecipeModel?> GetRecipeFromUrl(string slugifiedUrl);
        Task<bool> UrlUsed(string url);
        Task<bool> StoreUnscrapeableUrl(ParsedHtmlRecipeModel url);
        Task<List<ParsedHtmlRecipeModel>> GetUnscrapableRecipes(int page, int pageSize);
        Task<long> GetSetLength();
    }
}
