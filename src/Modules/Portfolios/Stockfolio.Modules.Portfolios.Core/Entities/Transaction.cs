﻿namespace Stockfolio.Modules.Portfolios.Core.Entities;

public class Transaction
{
    public Guid Id { get; set; }
    public DateTimeOffset Date { get; private set; }
}