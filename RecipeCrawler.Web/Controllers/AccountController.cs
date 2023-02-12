using Microsoft.AspNetCore.Mvc;
using RecipeCrawler.Core.Services.Accounts;
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
            _tokenGenerator = tokenGenerator;
        }

        [HttpPost("create")]
        public async Task<Entities.Chef> CreateAccount(CreateAccountModel model)
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

        [HttpGet("{guid}/verify")]
        public async Task<IActionResult> Verify(Guid guid)
        {
            await _accountService.VerifyAccount(guid);
            return Ok();
        }
    }
}
