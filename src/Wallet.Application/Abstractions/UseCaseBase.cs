using MediatR;

namespace Wallet.Application.Abstractions;

public abstract class UseCaseBase<TRequest, TResponse> 
   : IRequestHandler<TRequest, TResponse>  where TRequest : IRequest<TResponse>
{
    public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancelationToken = default);
}
