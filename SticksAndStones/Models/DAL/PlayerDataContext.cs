using Microsoft.EntityFrameworkCore;

namespace SticksAndStones.Models.DAL
{
    public class PlayerDataContext : DbContext
    {
        public PlayerDataContext(DbContextOptions<PlayerDataContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                    new User
                    {
                        UserID = 1,
                        UserName = "jP",
                        GamesPlayed = 10,
                        GamesWon = 6,
                        IsActive = false
                    },
                    new User
                    {
                        UserID = 2,
                        UserName = "Max",
                        GamesPlayed = 10,
                        GamesWon = 4,
                        IsActive = false
                    }
                );
        }
    }
}
