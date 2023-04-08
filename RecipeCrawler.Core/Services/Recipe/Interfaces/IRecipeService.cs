using RecipeCrawler.ViewModels.ViewModels;

namespace RecipeCrawler.Core.Services.Recipe.Interfaces;

public interface IRecipeService
{
    Task<RecipeViewModel> SaveRecipe(RecipeViewModel recipe);
    List<RecipeViewModel> GetRecipesByCookbookId(int chefId, int cookbookId);
    Task<RecipeViewModel> GetRecipeById(int recipeId, int chefId);
    Task<bool> UpdateRecipe(int chefId, RecipeViewModel recipe);
    Task<bool> DeleteRecipe(int chefId, int recipeId);
}