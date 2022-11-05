using Microsoft.EntityFrameworkCore;

namespace SticksAndStones.Models.DAL
{
    public class PlayerDataContext : DbContext
    {
        public PlayerDataContext(DbContextOptions<PlayerDataContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Leaderboard> Leaderboard { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                    new User
                    {
                        UserID = 1,
                        Username = "jP",
                        GamesPlayed = 10,
                        GamesWon = 6,
                        IsActive = false
                    },
                    new User
                    {
                        UserID = 2,
                        Username = "Max",
                        GamesPlayed = 10,
                        GamesWon = 4,
                        IsActive = false
                    }
                );
            modelBuilder.Entity<Leaderboard>().HasData(
                    new Leaderboard
                    {
                        ID = 1,
                        UserID = 1,
                        Ranking = 1,
                        Date = System.DateTime.Now
                    },
                    new Leaderboard
                    {
                        ID = 2,
                        UserID = 2,
                        Ranking = 1,
                        Date = System.DateTime.Now
                    }
                );
        }
    }
}
