using System;

namespace Stockfolio.Shared.Infrastructure.Vault.Exceptions;

internal sealed class VaultAuthTypeNotSupportedException : Exception
{
    public string AuthType { get; }

    public VaultAuthTypeNotSupportedException(string authType) : this(string.Empty, authType)
    {
    }

    public VaultAuthTypeNotSupportedException(string message, string authType) : base(message)
    {
        AuthType = authType;
    }
}