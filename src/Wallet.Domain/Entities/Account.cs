using Wallet.Domain.Factories;
using Wallet.Domain.Shared;

namespace Wallet.Domain.Entities
{
    public class Account : AggregateRoot
    {
        public Client? Client { get; private set; }
        public Guid ClientId { get; private set; }
        public float Balance { get; private set; }
        public float Limit { get; private set; }

        protected Account() : base() { }

        public Account(Client client, float balance) : base()
        {
            Client = client;
            ClientId = client.Id;
            Balance = balance;
            Validate();
        }

        public void ChangeClient(Client client)
        {
            Client = client;
            ClientId = client.Id;
            Validate();
        }

        public void ChangeLimit(float limit)
        {
            Limit = limit;
            Validate();
        }

        public float GetLimit()
        {
            return Limit;
        }

        public float GetNegativeLimit()
        {
            return -GetLimit();
        }

        public void Credit(float amount)
        {
            Balance += amount;
            Validate();
        }

        public void Debit(float amount)
        {
            Balance -= amount;
            Validate();
        }

        public void Validate()
        {
            AccountValidatorFactory.Create().Validate(this);
        }
    }
}
