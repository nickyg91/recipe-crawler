using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeCrawler.Core.Authentication;
using RecipeCrawler.Core.Services.Chef.Interfaces;
using RecipeCrawler.Entities;
using RecipeCrawler.ViewModels.ViewModels;
using RecipeCrawler.Web.Authorization;

namespace RecipeCrawler.Web.Controllers.Chef
{
    [Route("api/chef/[controller]")]
    [ApiController]
    [Authorize]
    public class CookbookController : ControllerBase
    {
        private readonly IChefService _chefService;
        private readonly IAuthenticatedChef _authenticatedChef;
        public CookbookController(IChefService chefService, IAuthenticatedChef authenticatedChef)
        {
            _chefService = chefService;
            _authenticatedChef = authenticatedChef;
        }

        [HttpGet]
        public List<CookbookViewModel> GetCookbooksByChefId()
        {
            return _chefService.GetCookbooksForChef(_authenticatedChef.ChefId);
        }

        [HttpGet("{id}"), ServiceFilter(typeof(HasAccessToCookbookAttribute))]
        public async Task<CookbookViewModel?> GetCookbookById(int id)
        {
            return await _chefService.GetCookbookById(id, _authenticatedChef.ChefId);
        }

        [HttpPost]
        public async Task<CookbookViewModel?> CreateCookbook(CookbookViewModel cookbook)
        {
            return await _chefService.CreateCookbook(cookbook, _authenticatedChef.ChefId);
        }

        [HttpPut("{id}"), ServiceFilter(typeof(HasAccessToCookbookAttribute))]
        public async Task<bool> UpdateCookbook(int id, [FromBody] CookbookViewModel cookbook)
        {
            return await _chefService.UpdateCookbook(cookbook, _authenticatedChef.ChefId);
        }

        [HttpDelete("{id}"), ServiceFilter(typeof(HasAccessToCookbookAttribute))]
        public async Task<bool> DeleteCookbook(int id)
        {
            return await _chefService.DeleteCookbook(_authenticatedChef.ChefId, id);
        }
    }
}
