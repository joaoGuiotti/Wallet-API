using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wallet.Domain.Entities;

namespace Wallet.Infrastructure.Persistence.Configurations;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable("Transactions");

        builder.HasKey(t => t.Id);
        builder.Property(t => t.AccountFromId)
            .IsRequired();
        builder.Property(t => t.AccountToId)
        .IsRequired();
        builder.Property(t => t.Amount)
            .IsRequired();
        builder.Ignore(t => t.Notification);
        builder.HasOne(t => t.AccountFrom)
            .WithMany()
            .HasForeignKey(t => t.AccountFromId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(t => t.AccountTo)
            .WithMany()
            .HasForeignKey(t => t.AccountToId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
