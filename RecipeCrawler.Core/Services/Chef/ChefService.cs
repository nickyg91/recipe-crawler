using RecipeCrawler.Core.Exceptions;
using RecipeCrawler.Core.Services.Chef.Interfaces;
using RecipeCrawler.Data.Repositories;
using RecipeCrawler.Entities;

namespace RecipeCrawler.Core.Services.Chef;

public class ChefService : IChefService
{
    private readonly ICookbookRepository _cookbookRepository;
    public ChefService(ICookbookRepository cookbookRepository)
    {
        _cookbookRepository = cookbookRepository;
    }
    public List<Cookbook> GetCookbooksForChef(int chefId)
    {
        return _cookbookRepository.GetCookbooksForChef(chefId).ToList();
    }

    public async Task<bool> DeleteCookbook(int chefId, int cookbookId)
    {
        var cookbook = await _cookbookRepository.GetCookbookById(cookbookId);

        if (cookbook != null && cookbook.ChefId != chefId)
        {
            throw new ChefCookbookAccessViolationException("This chef does not have access to this cookbook.");
        }

        var result = await _cookbookRepository.DeleteCookbook(cookbookId, chefId);
        return result > 0;
    }

    public async Task<bool> UpdateCookbook(Cookbook cookbook, int chefId)
    {
        var dbCookbook = await _cookbookRepository.GetCookbookById(cookbook.Id);

        if (dbCookbook != null && cookbook.ChefId != chefId)
        {
            throw new ChefCookbookAccessViolationException("This chef does not have access to this cookbook.");
        }

        var results = await _cookbookRepository.UpdateCookbook(cookbook);
        
        return results > 0;
    }
}