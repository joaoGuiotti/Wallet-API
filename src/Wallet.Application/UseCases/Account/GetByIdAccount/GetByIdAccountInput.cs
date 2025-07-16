using System;
using MediatR;
using Wallet.Application.UseCases.Account.Common;

namespace Wallet.Application.UseCases.Account.GetByIdAccount;

public record GetByIdAccountInput(Guid accountId) : IRequest<AccountModelOutput>;
