using RecipeCrawler.Entities;
using RecipeCrawler.ViewModels.ViewModels;

namespace RecipeCrawler.Core.Services.Chef.Interfaces;

public interface IChefService
{
    List<CookbookViewModel> GetCookbooksForChef(int chefId);
    Task<bool> DeleteCookbook(int chefId, int cookbookId);
    Task<bool> UpdateCookbook(CookbookViewModel cookbook, int chefId);
    Task<CookbookViewModel?> GetCookbookById(int cookbookId, int chefId);
    Task<CookbookViewModel?> CreateCookbook(CookbookViewModel cookbook, int chefId);
}