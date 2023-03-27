using RecipeCrawler.ViewModels.Enums;

namespace RecipeCrawler.ViewModels.ViewModels;

public class IngredientViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public byte Amount { get; set; }
    public MeasurementEnum Measurement { get; set; }
    public int StepId { get; set; }
}