using Wallet.Application.Abstractions;
using Wallet.Application.Interfaces.Repositories;
using Wallet.Application.UseCases.Account.Common;
using Wallet.Domain.Interfaces;

namespace Wallet.Application.UseCases.Account.CreateAccount
{
    public class CreateAccount : ICreateAccount
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateAccount(
            IAccountRepository accountRepository,
            IClientRepository clientRepository,
            IUnitOfWork unitOfWork
        )
        {
            _accountRepository = accountRepository;
            _clientRepository = clientRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<AccountModelOutput> Handle(CreateAccountInput request, CancellationToken cancelationToken = default)
        {
            var client = await _clientRepository.Find(request.clientId, cancelationToken);
            if (client == null)
                throw new ApplicationException("Client not found");

            var account = new Domain.Entities.Account(client, request.balance);

            await _unitOfWork.DoAsync(async uow =>
            {
                await _accountRepository.Create(account, cancelationToken);
            }, cancelationToken);

            return AccountModelOutput.FromAccount(account);
        }
    }
}
