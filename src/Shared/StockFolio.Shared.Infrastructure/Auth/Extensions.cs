using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Stockfolio.Shared.Abstractions.Modules;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stockfolio.Shared.Infrastructure.Auth;

public static class Extensions
{
    private const string AccessTokenCookieName = "__access-token";

    public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration, IList<IModule> modules = null)
    {
        var authOptions = configuration.GetOptions<AuthOptions>("auth");
        if (authOptions.AuthenticationDisabled)
        {
            services.AddSingleton<IPolicyEvaluator, DisabledAuthenticationPolicyEvaluator>();
        }

        var policies = modules?.SelectMany(x => x.Policies ?? Enumerable.Empty<string>()) ??
                      Enumerable.Empty<string>();

        services
            .AddAuthorization(authorization =>
            {
                authorization.DefaultPolicy = new AuthorizationPolicyBuilder()
                                             .AddAuthenticationSchemes(CookieAuthenticationDefaults.AuthenticationScheme)
                                             .RequireAuthenticatedUser()
                                             .Build();

                foreach (var policy in policies)
                {
                    authorization.AddPolicy(policy, x => x.AddAuthenticationSchemes(CookieAuthenticationDefaults.AuthenticationScheme)
                                                          .RequireClaim("permissions", policy));
                }
            })
            .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.Cookie.Name = AccessTokenCookieName;
                options.Cookie.HttpOnly = authOptions.AccessCookie.HttpOnly;
                options.Cookie.SameSite = authOptions.AccessCookie.SameSite?.ToLowerInvariant() switch
                {
                    "strict" => SameSiteMode.Strict,
                    "lax" => SameSiteMode.Lax,
                    "none" => SameSiteMode.None,
                    "unspecified" => SameSiteMode.Unspecified,
                    _ => SameSiteMode.Unspecified
                };
                options.ExpireTimeSpan = authOptions.AccessCookie.Expiration;
                options.SlidingExpiration = authOptions.AccessCookie.SlidingExpiration;
                options.Events = new()
                {
                    OnRedirectToLogin = (ctx) =>
                    {
                        ctx.Response.StatusCode = 401;
                        return Task.CompletedTask;
                    },
                    OnRedirectToAccessDenied = (ctx) =>
                    {
                        ctx.Response.StatusCode = 403;
                        return Task.CompletedTask;
                    },
                };
            });

        return services;
    }

    public static IApplicationBuilder UseAuth(this IApplicationBuilder app)
    {
        app.UseAuthentication();
        return app;
    }
}