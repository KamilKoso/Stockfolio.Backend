using System;
using Stockfolio.Shared.Abstractions.Exceptions;

namespace Stockfolio.Shared.Infrastructure.Exceptions;

public interface IExceptionCompositionRoot
{
    ExceptionResponse Map(Exception exception);
}