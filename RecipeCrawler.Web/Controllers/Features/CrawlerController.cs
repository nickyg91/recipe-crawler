using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using RecipeCrawler.Core.Models;
using RecipeCrawler.Core.Services;

namespace RecipeCrawler.Web.Controllers.Features
{
    [Route("api/features/[controller]")]
    [ApiController]
    public class CrawlerController : ControllerBase
    {
        private readonly IRecipeCrawlerService _recipeCrawlerService;
        public CrawlerController(IRecipeCrawlerService crawlerService)
        {
            _recipeCrawlerService = crawlerService;
        }

        [HttpPost]
        public async Task<IActionResult> CrawlForRecipe(CrawlerRecipeModel model)
        {
            var response = await _recipeCrawlerService.CrawlUrl(model.Url);
            return Ok(response);
        }

        [HttpPost("save")]
        public async Task<IActionResult> Save(ParsedHtmlRecipeModel model)
        {
            var randomInteger = Random.Shared.Next(0, int.MaxValue);
            var shortenedUrl = WebEncoders.Base64UrlEncode(BitConverter.GetBytes(randomInteger));

            var exists = await _recipeCrawlerService.UrlUsed(shortenedUrl);

            while (exists)
            {
                randomInteger = Random.Shared.Next(0, int.MaxValue);
                shortenedUrl = WebEncoders.Base64UrlEncode(BitConverter.GetBytes(randomInteger));
                exists = await _recipeCrawlerService.UrlUsed(shortenedUrl);
            }

            await _recipeCrawlerService.StoreRecipe(shortenedUrl, model);
            return Ok(shortenedUrl);
        }

        [HttpGet("{shortenedUrl}")]
        public async Task<IActionResult> GetRecipe(string shortenedUrl)
        {
            var recipe = await _recipeCrawlerService.GetRecipeFromUrl(shortenedUrl);
            return Ok(recipe);
        }

        [HttpPost("report")]
        public async Task<IActionResult> ReportUrlUnscrapeable(ParsedHtmlRecipeModel url)
        {
            var response = await _recipeCrawlerService.StoreUnscrapeableUrl(url);
            return Ok(response);
        }

        [HttpGet("recipes/reported/{page:int}/page/{pageSize:int}")]
        public async Task<IActionResult> GetReportedUrls(int page, int pageSize)
        {
            var totalItems = await _recipeCrawlerService.GetSetLength();
            var items = await _recipeCrawlerService.GetUnscrapableRecipes(page, pageSize);

            return Ok(new PagedRecipeModel
            {
                Recipes = items,
                TotalItems = totalItems,
            });
        }
    }
}