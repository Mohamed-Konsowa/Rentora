using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rentora.Domain.Models;

namespace Rentora.Persistence.Data.Configuration
{
    public class FavoriteConfiguration : IEntityTypeConfiguration<Favorite>
    {
        public void Configure(EntityTypeBuilder<Favorite> builder)
        {
            builder.HasIndex(f => new { f.ApplicationUserId, f.ProductId })
                .IsUnique();
        }
    }
}
