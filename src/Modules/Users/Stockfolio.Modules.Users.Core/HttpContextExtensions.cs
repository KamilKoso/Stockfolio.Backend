using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Stockfolio.Modules.Users.Core.Constants;
using Stockfolio.Modules.Users.Core.Entities;
using System.Security.Claims;

namespace Stockfolio.Modules.Users.Core;

internal static class HttpContextExtensions
{
    public static async Task SignInAsStockfolioUser(this HttpContext httpContext, User user)
    {
        List<Claim> claims = new()
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Email, user.Email),
            new(CustomClaims.EmailConfirmed, user.EmailConfirmed.ToString()),
        };

        foreach (var role in user.UserRoles.Select(x => x.Role.Name))
        {
            claims.Add(new(ClaimTypes.Role, role));
        }

        ClaimsIdentity claimsIdentity = new(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        ClaimsPrincipal claimsPrincipal = new(claimsIdentity);

        await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, new() { IsPersistent = true });
    }

    public static async Task SignOutAsStockfolioUser(this HttpContext httpContext)
        => await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
}