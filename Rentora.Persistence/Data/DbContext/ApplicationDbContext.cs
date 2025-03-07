using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
//using System.Composition;
using System.Reflection.Emit;
using Rentora.Domain.Models;
namespace Rentora.Persistence.Data.DbContext
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<RentalCart> RentalCarts { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<TransactionHistory> TransactionHistories { get; set; }
        public DbSet<ActivityLog> ActivityLogs { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Report>()
                .HasOne(r => r.ReporterUser)
                .WithMany(u => u.Reports)
                .HasForeignKey(r => r.ReporterUserId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.Entity<Report>()
                .HasOne(r => r.ReportedUser)
                .WithMany(u => u.Reporteds)
                .HasForeignKey(r => r.ReportedUserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<TransactionHistory>()
                .HasOne(t => t.FromUser)
                .WithMany(u => u.FromTransactionHistories)
                .HasForeignKey(t => t.FromUserId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.Entity<TransactionHistory>()
                .HasOne(t => t.ToUser)
                .WithMany(u => u.ToTransactionHistories)
                .HasForeignKey(t => t.ToUserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<RentalCart>()
                .HasOne(r => r.ApplicationUser)
                .WithMany(u => u.RentalCarts)
                .HasForeignKey(r => r.ApplicationUserId);

            builder.Entity<Product>().ToTable("Product");
        }
    }
}
