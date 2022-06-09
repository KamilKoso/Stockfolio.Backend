using System;
using System.Collections.Generic;

namespace Stockfolio.Shared.Abstractions.Auth;

public interface IJwtProvider
{
    JsonWebToken CreateToken(Guid userId, IEnumerable<string> roles = null, string audience = null,
        IDictionary<string, IEnumerable<string>> claims = null);
}