using Wallet.Domain.Factories;
using Wallet.Domain.Shared;

namespace Wallet.Domain.Entities { 
    public class Client : AggregateRoot
    {
        public string Name { get;  set; } = null!;
        public string Email { get; set; } = null!;
        public ICollection<Account> Accounts { get; private set; } = new List<Account>();

        public Client()
        {
            
        }

        public Client(string name, string email) : base()
        {
            Name = name;
            Email = email; 
            Validate();
        }

        public void AddAccount(Account account)
        {
            if(!account.ClientId.Equals(Id))
                Notification.AddError(nameof(Client), "Account does not belong to client");
            Accounts.Add(account);
            Validate();
        }

        public void RemoveAccount(Account account)
        {
            Accounts.Remove(account);
            Validate();
        }

        public void ChangeName(string name)
        {
            Name = name;
            Validate();
        }

        public void ChangeEmail(string email)
        {
            Email = email;
            Validate();
        }

        public void Validate()
        {
            ClientValidatorFactory.Create().Validate(this);
        }
    }
}