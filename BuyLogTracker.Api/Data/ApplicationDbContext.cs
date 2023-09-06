using BuyLogTracker.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace BuyLogTracker.Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<PurchaseHistory> PurchaseHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasIndex(u => new { u.Name, u.Phone })
                .IsUnique();
        }
    }
}
