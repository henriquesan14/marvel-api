using Marvel.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Marvel.API.Infra
{
    public class MarvelDbContext : DbContext
    {
        public MarvelDbContext(DbContextOptions<MarvelDbContext> options)
        : base(options)
        {
        }

        public DbSet<FavoriteCharacter> FavoriteCharacters { get; set; }
    }
}
