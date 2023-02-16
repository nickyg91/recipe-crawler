using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RecipeCrawler.Core.Exceptions;
using RecipeCrawler.Core.Services.Chef.Interfaces;

namespace RecipeCrawler.Web.Authorization;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class HasAccessToCookbookAttribute : AuthorizeAttribute, IAsyncAuthorizationFilter
{
    private readonly IChefService _chefService;
    public HasAccessToCookbookAttribute(IChefService chefService)
    {
        _chefService = chefService;
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
            try
            {
                var cookbook = await _chefService.GetCookbookById(cookbookId, chefId);
                if (cookbook?.ChefId != chefId)
                {
                    context.Result = new ForbidResult();
                }
            }
            catch (ChefCookbookAccessViolationException e)
            {
                context.Result = new ForbidResult();
            }
           
        }
    }
}