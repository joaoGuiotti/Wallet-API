namespace Wallet.Application.Abstractions;

public abstract class UseCaseBase<TRequest, TResponse>
{
    public abstract Task<TResponse> ExecuteAsync(TRequest request, CancellationToken cancelationToken = default);
}
