namespace RecipeCrawler.Entities
{
    public class Step : BaseEntity
    {
        public string? Description { get; set; }
        public int RecipeId { get; set; }
        public Recipe? Recipe { get; set; }
    }
}
