using HtmlAgilityPack;
using RecipeCrawler.Core.Models;

namespace RecipeCrawler.Core.Services
{
    public class RecipeCrawlerService : IRecipeCrawlerService
    {
        public async Task<ParsedHtmlRecipeModel> CrawlUrl(string url)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(url, HttpCompletionOption.ResponseContentRead);
                var html = await response.Content.ReadAsStringAsync();
                var htmlParser = new HtmlDocument();
                htmlParser.LoadHtml(html);
                var anyOrderedListsWithIngredients = htmlParser.DocumentNode.SelectNodes("//ol[contains(@class, 'ingredients')]");
                var anyUnorderedListsWithIngredients = htmlParser.DocumentNode.SelectNodes("//ul[contains(@class, 'ingredients')]");
                var anyDivTagsWithIngredients = htmlParser.DocumentNode.SelectNodes("//div[contains(@class, 'ingredients')]");
                var anyDivTagsWithSteps = htmlParser.DocumentNode.SelectNodes("//div[contains(@class, 'instructions')]|//section[contains(@class, 'steps')]");
                var anyUlWithSteps = htmlParser.DocumentNode.SelectNodes("//ul[contains(@class, 'instructions')]|//ul[contains(@class, 'directions')]");
                var anyOlWithSteps = htmlParser.DocumentNode.SelectNodes("//ol[contains(@class, 'instructions')]|//ol[contains(@class, 'directions')]");
                var listOfIngredientSections = new List<string>();
                var listOfDirectionSections = new List<string>();
                if (anyOrderedListsWithIngredients != null && anyOrderedListsWithIngredients.Any())
                {
                    listOfIngredientSections.AddRange(anyOrderedListsWithIngredients.Select(x => x.OuterHtml));
                }
                if (anyUnorderedListsWithIngredients != null && anyUnorderedListsWithIngredients.Any())
                {
                    listOfIngredientSections.AddRange(anyUnorderedListsWithIngredients.Select(x => x.OuterHtml));
                }
                if (anyDivTagsWithIngredients != null && anyDivTagsWithIngredients.Any())
                {
                    listOfIngredientSections.AddRange(anyDivTagsWithIngredients.Select(x => x.OuterHtml));
                }
                if (anyDivTagsWithSteps != null && anyDivTagsWithSteps.Any())
                {
                    listOfDirectionSections.AddRange(anyDivTagsWithSteps.Select(x => x.OuterHtml));
                }
                if (anyOlWithSteps != null && anyOlWithSteps.Any())
                {
                    listOfDirectionSections.AddRange(anyOlWithSteps.Select(x => x.OuterHtml));
                }
                if (anyUlWithSteps != null && anyUlWithSteps.Any())
                {
                    listOfDirectionSections.AddRange(anyUlWithSteps.Select(x => x.OuterHtml));
                }
                var parsedResponse = new ParsedHtmlRecipeModel
                {
                    Ingredients = listOfIngredientSections,
                    Steps = listOfDirectionSections,
                };
                return parsedResponse;
            }
        }
    }
}
