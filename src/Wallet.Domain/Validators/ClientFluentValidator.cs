using FluentValidation;
using FluentValidation.Results;
using Wallet.Domain.Entities;
using Wallet.Domain.Shared;

namespace Wallet.Domain.Validators
{
    public class ClientFluentValidator : Interfaces.IValidator<Client>
    {
        private readonly ClientValidator _validator = new ClientValidator();

        public void Validate(Client entity)
        {
            ValidationResult result = _validator.Validate(entity);

            if (!result.IsValid)
            {
                foreach (var failure in result.Errors)
                {
                    entity.Notification.AddError(nameof(Client), failure.ErrorMessage);
                }
            }

            if (entity.Notification.HasErrors())
                throw new NotificationException(entity.Notification.GetMessagesNormalized());
        }
    }

    public class ClientValidator : AbstractValidator<Client>
    {
        public ClientValidator()
        {
            RuleFor(c => c.Id).NotEmpty().WithMessage("Id is required");
            RuleFor(c => c.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(c => c.Email).NotEmpty().WithMessage("Email is required")
                                 .EmailAddress().WithMessage("Email is invalid"); 
        }
    }
}
