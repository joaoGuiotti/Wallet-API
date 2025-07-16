using Wallet.Domain.Common;

namespace Wallet.Application.Events.Transaction
{
    public class TransactionCreatedIntegrationEvent : IntegrationEvent
    {
        public Guid TransactionId { get; set; }
        public Guid AccountFromId { get; set; }
        public Guid AccountToId { get; set; }

        public TransactionCreatedIntegrationEvent(
            Guid transactionId,
            Guid accountFromId,
            Guid accountToId
        ) : base()  
        {
            TransactionId = transactionId;
            AccountFromId = accountFromId;
            AccountToId = accountToId;
        }

        public TransactionCreatedIntegrationEvent() { }
    }
}
