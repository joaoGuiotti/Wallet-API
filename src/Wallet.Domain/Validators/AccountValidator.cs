using Wallet.Domain.Entities;
using Wallet.Domain.Shared;

namespace Wallet.Domain.Validators;

public class AccountValidator : Interfaces.IValidator<Account>
{
    public void Validate(Account entity)
    {
        if (entity.Id.ToString() is null)
            entity.Notification.AddError(nameof(Transaction), "Id is required."); 
           
        if (entity.Notification.HasErrors())
            throw new NotificationException(entity.Notification.GetMessagesNormalized());
    }
}