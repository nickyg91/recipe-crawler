namespace RecipeCrawler.Core.Exceptions
{
    public class AccountAlreadyExistsException : Exception
    {
        public AccountAlreadyExistsException(string message) : base(message)
        {

        }
    }
}
