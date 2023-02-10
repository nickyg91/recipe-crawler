using RecipeCrawler.Entities;

namespace RecipeCrawler.Data.Repositories;

public interface ICookbookRepository
{
    Task<int> CreateCookbook(Cookbook cookbook);
    IEnumerable<Cookbook> GetCookbooksForChef(int chefId);
    Task<int> DeleteCookbook(int cookbookId, int chefId);
    Task<int> UpdateCookbook(Cookbook cookbook);
    Task<int> AddRecipeToCookbook(int cookbookId, Recipe recipe);
}