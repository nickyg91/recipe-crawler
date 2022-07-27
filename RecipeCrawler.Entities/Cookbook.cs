namespace RecipeCrawler.Entities
{
    public class Cookbook
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Chef Chef { get; set; }
    }
}
