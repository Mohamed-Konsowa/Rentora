using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rentora.Domain.Models;

namespace Rentora.Persistence.Data.Configuration
{
    public class ReportConfiguration : IEntityTypeConfiguration<Report>
    {
        public void Configure(EntityTypeBuilder<Report> builder)
        {
            builder.HasOne(r => r.ReporterUser)
                .WithMany(u => u.Reports)
                .HasForeignKey(r => r.ReporterUserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(r => r.ReportedUser)
                .WithMany(u => u.Reporteds)
                .HasForeignKey(r => r.ReportedUserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}

