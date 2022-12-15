using Microsoft.AspNetCore.Identity;
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
        public DbSet<MoveEffect> MoveEffects { get; set; }
        public DbSet<LobbyRegistration> Lobbies { get; set; }
        public SiteDataContext(DbContextOptions<SiteDataContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MoveEffect>()
                .HasKey(me => new { me.MoveId, me.EffectId });

            modelBuilder.Entity<MoveEffect>()
                .HasOne(me => me.Move)
                .WithMany(m => m.MoveEffects)
                .HasForeignKey(me => me.MoveId);

            modelBuilder.Entity<MoveEffect>()
                .HasOne(me => me.Effect)
                .WithMany(m => m.MoveEffects)
                .HasForeignKey(me => me.EffectId);

            modelBuilder.Entity<LobbyRegistration>()
                .HasOne(lr => lr.Host)
                .WithOne(u => u.HostLobby)
                .HasForeignKey<User>(u => u.HostId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>()
                .HasOne(u => u.HostLobby)
                .WithOne(lr => lr.Host)
                .HasForeignKey<LobbyRegistration>(lr => lr.HostId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<LobbyRegistration>()
                .HasOne(lr => lr.Guest)
                .WithOne(u => u.GuestLobby)
                .HasForeignKey<User>(u => u.GuestId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>()
                .HasOne(u => u.GuestLobby)
                .WithOne(lr => lr.Guest)
                .HasForeignKey<LobbyRegistration>(lr => lr.GuestId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
