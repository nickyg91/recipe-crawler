using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeCrawler.Core.Authentication;
using RecipeCrawler.Core.Services.Recipe.Interfaces;
using RecipeCrawler.ViewModels.ViewModels;
using RecipeCrawler.Web.Authorization;

namespace RecipeCrawler.Web.Controllers.Cookbook;

[Route("api/cookbook/{cookbookId:int}"), Authorize, TypeFilter(typeof(HasAccessToRecipeAttribute))]
public class RecipeController : ControllerBase
{
    private readonly IRecipeService _recipeService;
    private readonly IAuthenticatedChef _authenticatedChef;

    public RecipeController(IRecipeService recipeService, IAuthenticatedChef authenticatedChef)
    {
        _recipeService = recipeService;
        _authenticatedChef = authenticatedChef;
    }

    [HttpGet("recipes")]
    public List<RecipeViewModel> GetRecipesByCookbookId(int cookbookId)
    {
        return _recipeService.GetRecipesByCookbookId(_authenticatedChef.ChefId, cookbookId);
    }

    [HttpGet("recipe/{recipeId:int}")]
    public async Task<RecipeViewModel> GetRecipeById(int recipeId)
    {
        return await _recipeService.GetRecipeById(recipeId, _authenticatedChef.ChefId);
    }

    [HttpPut("recipe/{recipeId:int}")]
    public async Task<RecipeViewModel?> UpdateRecipe(int recipeId, RecipeViewModel recipe)
    {
        var isUpdated = await _recipeService.UpdateRecipe(_authenticatedChef.ChefId, recipe);
        if (isUpdated)
        {
            return await _recipeService.GetRecipeById(recipeId, _authenticatedChef.ChefId);
        }

        return null;
    }

    [HttpPost("recipe")]
    public async Task<RecipeViewModel> SaveRecipe(RecipeViewModel recipe)
    {
        return await _recipeService.SaveRecipe(recipe);
    }

    [HttpDelete("recipe/{recipeId:int}")]
    public async Task<bool> DeleteRecipe(int recipeId)
    {
        return await _recipeService.DeleteRecipe(_authenticatedChef.ChefId, recipeId);
    }
}