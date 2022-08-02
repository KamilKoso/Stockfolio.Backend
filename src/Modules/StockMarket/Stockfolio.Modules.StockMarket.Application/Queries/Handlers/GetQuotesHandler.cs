using Stockfolio.Modules.StockMarket.Application.Dto;
using Stockfolio.Modules.StockMarket.Application.Repositories;
using Stockfolio.Shared.Abstractions.Queries;

namespace Stockfolio.Modules.StockMarket.Application.Queries.Handlers;

internal class GetQuotesHandler : IQueryHandler<GetQuotes, IEnumerable<QuoteDetailsDto>>
{
    private readonly IQuotesRepository _quotesRepository;

    public GetQuotesHandler(IQuotesRepository quotesRepository)
    {
        _quotesRepository = quotesRepository;
    }

    public async Task<IEnumerable<QuoteDetailsDto>> HandleAsync(GetQuotes query, CancellationToken cancellationToken = default)
        => await _quotesRepository.GetQuotes(query.Symbols, cancellationToken);
}