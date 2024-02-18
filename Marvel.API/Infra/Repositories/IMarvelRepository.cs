using Marvel.API.Entities;

namespace Marvel.API.Infra.Repositories
{
    public interface IMarvelRepository
    {
        Task<Character> AddFavoriteCharacter(Character favoriteCharacter);
        Task<int> RemoveFromFavorites(int characterId);
        Task<IEnumerable<Character>> GetFavoriteCharacters();
    }
}
