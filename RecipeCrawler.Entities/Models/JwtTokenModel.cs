using System.Security.Claims;

namespace RecipeCrawler.Entities.Models
{
    public class JwtTokenModel
    {
        public string Token { get; set; }
        public List<Claim> Claims { get; set; }
        public string RefreshToken { get; set; }
    }
}
