
using MediatR;

namespace Wallet.Domain.Interfaces
{
    public interface IDomainEvent : INotification
    {   
        DateTime OccuredAt { get; }

        // get payload
        Object GetPayload();
    }
}
