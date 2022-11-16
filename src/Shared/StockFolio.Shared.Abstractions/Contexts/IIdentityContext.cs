using System;
using System.Collections.Generic;

namespace Stockfolio.Shared.Abstractions.Contexts;

public interface IIdentityContext
{
    bool IsAuthenticated { get; }
    Guid Id { get; }
    string Email { get; }
    string Role { get; }
    Dictionary<string, IEnumerable<string>> Claims { get; }
    bool IsUser();
    bool IsAdmin();
}