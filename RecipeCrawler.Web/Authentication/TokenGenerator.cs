using Microsoft.Extensions.Options;
using RecipeCrawler.Core.Services.Accounts;
using RecipeCrawler.Entities;
using RecipeCrawler.Entities.Models;
using RecipeCrawler.Web.Configuration;

namespace RecipeCrawler.Web.Authentication
{
    public class TokenGenerator
    {
        public readonly IAccountService _accountService;
        private readonly JwtSettings _jwtSettings;
        public TokenGenerator(IOptions<JwtSettings> jwtSettings, IAccountService accountService)
        {
            _accountService = accountService;
            _jwtSettings = jwtSettings.Value;
        }

        private async Task<Chef> Authenticate(LoginModel model)
        {
            var chef = await _accountService.Authenticate(model.Email, model.Password);
            return chef;
        }

        public async Task<string> GenerateToken(LoginModel model)
        {
            return "";
        }
    }
}
