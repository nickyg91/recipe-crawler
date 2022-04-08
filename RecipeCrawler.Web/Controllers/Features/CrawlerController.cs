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
            using (var provider = System.Security.Cryptography.SHA512.Create())
            {
                var urlBytes = System.Text.Encoding.GetEncoding(0).GetBytes(model.Url);
                var hashedString = provider.ComputeHash(urlBytes);
                // store it in redis
                return Ok(hashedString);
            }
        }
    }
}