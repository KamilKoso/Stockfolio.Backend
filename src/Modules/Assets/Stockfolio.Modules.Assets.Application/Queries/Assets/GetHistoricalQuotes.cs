using Stockfolio.Modules.Assets.Application.Dto.Assets;
using Stockfolio.Modules.Assets.Core.ValueObjects;
using Stockfolio.Shared.Abstractions.Queries;

namespace Stockfolio.Modules.Assets.Application.Queries.Assets;

internal record GetHistoricalQuotes : IQuery<HistoricalQuotesDto>
{
    public GetHistoricalQuotes(string symbol, DateTimeOffset? start, DateTimeOffset? end, string range, string interval)
    {
        Symbol = symbol;
        Range = range;
        Interval = new Interval(interval ?? "1d");
        Start = start ?? DateTimeOffset.Now.AddYears(-1);
        End = end ?? DateTimeOffset.Now;
    }

    public string Symbol { get; }
    public DateTimeOffset Start { get; }
    public DateTimeOffset End { get; }
    public Interval Interval { get; }
    public string Range { get; }
}