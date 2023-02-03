namespace RecipeCrawler.Core.Exceptions;

public class VerificationEmailSendException : Exception
{
    public VerificationEmailSendException(string message) : base(message) { }
}