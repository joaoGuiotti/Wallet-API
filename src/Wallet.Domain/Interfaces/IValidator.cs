using Wallet.Domain.Shared;

namespace Wallet.Domain.Interfaces
{
    public interface IValidator<T> where T : Entity
    {
        void Validate(T entity);
    }
}
