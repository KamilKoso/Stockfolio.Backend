using Stockfolio.Modules.StockMarket.Application.Dto;
using Stockfolio.Shared.Abstractions.Queries;

namespace Stockfolio.Modules.StockMarket.Application.Queries;

internal class GetQuotes : IQuery<IEnumerable<QuoteDetailsDto>>
{
    public IEnumerable<string> Symbols { get; set; }
}