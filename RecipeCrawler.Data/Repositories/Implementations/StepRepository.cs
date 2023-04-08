using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RecipeCrawler.Data.Database.Contexts;
using RecipeCrawler.Entities;

namespace RecipeCrawler.Data.Repositories.Implementations;

public class StepRepository : IStepRepository
{
    private readonly ChefferDbContext _ctx;
    private readonly IMapper _mapper;
    public StepRepository(ChefferDbContext ctx, IMapper mapper)
    {
        _ctx = ctx;
        _mapper = mapper;
    }
    
    public IEnumerable<Step> GetStepsForRecipe(int recipeId)
    {
        return _ctx.Steps
            .Where(x => x.RecipeId == recipeId)
            .OrderBy(x => x.Order)
            .AsNoTracking();
    }

    public async Task<IEnumerable<Step>> UpdateStepsForRecipe(int recipeId, List<Step> stepsToUpdate)
    {
        var stepIds = stepsToUpdate.Select(x => x.Id);
        var steps = _ctx.Steps.Where(x => stepIds.Contains(x.Id) && x.RecipeId == recipeId);

        _mapper.Map(stepsToUpdate, steps);

        await _ctx.SaveChangesAsync();

        return _ctx.Steps.Where(x => x.RecipeId == recipeId).AsNoTracking();
    }

    public async Task<IEnumerable<Step>> DeleteStepsForRecipe(int recipeId, List<Step> stepsToDelete)
    {
        var steps = _ctx.Steps.Where(x => stepsToDelete.Select(y => y.Id).Contains(x.Id) && x.RecipeId == recipeId);
        _ctx.Steps.RemoveRange(steps);
        await _ctx.SaveChangesAsync();
        return _ctx.Steps.Where(x => x.RecipeId == recipeId).AsNoTracking();
    }
}