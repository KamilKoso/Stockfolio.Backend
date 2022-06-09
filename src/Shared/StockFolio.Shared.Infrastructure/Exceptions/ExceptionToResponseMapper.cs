using Stockfolio.Shared.Abstractions.Exceptions;
using System;
using System.Net;

namespace Stockfolio.Shared.Infrastructure.Exceptions;

internal sealed class ExceptionToResponseMapper : IExceptionToResponseMapper
{
    public ExceptionResponse Map(Exception exception)
        => exception switch
        {
            StockfolioException ex => new ExceptionResponse(new ErrorsResponse(new Error(ex.Code, ex.Message)),
                HttpStatusCode.BadRequest),
            _ => new ExceptionResponse(new ErrorsResponse(new Error("Error", "Unrecognized error occurred.")),
                HttpStatusCode.InternalServerError)
        };

    private record Error(string Code, string Message);

    private record ErrorsResponse(params Error[] Errors);
}