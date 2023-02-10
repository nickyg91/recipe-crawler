namespace RecipeCrawler.Core.Exceptions;

public class AccountNotFoundFromVerificationEmailException : Exception
{
    public AccountNotFoundFromVerificationEmailException(string message) : base(message)
    {
        
    }
}