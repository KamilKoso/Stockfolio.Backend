using Microsoft.Extensions.Logging;
using Stockfolio.Shared.Abstractions.Queries;
using Stockfolio.Shared.Infrastructure.Cache;
using Stockfolio.Shared.Infrastructure.Serialization;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Stockfolio.Shared.Infrastructure.Queries.Decorators;

[Decorator]
internal class CachedQueryHandlerDecorator<TQuery, TResult> : IQueryHandler<TQuery, TResult>
    where TQuery : class, IQuery<TResult>
{
    private readonly IQueryHandler<TQuery, TResult> _decorated;
    private readonly ICache _cache;
    private readonly ILogger<CachedQueryHandlerDecorator<TQuery, TResult>> _logger;
    private readonly Func<TQuery, TimeSpan?> _expirationBuilder;
    private readonly Func<TQuery, string> _generateKey;

    public CachedQueryHandlerDecorator(IQueryHandler<TQuery, TResult> decorated,
                                       ICache cache,
                                       IJsonSerializer jsonSerializer,
                                       ILogger<CachedQueryHandlerDecorator<TQuery, TResult>> logger,
                                       Func<TQuery, string> generateKey = null,
                                       Func<TQuery, TimeSpan?> expirationBuilder = null)
    {
        _decorated = decorated;
        _cache = cache;
        _logger = logger;
        _expirationBuilder = expirationBuilder;
        _generateKey = generateKey ?? (query => $"{nameof(query)}_{jsonSerializer.Serialize(query)}");
    }

    public async Task<TResult> HandleAsync(TQuery query, CancellationToken cancellationToken = default)
    {
        var cacheKey = _generateKey(query);
        var cached = await _cache.GetAsync<TResult>(cacheKey);
        if (cached is not null)
        {
            _logger.LogInformation("Returning result for {query} query from the cache.", typeof(TQuery).Name);
            return cached;
        }

        var result = await _decorated.HandleAsync(query, cancellationToken);
        await _cache.SetAsync(cacheKey, result, _expirationBuilder(query));
        return result;
    }
}