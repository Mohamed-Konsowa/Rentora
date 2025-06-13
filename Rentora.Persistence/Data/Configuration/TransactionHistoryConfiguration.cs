using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rentora.Domain.Models;

namespace Rentora.Persistence.Data.Configuration
{
    public class TransactionHistoryConfiguration : IEntityTypeConfiguration<TransactionHistory>
    {
        public void Configure(EntityTypeBuilder<TransactionHistory> builder)
        {
            builder.HasOne(t => t.FromUser)
                .WithMany(u => u.FromTransactionHistories)
                .HasForeignKey(t => t.FromUserId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(t => t.ToUser)
                .WithMany(u => u.ToTransactionHistories)
                .HasForeignKey(t => t.ToUserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
