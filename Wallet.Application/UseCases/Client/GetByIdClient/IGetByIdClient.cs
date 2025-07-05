using MediatR;
using Wallet.Application.UseCases.Client.Common;

namespace Wallet.Application.UseCases.Client.GetByIdClient;

public interface IGetByIdClient : IRequestHandler<GetByIdClientInput, ClientModelOutput>
{

}
