using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using RecipeCrawler.Web.Models;

namespace RecipeCrawler.Web.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CrawlerController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CrawlForRecipe(CrawlerRecipeModel model)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(model.Url, HttpCompletionOption.ResponseContentRead);
                var html = await response.Content.ReadAsStringAsync();
                var htmlParser = new HtmlDocument();
                htmlParser.LoadHtml(html);
                var anyOrderedLists = htmlParser.DocumentNode.SelectNodes("//ol");
                var anyUnorderedLists = htmlParser.DocumentNode.SelectNodes("//ul");
                var text = "";
                if (anyOrderedLists.Any())
                {
                    text += string.Join("", anyOrderedLists.Select(x => x.OuterHtml));
                }
                if (anyUnorderedLists.Any())
                {
                    text += string.Join("", anyUnorderedLists.Select(x => x.OuterHtml));
                }
                
                return Ok(text);
            }
        }
    }
}