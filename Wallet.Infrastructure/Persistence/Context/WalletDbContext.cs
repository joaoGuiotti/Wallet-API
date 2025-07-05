using Microsoft.EntityFrameworkCore;
using Wallet.Domain.Entities;
using Wallet.Domain.Shared;
using Wallet.Infrastructure.Persistence.Configurations;

namespace Wallet.Infrastructure.Persistence.Context
{
    public class WalletDbContext : DbContext
    {
        public WalletDbContext(DbContextOptions<WalletDbContext> options) : base(options) { }

        public DbSet<Client> Clients => Set<Client>();
        public DbSet<Account> Accounts => Set<Account>();
        public DbSet<Transaction> Transactions => Set<Transaction>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<Notification>();
            modelBuilder.ApplyConfiguration(new ClientConfiguration());
            modelBuilder.ApplyConfiguration(new AccountConfiguration());
            modelBuilder.ApplyConfiguration(new TransactionConfiguration());

            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(WalletDbContext).Assembly);
            SetInitialData(modelBuilder);
        }

        private void SetInitialData(ModelBuilder modelBuilder)
        {
            // Seeds para Client
            var client1 = new Client("Cliente 1", "cliente1@email.com");
            var client2 = new Client("Cliente 2", "cliente2@email.com");

            modelBuilder.Entity<Client>().HasData(
                new { Id = client1.Id, Name = client1.Name, Email = client1.Email, CreatedAt = DateTimeOffset.UtcNow },
                new { Id = client2.Id, Name = client2.Name, Email = client2.Email, CreatedAt = DateTimeOffset.UtcNow }
            );

            // Seeds para Account
            var account1 = new Account(client1, 1000)
            {
                Id = new Guid("00000000-0000-0000-0000-000000000001")
            };
            var account2 = new Account(client2, 500)
            {
                Id = new Guid("00000000-0000-0000-0000-000000000002")
            };

            modelBuilder.Entity<Account>().HasData(
                new { Id = account1.Id, ClientId = client1.Id, Balance = account1.Balance, CreatedAt = DateTimeOffset.UtcNow },
                new { Id = account2.Id, ClientId = client2.Id, Balance = account2.Balance, CreatedAt = DateTimeOffset.UtcNow }
            );

            // Seeds para Transaction (opcional, se quiser transações iniciais)
            var transaction1 = new Transaction(account1, account2, 100);

            modelBuilder.Entity<Transaction>().HasData(
                new
                {
                    Id = transaction1.Id,
                    AccountFromId = account1.Id,
                    AccountToId = account2.Id,
                    Amount = transaction1.Amount,
                    CreatedAt = DateTimeOffset.UtcNow
                }
            );
        }
    }
}
