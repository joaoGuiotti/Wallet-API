using Wallet.Domain.Events.Transaction;
using Wallet.Domain.Factories;
using Wallet.Domain.Shared;

namespace Wallet.Domain.Entities
{
    public class Transaction : AggregateRoot
    {
        public Account AccountFrom { get; private set; }
        public Guid AccountFromId { get; private set; }
        public Account AccountTo { get; private set; }
        public Guid AccountToId { get; private set; }
        public float Amount { get; private set; }

        protected Transaction() : base()
        { }

        public Transaction(Account accountFrom, Account accountTo, float ammount) : base()
        {
            AccountFrom = accountFrom;
            AccountFromId = accountFrom.Id;
            AccountTo = accountTo;
            AccountToId = accountTo.Id;
            Amount = ammount;

            Validate();
        }

        public void Commit()
        {
            AccountFrom.Debit(Amount);
            AccountTo.Credit(Amount);

            ApplyEvent(new TransactionCreatedEvent(Id, AccountFromId, AccountToId));
        }

        public void Validate()
        {
            TransactionValidatorFactory.Create().Validate(this);
        }
    }
}
