using System;
using MediatR;
using Wallet.Application.UseCases.Account.Common;

namespace Wallet.Application.UseCases.Account.CreateAccount;

public interface ICreateAccount : IRequestHandler<CreateAccountInput, AccountModelOutput>
{

}
