namespace RecipeCrawler.Core.Exceptions;

public class AccountNotVerifiedException : Exception
{
    public AccountNotVerifiedException(string message) : base(message)
    {
        
    }
}