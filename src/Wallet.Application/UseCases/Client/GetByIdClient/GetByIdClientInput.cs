using MediatR;
using Wallet.Application.UseCases.Client.Common;

namespace Wallet.Application.UseCases.Client.GetByIdClient;

public record GetByIdClientInput(Guid ClientId) : IRequest<ClientModelOutput>;
