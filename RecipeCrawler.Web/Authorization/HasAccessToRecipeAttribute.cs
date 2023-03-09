using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RecipeCrawler.Core.Authentication;
using RecipeCrawler.Data.Repositories;

namespace RecipeCrawler.Web.Authorization;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class HasAccessToRecipeAttribute : AuthorizeAttribute, IAsyncAuthorizationFilter
{
    private readonly IRecipeRepository _recipeRepository;
    private readonly IAuthenticatedChef _authenticatedChef;

    public HasAccessToRecipeAttribute(IRecipeRepository recipeRepository, IAuthenticatedChef authenticatedChef)
    {
        _recipeRepository = recipeRepository;
        _authenticatedChef = authenticatedChef;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var recipeIdString = context.RouteData.Values["recipeId"]?.ToString();

        if (!string.IsNullOrEmpty(recipeIdString))
        {
            int.TryParse(recipeIdString, out var recipeId);
            var recipe = await _recipeRepository.GetRecipeById(recipeId);
            if (recipe == null)
            {
                context.Result = new NotFoundResult();
            }
            else
            {
                if (recipe.Cookbook != null && recipe.Cookbook.ChefId != _authenticatedChef.ChefId)
                {
                    context.Result = new ForbidResult();
                }
            }
        }
    }
}