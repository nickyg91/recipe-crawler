using Microsoft.AspNetCore.Mvc;
using RecipeCrawler.Core.Services.Accounts;
using RecipeCrawler.Entities;
using RecipeCrawler.Entities.Models;

namespace RecipeCrawler.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("create")]
        public async Task<Chef> CreateAccount(CreateAccountModel model)
        {
            var createdChef = await _accountService.Create(model);
            return createdChef;
        }
    }
}
