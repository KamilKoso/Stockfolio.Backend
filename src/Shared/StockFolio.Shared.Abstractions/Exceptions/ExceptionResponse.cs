using System.Net;

namespace Stockfolio.Shared.Abstractions.Exceptions;

public record ExceptionResponse(ErrorsResponse Response, HttpStatusCode StatusCode);

public record Error(string Code, string Message);

public record ErrorsResponse(params Error[] Errors);