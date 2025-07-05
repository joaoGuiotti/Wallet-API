using Wallet.Domain.Events;
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
            RegisterHandler<TransactionCreatedEvent>(OnTransactionCreated);

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
            
            ApplyEvent(new TransactionCreatedEvent(Id));
        }

        public void Validate()
        {
            TransactionValidatorFactory.Create().Validate(this);
        }

        private void OnTransactionCreated(TransactionCreatedEvent @event)
        {
            Console.WriteLine($" =========================> Transaction {@event.TransactionId} was created and handled locally.");
        }
    }
}
