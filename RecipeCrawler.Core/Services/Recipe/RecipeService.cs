using AutoMapper;
using RecipeCrawler.Core.Exceptions;
using RecipeCrawler.Core.Services.Recipe.Interfaces;
using RecipeCrawler.Data.Repositories;
using RecipeCrawler.Entities;
using RecipeCrawler.ViewModels.ViewModels;

namespace RecipeCrawler.Core.Services.Recipe;

public class RecipeService : IRecipeService
{
    private readonly IRecipeRepository _recipeRepository;
    private readonly IMapper _mapper;

    public RecipeService(
        IRecipeRepository recipeRepository,
        IMapper mapper)
    {
        _recipeRepository = recipeRepository;
        _mapper = mapper;
    }

    public async Task<RecipeViewModel> SaveRecipe(RecipeViewModel recipe)
    {
        var dbRecipe = _mapper.Map<RecipeViewModel, Entities.Recipe>(recipe);

        var savedRecipe = await _recipeRepository.SaveRecipe(dbRecipe);
        return _mapper.Map<Entities.Recipe, RecipeViewModel>(savedRecipe);
    }

    public List<RecipeViewModel> GetRecipesByCookbookId(int chefId, int cookbookId)
    {
        var recipes = _recipeRepository.GetRecipesByCookbookId(cookbookId).ToList();
        var mappedRecipes = _mapper.Map<List<Entities.Recipe>, List<RecipeViewModel>>(recipes);
        return mappedRecipes;
    }

    public async Task<RecipeViewModel> GetRecipeById(int recipeId, int chefId)
    {
        var hasAccessToRecipe = await _recipeRepository.ChefHasAccessToRecipe(recipeId, chefId);
        if (!hasAccessToRecipe)
        {
            throw new ChefRecipeAccessViolationException("Chef does not have access to this recipe!");
        }

        var recipe = await _recipeRepository.GetRecipeById(recipeId);
        if (recipe == null)
        {
            throw new RecipeNotFoundException("Recipe not found!");
        }

        var mappedEntity = _mapper.Map<Entities.Recipe, RecipeViewModel>(recipe);
        return mappedEntity;
    }

    public async Task<bool> UpdateRecipe(int chefId, RecipeViewModel recipe)
    {
        var hasAccessToRecipe = await _recipeRepository.ChefHasAccessToRecipe(recipe.Id, chefId);
        if (!hasAccessToRecipe)
        {
            throw new ChefRecipeAccessViolationException("Chef does not have access to this recipe!");
        }

        var dbRecipe = await _recipeRepository.GetRecipeById(recipe.Id);
        _mapper.Map(recipe, dbRecipe);
        return await _recipeRepository.UpdateRecipe(dbRecipe!);
    }

    public async Task<bool> DeleteRecipe(int chefId, int recipeId)
    {
        var hasAccessToRecipe = await _recipeRepository.ChefHasAccessToRecipe(recipeId, chefId);
        if (!hasAccessToRecipe)
        {
            throw new ChefRecipeAccessViolationException("Chef does not have access to this recipe!");
        }

        return await _recipeRepository.DeleteRecipe(recipeId);
    }
}