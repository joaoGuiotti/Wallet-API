
using MediatR;

namespace Wallet.Domain.Interfaces
{
    public interface IDomainEvent : INotification
    {   
         public string AggregateType { get; set; }
        DateTime OccuredAt { get; }

        // get payload
        Object GetPayload();
    }
}
