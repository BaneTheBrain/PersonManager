using System.Net;

namespace PersonManagerService.API.Extensions.Middleware;

public sealed record CustomResponseErrorDetails(HttpStatusCode status, string Title, string Details);
