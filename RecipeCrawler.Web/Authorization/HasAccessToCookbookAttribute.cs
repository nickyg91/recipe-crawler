using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RecipeCrawler.Core.Exceptions;
using RecipeCrawler.Data.Repositories;

namespace RecipeCrawler.Web.Authorization;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class HasAccessToCookbookAttribute : AuthorizeAttribute, IAsyncAuthorizationFilter
{
    private readonly ICookbookRepository _cookbookRepository;
    public HasAccessToCookbookAttribute(ICookbookRepository cookbookRepository)
    {
        _cookbookRepository = cookbookRepository;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var cookbookIdString = context.RouteData.Values["id"]?.ToString();
        var chefIdString = context.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "chefId")?.Value;
        if (string.IsNullOrEmpty(chefIdString))
        {
            context.Result = new ForbidResult();
            return;
        }
        if (string.IsNullOrEmpty(cookbookIdString))
        {
            context.Result = new NotFoundResult();
            return;
        }
        
        var hasChefId =
            int.TryParse(chefIdString, out var chefId);
        var hasCookbookId = 
            int.TryParse(cookbookIdString, out var cookbookId);
        if (!hasChefId && !hasCookbookId)
        {
            context.Result = new NotFoundResult();
        }
        else
        {
            var cookbook = await _cookbookRepository.GetCookbookById(cookbookId);
            if (cookbook?.ChefId != chefId)
            {
                context.Result = new ForbidResult();
            }
        }
    }
}