using RecipeCrawler.Entities;
using RecipeCrawler.Entities.Models;

namespace RecipeCrawler.Core.Services.Accounts
{
    public interface IAccountService
    {
        Task<Entities.Chef> Create(CreateAccountModel model);
        Task ChangePassword(int chefId, string oldPassword, string newPassword);
        Task ResetPassword(int chefId, string newPassword);
        Task VerifyAccount(Guid verificationGuid);
        Task<Entities.Chef?> Authenticate(string username, string password);
        Task SendNewVerificationEmail(Guid oldGuid);
    }
}
