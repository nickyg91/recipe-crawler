namespace RecipeCrawler.Core.Exceptions;

public class AccountVerificationException : Exception
{
    public AccountVerificationException(string message) : base(message) { }
}