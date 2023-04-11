using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RecipeCrawler.Data.Database.Contexts;
using RecipeCrawler.Entities;

namespace RecipeCrawler.Data.Repositories.Implementations;

public class RecipeRepository : IRecipeRepository
{
    private readonly ChefferDbContext _context;
    private readonly IMapper _mapper;
    public RecipeRepository(ChefferDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Recipe> SaveRecipe(Recipe recipe)
    {
        await _context.Recipes.AddAsync(recipe);
        await _context.SaveChangesAsync();
        return recipe;
    }

    public IEnumerable<Recipe> GetRecipesByCookbookId(int cookbookId)
    {
        return _context.Recipes
            .AsNoTracking()
            .Where(x => x.CookbookId == cookbookId);
    }

    public async Task<Recipe?> GetRecipeById(int id)
    {
        return await _context.Recipes
            .AsNoTracking()
            .Include(x => x.Steps)
            .ThenInclude(x => x.StepIngredients)
            .Include(x => x.Ingredients)
            .SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<bool> UpdateRecipe(Recipe recipe)
    {
        var dbRecipe = await _context.Recipes.SingleOrDefaultAsync(x => x.Id == recipe.Id);

        _mapper.Map(recipe, dbRecipe);

        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteRecipe(int id)
    {
        var dbRecipe = await _context.Recipes.SingleOrDefaultAsync(x => x.Id == id);
        if (dbRecipe == null)
        {
            return false;
        }
        _context.Remove(dbRecipe);
        return await _context.SaveChangesAsync() > 0;

    }

    public async Task<bool> ChefHasAccessToRecipe(int recipeId, int chefId)
    {
        var recipe = await _context.Recipes
            .Include(x => x.Cookbook)
            .ThenInclude(x => x!.Chef)
            .SingleOrDefaultAsync(x => x.Id == recipeId);

        if (recipe != null)
        {
            return recipe.Cookbook!.Chef!.Id == chefId;
        }

        return false;
    }
}