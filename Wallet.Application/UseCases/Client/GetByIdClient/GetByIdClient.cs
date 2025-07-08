

using Wallet.Application.Abstractions;
using Wallet.Application.Interfaces.Repositories;
using Wallet.Application.UseCases.Client.Common;

namespace Wallet.Application.UseCases.Client.GetByIdClient;

public class GetByIdClient : UseCaseBase<GetByIdClientInput, ClientModelOutput>
{
    private readonly IClientRepository _repository;

    public GetByIdClient(IClientRepository repository)
    {
        _repository = repository;
    }

    public override async Task<ClientModelOutput> Handle(GetByIdClientInput request, CancellationToken cancelationToken)
    {
        var client = await _repository.Find(request.ClientId, cancelationToken);
        return ClientModelOutput.FromClient(client!);
    }
}
