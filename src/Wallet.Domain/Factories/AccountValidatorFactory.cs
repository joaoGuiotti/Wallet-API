
using Wallet.Domain.Entities;
using Wallet.Domain.Validators;

namespace Wallet.Domain.Factories;

public class AccountValidatorFactory
{

    public static Interfaces.IValidator<Account> Create()
    {
        return new AccountValidator();
    }
}
