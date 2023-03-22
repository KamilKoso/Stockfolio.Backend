//namespace Stockfolio.Modules.StockMarket.Application.Models;

//internal sealed class QuoteEvent
//{
//    private QuoteEvent(string value)
//    {
//        Value = value;
//    }

//    public string Value { get; }

//    public static QuoteEvent Dividends { get => new("div"); }
//    public static QuoteEvent CapitalGains { get => new("capitalGain"); }
//    public static QuoteEvent Splits { get => new("split"); }

//    public static implicit operator string(QuoteEvent quoteEvent)
//           => quoteEvent.Value;
//}