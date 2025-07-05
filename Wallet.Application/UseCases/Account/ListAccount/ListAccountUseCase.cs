using AutoMapper;
using Wallet.Application.Interfaces.Repositories;
using Wallet.Application.UseCases.Account.Common;
using Wallet.Domain.Entities;

namespace Wallet.Application.UseCases.Account.GetAllAccount
{
    public class ListAccountUseCase
    {
        public readonly IAccountRepository _accountRepository;
        public readonly IClientRepository _clientRepository;

        public ListAccountUseCase(
            IAccountRepository accountrepository,
            IClientRepository clientRepository
        )
        {
            _accountRepository = accountrepository;
            _clientRepository = clientRepository;
        }

        //public async Task<IEnumerable<AccountModelOutput>> ExecuteAsync(ListAccountInput input)
        //{
        //    //var searchOutput = await _accountRepository.FindAll();
        //    //IEnumerable<AccountModelOutput> output = searchOutput.;

        //    //var accounts = await _accountRepository.FindAll();
        //    //var result  =  accounts.Select(acc => _mapper.Map<GetAllAccountResult>(acc)).ToList();
        //    //return result;
        //}
    }
}
