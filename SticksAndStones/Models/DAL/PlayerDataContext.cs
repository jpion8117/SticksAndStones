using Microsoft.EntityFrameworkCore;

namespace SticksAndStones.Models.DAL
{
    public class PlayerDataContext : DbContext
    {
        public PlayerDataContext(DbContextOptions<PlayerDataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}
