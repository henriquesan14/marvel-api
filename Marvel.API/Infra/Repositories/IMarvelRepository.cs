using Marvel.API.Models;

namespace Marvel.API.Infra.Repositories
{
    public interface IMarvelRepository
    {
        Task<int> AddFavoriteCharacter(FavoriteCharacter favoriteCharacter);
        Task<int> RemoveFromFavorites(int characterId);
        Task<IEnumerable<FavoriteCharacter>> GetFavoriteCharacters();
    }
}
