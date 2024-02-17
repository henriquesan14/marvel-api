using Marvel.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Marvel.API.Infra.Repositories
{
    public class MarvelRepository : IMarvelRepository
    {
        private readonly MarvelDbContext _context;

        public MarvelRepository(MarvelDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddFavoriteCharacter(FavoriteCharacter favoriteCharacter)
        {
            int result = 0;
            var character = await _context.FavoriteCharacters.FirstOrDefaultAsync(c => c.Id == favoriteCharacter.Id);
            if (character == null)
            {
                await _context.FavoriteCharacters.AddAsync(favoriteCharacter);
                result = await _context.SaveChangesAsync();
            }
            return result;
        }

        public async Task<int> RemoveFromFavorites(int characterId)
        {
            int result = 0;
            var character = await _context.FavoriteCharacters.FirstOrDefaultAsync(c => c.Id == characterId);
            if (character != null)
            {
                _context.Remove(character);
                await _context.SaveChangesAsync();
            }
            return result;
        }

        public async Task<IEnumerable<FavoriteCharacter>> GetFavoriteCharacters()
        {
            return await _context.FavoriteCharacters.AsNoTracking().ToListAsync();
        }
    }
}
