using Stockfolio.Shared.Abstractions;
using Stockfolio.Shared.Abstractions.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Stockfolio.Shared.Infrastructure.Contexts;

public class IdentityContext : IIdentityContext
{
    public bool IsAuthenticated { get; }
    public Guid? Id { get; }
    public string Email { get; }
    public string Role { get; }

    public Dictionary<string, IEnumerable<string>> Claims { get; }

    private IdentityContext()
    {
    }

    public IdentityContext(Guid? id)
    {
        Id = id;
        IsAuthenticated = id.HasValue;
    }

    public IdentityContext(ClaimsPrincipal principal)
    {
        var userId = principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        if (principal?.Identity is null || userId.IsNullOrWhiteSpace())
        {
            return;
        }

        IsAuthenticated = principal.Identity?.IsAuthenticated is true;
        Id = IsAuthenticated ? Guid.Parse(userId) : Guid.Empty;
        Email = principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
        Role = principal.Claims.SingleOrDefault(x => x.Type == ClaimTypes.Role)?.Value;
        Claims = principal.Claims.GroupBy(x => x.Type)
            .ToDictionary(x => x.Key, x => x.Select(c => c.Value.ToString()));
    }

    public static IIdentityContext Empty => new IdentityContext();
}