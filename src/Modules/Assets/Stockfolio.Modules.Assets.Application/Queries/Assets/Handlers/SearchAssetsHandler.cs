using Stockfolio.Modules.Assets.Application.Dto.Assets;
using Stockfolio.Modules.Assets.Application.Exceptions;
using Stockfolio.Modules.Assets.Application.Repositories;
using Stockfolio.Shared.Abstractions.Queries;

namespace Stockfolio.Modules.Assets.Application.Queries.Assets.Handlers;

internal sealed class SearchAssetsHandler : IQueryHandler<SearchAssets, SearchAssetsResultDto>
{
    internal static Func<SearchAssets, string> CacheKeyBuilder = (query) => $"{typeof(SearchAssets)}_{query.Query}";
    internal static TimeSpan CacheExpiration = TimeSpan.FromMinutes(5);

    private readonly IStockMarketRepository _quotesRepository;

    public SearchAssetsHandler(IStockMarketRepository stockMarketRepository)
    {
        _quotesRepository = stockMarketRepository;
    }

    public Task<SearchAssetsResultDto> HandleAsync(SearchAssets query, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(query.Query))
            throw new MissingSearchQueryException();

        return _quotesRepository.SearchSecurities(query.Query, query.Count, cancellationToken);
    }
}