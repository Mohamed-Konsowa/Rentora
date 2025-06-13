using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Rentora.Domain.Models;
using Rentora.Domain.Models.Categories;
using System.Reflection;

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
        public DbSet<OTP> OTPs { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Sport> sports { get; set; }
        public DbSet<Transportation> Transportations { get; set; }
        public DbSet<Travel> Travels { get; set; }
        public DbSet<Electronic> Electronics { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }
    }
}
