using System;

namespace Stockfolio.Shared.Abstractions.Time;

public interface IClock
{
    DateTime CurrentDate();
}