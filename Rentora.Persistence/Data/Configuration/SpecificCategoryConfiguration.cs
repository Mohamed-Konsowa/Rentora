using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rentora.Domain.Models.Categories;

namespace Rentora.Persistence.Data.Configuration
{
    public class SpecificCategoryConfiguration : IEntityTypeConfiguration<Sport>
                                               , IEntityTypeConfiguration<Transportation>
                                               , IEntityTypeConfiguration<Travel>
                                               , IEntityTypeConfiguration<Electronic>
    {
        public void Configure(EntityTypeBuilder<Sport> builder)
        {
            builder.HasOne(x => x.Product)
                .WithMany()
                .OnDelete(DeleteBehavior.Cascade);
        }

        public void Configure(EntityTypeBuilder<Transportation> builder)
        {
            builder.HasOne(x => x.Product)
                .WithMany()
                .OnDelete(DeleteBehavior.Cascade);
        }

        public void Configure(EntityTypeBuilder<Travel> builder)
        {
            builder.HasOne(x => x.Product)
                .WithMany()
                .OnDelete(DeleteBehavior.Cascade);
        }

        public void Configure(EntityTypeBuilder<Electronic> builder)
        {
            builder.HasOne(x => x.Product)
                .WithMany()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

