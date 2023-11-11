using Stockfolio.Modules.Assets.Core.Exceptions;

namespace Stockfolio.Modules.Assets.Core.ValueObjects;
internal record Interval
{
    public static readonly string[] SupportedIntervals = { "1m", "2m", "5m", "15m", "30m", "60m", "90m", "1h", "1d", "5d", "1wk", "1mo", "3mo" };

    public string Unit { get; }
    public uint Value { get; }

    public Interval(string interval)
    {
        if (!SupportedIntervals.Contains(interval))
            throw new UnsupportedIntervalException(interval);

        bool isNumber = char.IsDigit(interval[1]);

        if (isNumber)
        {
            Value = uint.Parse(interval[0..2]);
            Unit = interval[2..];
        }
        else
        {
            Value = uint.Parse(interval[0..1]);
            Unit = interval[1..];
        }
    }

    public static implicit operator string(Interval interval) => interval.Value + interval.Unit;

    public static implicit operator Interval(string value) => new(value);
}