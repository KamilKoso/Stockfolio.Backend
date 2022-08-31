using Stockfolio.Modules.StockMarket.Application.Dto;
using Stockfolio.Shared.Abstractions.Queries;

namespace Stockfolio.Modules.StockMarket.Application.Queries;

internal class GetDividends : IQuery<QuoteDividendsDto>
{
    public GetDividends(string symbol, DateTimeOffset? start, DateTimeOffset? end)
    {
        Symbol = symbol;
        Start = start ?? DateTimeOffset.Now.AddYears(-1); ;
        End = end ?? DateTimeOffset.Now;
    }

    public string Symbol { get; set; }
    public DateTimeOffset Start { get; set; }
    public DateTimeOffset End { get; set; }
}