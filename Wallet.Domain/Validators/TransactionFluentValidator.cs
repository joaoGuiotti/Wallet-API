using FluentValidation;
using Wallet.Domain.Entities;
using Wallet.Domain.Shared;

namespace Wallet.Domain.Validators;

public class TransactionFluentValidator : Interfaces.IValidator<Transaction>
{
    public void Validate(Transaction entity)
    {
        if (entity.Id.ToString() is null)
            entity.Notification.AddError(nameof(Transaction), "Id is required.");
        if (entity.AccountFrom is null)
            entity.Notification.AddError(nameof(Transaction), "AccountFrom is required.");
        if (entity.AccountFromId.ToString() is null)
            entity.Notification.AddError(nameof(Transaction), "AccountId is required.");
        if (entity.Amount <= 0)
            entity.Notification.AddError(nameof(Transaction), "Amount must be greater than zero.");
        if (entity.Notification.HasErrors())
            throw new NotificationException(entity.Notification.GetMessagesNormalized());
    }
}
