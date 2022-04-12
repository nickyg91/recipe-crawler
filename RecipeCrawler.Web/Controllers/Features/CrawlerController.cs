using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using RecipeCrawler.Core.Models;
using RecipeCrawler.Core.Services;
using System.Text;

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
            var bytes = Encoding.UTF8.GetBytes(model.Url);
            var shortenedUrl = WebEncoders.Base64UrlEncode(bytes);
            await _recipeCrawlerService.StoreRecipe(shortenedUrl, model);
            return Ok(shortenedUrl);
        }

        [HttpGet("find/{slug}")]
        public async Task<IActionResult> GetRecipe(string slug)
        {
            var recipe = await _recipeCrawlerService.GetRecipeFromUrl(slug);
            return Ok(recipe);
        }
    }
}