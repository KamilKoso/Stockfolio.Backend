using System;

namespace Stockfolio.Shared.Infrastructure.Auth;

public class AuthOptions
{
    public bool AuthenticationDisabled { get; set; }
    public CookieOptions AccessCookie { get; set; }

    public class CookieOptions
    {
        public bool HttpOnly { get; set; }
        public bool Secure { get; set; }
        public string SameSite { get; set; }
        public TimeSpan Expiration { get; set; }
        public bool SlidingExpiration { get; set; }
    }
}