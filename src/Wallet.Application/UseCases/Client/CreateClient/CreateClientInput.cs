using MediatR;
using Wallet.Application.UseCases.Client.Common;

namespace Wallet.Application.UseCases.Client.CreateClient;

public sealed record CreateClientInput(string Name, string Email) : IRequest<ClientModelOutput>; 
