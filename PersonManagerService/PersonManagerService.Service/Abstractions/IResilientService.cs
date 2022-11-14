using Polly.CircuitBreaker;
using Polly.Retry;
using Polly.Wrap;

namespace PersonManagerService.Application.Abstractions;

public interface IResilientService
{
    public AsyncRetryPolicy RetryPolicy { get; }
    public AsyncCircuitBreakerPolicy CircutBreakerPolicy { get; }
    public AsyncPolicyWrap ResilientPolicy { get; }
}
