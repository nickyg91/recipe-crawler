using Microsoft.AspNetCore.Mvc;
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
            var url = await _recipeCrawlerService.StoreRecipe(model);
            return Ok(url);
        }

        [HttpGet("{slug:string}")]
        public async Task<IActionResult> Save(string slug)
        {
            var recipe = await _recipeCrawlerService.GetRecipeFromUrl(slug);
            return Ok(recipe);
        }
    }
}