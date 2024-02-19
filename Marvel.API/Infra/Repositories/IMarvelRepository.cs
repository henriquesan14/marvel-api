using Marvel.API.Entities;

namespace Marvel.API.Infra.Repositories
{
    public interface IMarvelRepository
    {
        Task<Character> AddFavoriteCharacter(Character favoriteCharacter);
        Task<int> RemoveFromFavorites(Character character);
        Task<IEnumerable<Character>> GetFavoriteCharacters();
        Task<Character> GetFavoriteCharacterById(long id);
    }
}
