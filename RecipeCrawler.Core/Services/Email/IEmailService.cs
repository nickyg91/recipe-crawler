using System.Net.Mail;

namespace RecipeCrawler.Core.Services.Email;

public interface IEmailService
{
    Task SendVerificationEmail(string to, Guid verificationGuid);
}