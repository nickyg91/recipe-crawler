namespace RecipeCrawler.Core.Exceptions;

public class RecipeNotFoundException : Exception
{
    public RecipeNotFoundException(string message) : base(message)
    {
        
    }
}