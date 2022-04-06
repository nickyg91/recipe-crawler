namespace RecipeCrawler.Web 
{
    public class ParsedHtmlRecipeModel
    {
        public List<string>? Ingredients { get; set; }
        public List<string>? Steps { get; set; }
        public string? Url { get; set; }
    }
}