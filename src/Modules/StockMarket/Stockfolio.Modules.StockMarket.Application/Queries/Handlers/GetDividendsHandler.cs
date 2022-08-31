using Stockfolio.Modules.StockMarket.Application.Dto;
using Stockfolio.Modules.StockMarket.Application.Repositories;
using Stockfolio.Shared.Abstractions.Queries;

namespace Stockfolio.Modules.StockMarket.Application.Queries.Handlers;

internal class GetDividendsHandler : IQueryHandler<GetDividends, QuoteDividendsDto>
{
    private readonly IQuotesRepository _quotesRepository;

    public GetDividendsHandler(IQuotesRepository quotesRepository)
    {
        _quotesRepository = quotesRepository;
    }

    public async Task<QuoteDividendsDto> HandleAsync(GetDividends query, CancellationToken cancellationToken = default)
     => await _quotesRepository.GetDividends(query.Symbol, query.Start, query.End, cancellationToken);
}