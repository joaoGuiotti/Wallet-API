using MediatR;
using Wallet.Application.UseCases.Client.Common;

namespace Wallet.Application.UseCases.Client.CreateClient;

public interface ICreateClient : IRequestHandler<CreateClientInput, ClientModelOutput>
{

}
