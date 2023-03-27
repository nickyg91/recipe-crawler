namespace RecipeCrawler.Entities
{
    public class Ingredient : BaseEntity
    {
        public string Name { get; set; }
        public MeasurementsEnum Measurement { get; set; }
        public byte Amount { get; set; }
        public int StepId { get; set; }
        public Step Step { get; set; }
    }
}
