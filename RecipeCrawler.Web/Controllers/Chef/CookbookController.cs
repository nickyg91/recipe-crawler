using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeCrawler.Core.Authentication;
using RecipeCrawler.Core.Services.Chef.Interfaces;
using RecipeCrawler.Entities;
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
        public List<Cookbook> GetCookbooksByChefId()
        {
            return _chefService.GetCookbooksForChef(_authenticatedChef.ChefId);
        }

        [HttpGet("{id}"), ServiceFilter(typeof(HasAccessToCookbookAttribute))]
        public async Task<Cookbook?> GetCookbookById(int id)
        {
            return await _chefService.GetCookbookById(id, _authenticatedChef.ChefId);
        }

        [HttpPost]
        public async Task<Cookbook?> CreateCookbook(Cookbook cookbook)
        {
            cookbook.ChefId = _authenticatedChef.ChefId;
            return await _chefService.CreateCookbook(cookbook);
        }

        [HttpPut("{id}"), ServiceFilter(typeof(HasAccessToCookbookAttribute))]
        public async Task<bool> UpdateCookbook(int id, [FromBody] Cookbook cookbook)
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
