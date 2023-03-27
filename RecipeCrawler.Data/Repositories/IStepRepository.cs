using RecipeCrawler.Entities;

namespace RecipeCrawler.Data.Repositories;

public interface IStepRepository
{
    IEnumerable<Step> GetStepsForRecipe(int recipeId);
    Task<IEnumerable<Step>> UpdateStepsForRecipe(int recipeId, List<Step> stepsToUpdate);
    Task<IEnumerable<Step>> DeleteStepsForRecipe(int recipeId, List<Step> stepsToDelete);
}