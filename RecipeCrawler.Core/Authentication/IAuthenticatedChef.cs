using System.Security.Principal;

namespace RecipeCrawler.Core.Authentication;

public interface IAuthenticatedChef : IPrincipal
{
    int ChefId { get; }
    string Email { get; }
    string Username { get; }
}