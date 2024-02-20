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
            try
            {
                await _context.FavoriteCharacters.AddAsync(favoriteCharacter);
                await _context.SaveChangesAsync();
            }catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            
            return favoriteCharacter;
        }

        public async Task<int> RemoveFromFavorites(Character character)
        {
            _context.Remove(character);
            return await _context.SaveChangesAsync();
            
        }

        public async Task<IEnumerable<Character>> GetFavoriteCharacters()
        {
            return await _context.FavoriteCharacters.AsNoTracking().Include(f => f.Thumbnail).ToListAsync();
        }

        public async Task<Character> GetFavoriteCharacterById(long id)
        {
            return await _context.FavoriteCharacters.Include(f => f.Thumbnail).FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
