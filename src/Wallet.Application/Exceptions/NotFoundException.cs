using System;
using Wallet.Domain.Shared;

namespace Wallet.Application.Exceptions;

public class NotFoundException : ApplicationException
{
    public NotFoundException(string? message) : base(message)
    { }

    public static void ThrowIfNull(
        Entity? @object,
        string exceptionMessage
    ) {
        if (@object is null)
                throw new NotFoundException(exceptionMessage);
    }
}
