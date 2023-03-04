using System.Security.Claims;

namespace RecipeCrawler.Core.Authentication;

public class AuthenticatedChef : ClaimsPrincipal, IAuthenticatedChef
{
    public AuthenticatedChef(ClaimsPrincipal claimsPrincipal) : base(claimsPrincipal)
    {
        
    }
    
    public int ChefId => int.Parse(Claims.First(x => x.Type == "chefId").Value);
    public string Email => Claims.First(x => x.Type == "email").Value;
    public string Username => Claims.First(x => x.Type == "username").Value;
}