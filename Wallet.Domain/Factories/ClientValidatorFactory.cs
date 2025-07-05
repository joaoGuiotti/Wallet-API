using Wallet.Domain.Entities;
using Wallet.Domain.Validators;

namespace Wallet.Domain.Factories;
public class ClientValidatorFactory
{
    public static Interfaces.IValidator<Client> Create()
    {
        return new ClientFluentValidator();
    }
}
