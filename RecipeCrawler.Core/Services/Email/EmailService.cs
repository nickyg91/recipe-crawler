using System.Net.Mail;
using RecipeCrawler.Core.Configuration;
using RecipeCrawler.Core.Exceptions;

namespace RecipeCrawler.Core.Services.Email;

public class EmailService : IEmailService
{
    private readonly EmailConfigurationOptions _emailConfigurationOptions;
    private readonly string _environmentUrl;
    public EmailService(EmailConfigurationOptions emailConfigurationOptions, string environmentUrl)
    {
        _emailConfigurationOptions = emailConfigurationOptions;
        _environmentUrl = environmentUrl;
    }
    
    public async Task SendVerificationEmail(string to, Guid verificationGuid)
    {
        try
        {
            var body = 
            @$"
                Please click the following link to verify your Cheffer account!
                {_environmentUrl}/verify/{verificationGuid.ToString()}
                This URL will expire within 3 days of your account creation.
                To request another one, please log in and request verification.
            ";
            var email = new MailMessage("cheffer@no-reply.com", to, "Verify your Cheffer Account!" ,body);
            
            using var smtpClient = new SmtpClient(_emailConfigurationOptions.SmtpUrl, _emailConfigurationOptions.Port);
            await smtpClient.SendMailAsync(email);
        }
        catch (Exception e)
        {
            throw new VerificationEmailSendException(e.Message);
        }
    }
}