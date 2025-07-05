
namespace Wallet.Application.UseCases.Client.CreateClient;

using Wallet.Application.Interfaces.Repositories;
using Wallet.Application.UseCases.Client.Common;
using Wallet.Domain.Entities;
using Wallet.Domain.Interfaces;
public class CreateClient : ICreateClient
{
    private readonly IClientRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateClient(IClientRepository repository, IUnitOfWork uow)
    {
        _repository = repository;
        _unitOfWork = uow;
    }

    public async Task<ClientModelOutput> Handle(CreateClientInput request, CancellationToken cancelationToken = default)
    {
        if (await _repository.ExistsByEmailAsync(request.Email, cancelationToken))
        {
            throw new ApplicationException("Email already in use");
        }

        var client = new Client(request.Name, request.Email);

        await _unitOfWork.DoAsync(async uow
            => await _repository.Create(client, cancelationToken),
            cancelationToken);

        return ClientModelOutput.FromClient(client);
    }
}
