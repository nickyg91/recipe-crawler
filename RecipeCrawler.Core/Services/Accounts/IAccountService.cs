using RecipeCrawler.Entities;
using RecipeCrawler.Entities.Models;

namespace RecipeCrawler.Core.Services.Accounts
{
    public interface IAccountService
    {
        Task<Chef> Create(CreateAccountModel model);
        Task ChangePassword(string oldPassword, string newPassword);
        Task ResetPassword(string newPassword);
    }
}
