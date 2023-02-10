using Microsoft.EntityFrameworkCore;
using RecipeCrawler.Data.Database.Contexts;
using RecipeCrawler.Entities;

namespace RecipeCrawler.Data.Repositories.Implementations;

public class CookbookRepository : ICookbookRepository
{
    private readonly ChefferDbContext _context;

    public CookbookRepository(ChefferDbContext context)
    {
        _context = context;
    }

    public async Task<int> CreateCookbook(Cookbook cookbook)
    {
        var addedCookbook = await _context.Cookbooks.AddAsync(cookbook);
        await _context.SaveChangesAsync();
        return addedCookbook.Entity.Id;
    }

    public IEnumerable<Cookbook> GetCookbooksForChef(int chefId)
    {
        return _context.Cookbooks.Where(x => x.ChefId == chefId);
    }

    public async Task<int> DeleteCookbook(int cookbookId, int chefId)
    {
        var cookbook = await _context.Cookbooks
            .SingleOrDefaultAsync(x => x.ChefId == chefId && x.Id == cookbookId);
        if (cookbook == null)
        {
            return 0;
        }
        _context.Cookbooks.Remove(cookbook);
        return await _context.SaveChangesAsync();
    }

    public async Task<int> UpdateCookbook(Cookbook cookbook)
    {
        throw new NotImplementedException();
    }

    public async Task<int> AddRecipeToCookbook(int cookbookId, Recipe recipe)
    {
        throw new NotImplementedException();
    }
}