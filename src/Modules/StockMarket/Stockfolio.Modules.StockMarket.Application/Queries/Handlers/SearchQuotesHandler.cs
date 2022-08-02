using Stockfolio.Modules.StockMarket.Application.DTO;
using Stockfolio.Modules.StockMarket.Application.Exceptions;
using Stockfolio.Modules.StockMarket.Application.Repositories;
using Stockfolio.Shared.Abstractions.Queries;

namespace Stockfolio.Modules.StockMarket.Application.Queries.Handlers;

internal class SearchQuotesHandler : IQueryHandler<SearchQuotes, SearchQuotesDto>
{
    private readonly IQuotesRepository _quotesRepository;

    public SearchQuotesHandler(IQuotesRepository quotesRepository)
    {
        _quotesRepository = quotesRepository;
    }

    public Task<SearchQuotesDto> HandleAsync(SearchQuotes query, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(query.Query))
            throw new MissingSearchQueryException();

        return _quotesRepository.SearchQuotes(query.Query, query.Count, cancellationToken);
    }
}