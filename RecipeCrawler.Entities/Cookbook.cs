namespace RecipeCrawler.Entities
{
    public class Cookbook : BaseEntity
    {
        public string Name { get; set; }
        public Chef Chef { get; set; }
        public int ChefId { get; set; }
        public ICollection<Recipe> Recipes { get; set; }
        public byte[]? CoverImage { get; set; }
    }
}
