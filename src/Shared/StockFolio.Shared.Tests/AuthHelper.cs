namespace Stockfolio.Shared.Tests;

//public static class AuthHelper
//{
//    private static readonly ClaimsProvider AuthManager;

//    static AuthHelper()
//    {
//        var options = OptionsHelper.GetOptions<AuthOptions>("auth");
//        AuthManager = new ClaimsProvider(options, new UtcClock());
//    }

//    public static string GenerateJwt(Guid userId, IEnumerable<string> roles = null, string audience = null,
//        IDictionary<string, IEnumerable<string>> claims = null)
//        => AuthManager.CreateToken(userId, roles, audience, claims).AccessToken;
//}