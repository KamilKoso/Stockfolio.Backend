﻿using System;
using System.Threading.Tasks;

namespace Stockfolio.Shared.Abstractions;

public static class Extensions
{
    public static async Task<T> NotNull<T>(this Task<T> task, Func<Exception> exception = null)
    {
        if (task is null)
        {
            throw new InvalidOperationException("Task cannot be null.");
        }

        var result = await task;
        if (result is not null)
        {
            return result;
        }

        if (exception is not null)
        {
            throw exception();
        }

        throw new InvalidOperationException("Returned result is null.");
    }
}