namespace Marvel.API.Exceptions
{
    public class MaximumFavoriteCharacterException : Exception
    {
        public MaximumFavoriteCharacterException(string? message) : base(message)
        {
        }
    }
}
