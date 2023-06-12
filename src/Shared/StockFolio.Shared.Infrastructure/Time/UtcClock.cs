using Stockfolio.Shared.Abstractions.Time;
using System;

namespace Stockfolio.Shared.Infrastructure.Time;

public class UtcClock : IClock
{
    public DateTime CurrentDate() => DateTime.UtcNow;
}