using Microsoft.EntityFrameworkCore;
using Ticketing.Models;

namespace Ticketing.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options)
        : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Event> Events { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => new {u.Email, u.Phone})
                .IsUnique();

            modelBuilder.Entity<Ticket>()
                .HasIndex(t => new { t.Unique_Number });
        }
        public DbSet<Ticketing.Models.Ticket> Ticket { get; set; } = default!;

    }
}
