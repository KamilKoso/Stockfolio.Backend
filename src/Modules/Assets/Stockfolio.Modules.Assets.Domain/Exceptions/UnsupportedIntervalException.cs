using Stockfolio.Modules.Assets.Core.ValueObjects;
using Stockfolio.Shared.Abstractions.Exceptions;

namespace Stockfolio.Modules.Assets.Core.Exceptions;

internal class UnsupportedIntervalException : StockfolioException
{
    public UnsupportedIntervalException(string interval) : base($"{interval} is invalid. Valid intervals: [{string.Join(", ", Interval.SupportedIntervals)}]")
    {
    }
}