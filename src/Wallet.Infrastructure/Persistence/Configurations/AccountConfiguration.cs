using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wallet.Domain.Entities;

namespace Wallet.Infrastructure.Persistence.Configurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("Accounts");

            builder.HasKey(x => x.Id);
            builder.Property(a => a.Balance)
                .IsRequired();
            builder.Property(a => a.Limit)
               .IsRequired()
               .HasDefaultValue(0);
            builder.Property(a => a.ClientId)
                .IsRequired();
            builder.Property(a => a.CreatedAt)
               .IsRequired();
            builder.Ignore(a => a.Notification);
            builder.HasOne(a => a.Client)
                   .WithMany(c => c.Accounts)
                   .HasForeignKey(a => a.ClientId);
        }
    }
}
