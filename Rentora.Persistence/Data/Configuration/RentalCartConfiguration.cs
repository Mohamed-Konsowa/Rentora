using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rentora.Domain.Models;

namespace Rentora.Persistence.Data.Configuration
{
    public class RentalCartConfiguration : IEntityTypeConfiguration<RentalCart>
    {
        public void Configure(EntityTypeBuilder<RentalCart> builder)
        {
            builder.HasOne(r => r.ApplicationUser)
                .WithMany(u => u.RentalCarts)
                .HasForeignKey(r => r.ApplicationUserId);

            builder.HasIndex(c => new { c.ApplicationUserId, c.ProductId })
                .IsUnique();
        }
    }
}
