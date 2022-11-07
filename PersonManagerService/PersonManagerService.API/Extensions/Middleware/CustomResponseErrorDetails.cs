using FluentValidation.Results;
using System.Net;

namespace PersonManagerService.API.Extensions.Middleware;

public sealed record CustomResponseErrorDetails(HttpStatusCode statusCode, string Title, string Message, IEnumerable<ValidationFailure> Errors = null);
