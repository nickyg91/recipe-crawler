namespace RecipeCrawler.Core.Exceptions;

public class CookbookNotFoundException : Exception
{
    public CookbookNotFoundException(string message) : base(message)
    {
        
    }
}