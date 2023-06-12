namespace Stockfolio.Modules.Assets.Application.Dto.Assets;

internal record HistoricalQuotesDto
{
    public HistoricalQuotesDto(string symbol, string currency, IList<DividendDto> dividends, IList<SplitDto> splits, IList<Quote> quotes)
    {
        Symbol = symbol;
        Currency = currency;
        Dividends = (dividends ?? Array.Empty<DividendDto>()).AsReadOnly();
        Splits = (splits ?? Array.Empty<SplitDto>()).AsReadOnly();
        Quotes = (quotes ?? Array.Empty<Quote>()).AsReadOnly();
    }

    public string Symbol { get; private set; }
    public string Currency { get; private set; }
    public IReadOnlyCollection<DividendDto> Dividends { get; private set; }
    public IReadOnlyCollection<SplitDto> Splits { get; private set; }
    public IReadOnlyCollection<Quote> Quotes { get; private set; }
}