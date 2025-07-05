using Wallet.Domain.Entities;
using Wallet.Domain.Validators;

namespace Wallet.Domain.Factories;

public class TransactionValidatorFactory 
{
     public static Interfaces.IValidator<Transaction> Create()
    {
        return new TransactionFluentValidator();
    }
}
