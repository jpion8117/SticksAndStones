using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SticksAndStones.Models.DAL
{
    public class SiteDataContext : IdentityDbContext<User>
    {
        public DbSet<Character> Characters { get; set; }
        public DbSet<Tagline> Taglines { get; set; }
        public DbSet<Move> Moves { get; set; }
        public DbSet<Effect> Effects { get; set; }
        public SiteDataContext(DbContextOptions<SiteDataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MoveEffect>().HasKey(me => new { me.MoveId, me.EffectId });

            modelBuilder.Entity<MoveEffect>()
                .HasOne(me => me.Move)
                .WithMany(m => m.MoveEffects)
                .HasForeignKey(me => me.MoveId);

            modelBuilder.Entity<MoveEffect>()
                .HasOne(me => me.Effect)
                .WithMany(m => m.MoveEffects)
                .HasForeignKey(me => me.EffectId);
        }
    }
}
