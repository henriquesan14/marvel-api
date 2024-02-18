namespace Marvel.API.Exceptions
{
    public class AlreadyFavoriteCharacterException : Exception
    {
        public AlreadyFavoriteCharacterException(string? message) : base(message)
        {
        }
    }
}
