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

        public DbSet<Character> FavoriteCharacters { get; set; }
        public DbSet<Thumbnail> Thumbnails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Thumbnail>().HasKey(t => t.Id);
            modelBuilder.Entity<Character>().HasKey(fc => fc.Id);
            modelBuilder.Entity<Character>().Property(fc => fc.Id).ValueGeneratedNever();

            modelBuilder.Entity<Character>()
            .HasOne(fc => fc.Thumbnail)
            .WithOne(t => t.Character)
            .HasForeignKey<Thumbnail>(t => t.Id)
            .OnDelete(DeleteBehavior.Cascade);


            base.OnModelCreating(modelBuilder);
        }
    }
}
