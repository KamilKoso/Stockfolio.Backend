using Stockfolio.Shared.Infrastructure.Auth;
using Stockfolio.Shared.Infrastructure.Time;
using System;
using System.Collections.Generic;

namespace Stockfolio.Shared.Tests;

public static class AuthHelper
{
    private static readonly JwtAccessTokenProvider AuthManager;

    static AuthHelper()
    {
        var options = OptionsHelper.GetOptions<AuthOptions>("auth");
        AuthManager = new JwtAccessTokenProvider(options, new UtcClock());
    }

    public static string GenerateJwt(Guid userId, IEnumerable<string> roles = null, string audience = null,
        IDictionary<string, IEnumerable<string>> claims = null)
        => AuthManager.CreateToken(userId, roles, audience, claims).AccessToken;
}