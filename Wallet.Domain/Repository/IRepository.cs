
using Wallet.Domain.Shared;

namespace Wallet.Domain.Repository
{
    public interface IRepository<T>
        where T : AggregateRoot
    {
        Task Create(T entity, CancellationToken cancelationToken);
        Task Update(T entity, CancellationToken cancelationToken);
        Task Delete(T entity, CancellationToken cancelationToken);
        Task<T?> Find(Guid id, CancellationToken cancelationToken);
    }

    public interface ISearchableRepository<TAggregate>
        where TAggregate : AggregateRoot
    {
        Task<SearchOutput<TAggregate>> SearchAsync(
            SearchInput input,
            CancellationToken cancellationToken
        );
    }
}
