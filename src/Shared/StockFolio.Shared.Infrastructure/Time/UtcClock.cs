using System;
using Stockfolio.Shared.Abstractions.Time;

namespace Stockfolio.Shared.Infrastructure.Time;

public class UtcClock : IClock
{
    public DateTime CurrentDate() => DateTime.UtcNow;
}