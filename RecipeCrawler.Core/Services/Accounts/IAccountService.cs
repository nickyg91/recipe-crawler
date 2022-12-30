using RecipeCrawler.Entities;
using RecipeCrawler.Entities.Models;

namespace RecipeCrawler.Core.Services.Accounts
{
    public interface IAccountService
    {
        Task<Chef> Create(CreateAccountModel model);
        Task ChangePassword(int chefId, string oldPassword, string newPassword);
        Task ResetPassword(int chefId, string newPassword);
        Task<Chef> Authenticate(string username, string password);
    }
}
