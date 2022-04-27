namespace RecipeCrawler.Core.Models 
{
    public class ParsedHtmlRecipeModel
    {
        public List<string>? Ingredients { get; set; }
        public List<string>? Steps { get; set; }
        public string? Url { get; set; }
        public string? Title { get; set; }
    }
}