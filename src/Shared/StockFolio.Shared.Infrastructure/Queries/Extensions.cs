using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Stockfolio.Shared.Abstractions.Queries;
using Stockfolio.Shared.Infrastructure.Cache;
using Stockfolio.Shared.Infrastructure.Queries.Decorators;
using Stockfolio.Shared.Infrastructure.Serialization;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Stockfolio.Shared.Infrastructure.Queries;

public static class Extensions
{
    public static IServiceCollection AddQueries(this IServiceCollection services, IEnumerable<Assembly> assemblies)
    {
        services.AddSingleton<IQueryDispatcher, QueryDispatcher>();
        services.Scan(s => s.FromAssemblies(assemblies)
                            .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>))
                                              .WithoutAttribute<DecoratorAttribute>())
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        return services;
    }

    public static IServiceCollection AddPagedQueryDecorator(this IServiceCollection services)
    {
        services.TryDecorate(typeof(IQueryHandler<,>), typeof(PagedQueryHandlerDecorator<,>));

        return services;
    }

    public static IServiceCollection AddCachedQueryDecorator<TQuery, TResult>(this IServiceCollection services,
                                                                              Func<TQuery, string> cacheKeyBuilder = null,
                                                                              TimeSpan? expiration = null)
        where TQuery : class, IQuery<TResult>
        where TResult : class
    {
        return services.AddCachedQueryDecorator<TQuery, TResult>(cacheKeyBuilder, (_) => expiration);
    }

    public static IServiceCollection AddCachedQueryDecorator<TQuery, TResult>(this IServiceCollection services,
                                                                              Func<TQuery, string> cacheKeyBuilder = null,
                                                                              Func<TQuery, TimeSpan?> expirationBuilder = null)
        where TQuery : class, IQuery<TResult>
        where TResult : class
    {
        services.TryDecorate<IQueryHandler<TQuery, TResult>>
           ((inner, sp) => new CachedQueryHandlerDecorator<TQuery, TResult>(inner,
                                                                            sp.GetRequiredService<ICache>(),
                                                                            sp.GetRequiredService<IJsonSerializer>(),
                                                                            sp.GetRequiredService<ILogger<CachedQueryHandlerDecorator<TQuery, TResult>>>(),
                                                                            cacheKeyBuilder,
                                                                            expirationBuilder));

        return services;
    }
}