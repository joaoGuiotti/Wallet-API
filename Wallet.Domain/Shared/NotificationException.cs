using System;

namespace Wallet.Domain.Shared;

public class NotificationException : Exception
{
    public NotificationException(string message) : base(message)
    {  
    }
}
