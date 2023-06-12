using Stockfolio.Shared.Abstractions.Exceptions;
using System;

namespace Stockfolio.Shared.Infrastructure.Exceptions;

public interface IExceptionCompositionRoot
{
    ExceptionResponse Map(Exception exception);
}