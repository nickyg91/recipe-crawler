using RecipeCrawler.Entities;

namespace RecipeCrawler.Data.Repositories;

public interface IRecipeRepository
{
    Task<Recipe> SaveRecipe(Recipe recipe);
    IEnumerable<Recipe> GetRecipesByCookbookId(int cookbookId);
    Task<Recipe?> GetRecipeById(int id);
    Task<bool> UpdateRecipe(Recipe recipe);
    Task<bool> DeleteRecipe(int id);
    Task<bool> ChefHasAccessToRecipe(int recipeId, int chefId);
}