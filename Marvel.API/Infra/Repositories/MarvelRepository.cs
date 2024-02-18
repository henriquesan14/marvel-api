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

        public async Task<Character> AddFavoriteCharacter(Character favoriteCharacter)
        {
            var character = await _context.FavoriteCharacters.FirstOrDefaultAsync(c => c.Id == favoriteCharacter.Id);
            if (character == null)
            {
                await _context.FavoriteCharacters.AddAsync(favoriteCharacter);
                await _context.SaveChangesAsync();
                return favoriteCharacter;
            }
            return null!;
        }

        public async Task<int> RemoveFromFavorites(int characterId)
        {
            int result = 0;
            var character = await _context.FavoriteCharacters.FirstOrDefaultAsync(c => c.Id == characterId);
            if (character != null)
            {
                _context.Remove(character);
                result = await _context.SaveChangesAsync();
            }
            return result;
        }

        public async Task<IEnumerable<Character>> GetFavoriteCharacters()
        {
            IEnumerable<Character> result = null;
            try
            {
                result = await _context.FavoriteCharacters.AsNoTracking().Include(f => f.Thumbnail).ToListAsync();
            }catch(Exception e)
            {
                Console.WriteLine(e);
            }
            return result;
        }
    }
}
