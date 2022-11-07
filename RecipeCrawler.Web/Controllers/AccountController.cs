using Microsoft.AspNetCore.Mvc;
using RecipeCrawler.Core.Services.Accounts;
using RecipeCrawler.Entities;
using RecipeCrawler.Entities.Models;
using RecipeCrawler.Web.Authentication;

namespace RecipeCrawler.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly TokenGenerator _tokenGenerator;
        public AccountController(IAccountService accountService, TokenGenerator tokenGenerator)
        {
            _accountService = accountService;
        }

        [HttpPost("create")]
        public async Task<Chef> CreateAccount(CreateAccountModel model)
        {
            var createdChef = await _accountService.Create(model);
            return createdChef;
        }

        [HttpPost("login")]
        public async Task<JwtTokenModel> Login(LoginModel model)
        {
            var token = await _tokenGenerator.GenerateToken(model);
            return token;
        }
    }
}
