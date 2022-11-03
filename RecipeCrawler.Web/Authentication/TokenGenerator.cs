using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RecipeCrawler.Core.Exceptions;
using RecipeCrawler.Core.Services.Accounts;
using RecipeCrawler.Entities;
using RecipeCrawler.Entities.Models;
using RecipeCrawler.Web.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RecipeCrawler.Web.Authentication
{
    public class TokenGenerator
    {
        public readonly IAccountService _accountService;
        private readonly JwtSettingsOptions _jwtSettings;
        public TokenGenerator(IOptions<JwtSettingsOptions> jwtSettings, IAccountService accountService)
        {
            _accountService = accountService;
            _jwtSettings = jwtSettings.Value;
        }

        private async Task<Chef> Authenticate(LoginModel model)
        {
            var chef = await _accountService.Authenticate(model.Email, model.Password);
            return chef;
        }

        public async Task<JwtTokenModel> GenerateToken(LoginModel model)
        {
            var chef = await Authenticate(model);
            if (chef == null)
            {
                throw new AuthenticationException("Account not found.");
            }

            var key = Encoding.UTF8.GetBytes(_jwtSettings.Key);

            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature);
            var jwtToken = new JwtSecurityToken(
                _jwtSettings.Issuer,
                _jwtSettings.Audience, 
                new List<Claim>
                {
                    new Claim("chefId", chef.Id.ToString()),
                },
                null,
                DateTime.UtcNow.AddMinutes(30),
                signingCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            var refreshToken = Guid.NewGuid();

            return new JwtTokenModel
            {
                Token = tokenString,
                Claims = jwtToken.Claims.ToList(),
                RefreshToken = refreshToken.ToString(),
            };
        }
    }
}
