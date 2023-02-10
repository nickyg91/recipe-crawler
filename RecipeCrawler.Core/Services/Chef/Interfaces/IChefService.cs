using RecipeCrawler.Entities;

namespace RecipeCrawler.Core.Services.Chef.Interfaces;

public interface IChefService
{
    Task<List<Cookbook>> GetCookbooksForChef(int chefId);
    Task<bool> DeleteCookbook(int chefId, int cookbookId);
    //Task<
}