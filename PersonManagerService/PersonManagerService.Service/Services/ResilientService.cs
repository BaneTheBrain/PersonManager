using Polly.CircuitBreaker;
using Polly.Retry;
using Polly.Wrap;
using Polly;
using Microsoft.Extensions.Logging;
using PersonManagerService.Application.Abstractions;
using PersonManagerService.Domain.Configuration;
using Microsoft.Extensions.Options;

namespace PersonManagerService.Application.Service;

public class ResilientService : IResilientService
{
    private readonly ILogger<ResilientService> _logger;
    public AsyncRetryPolicy RetryPolicy { get; }
    public AsyncCircuitBreakerPolicy CircutBreakerPolicy { get; }
    public AsyncPolicyWrap ResilientPolicy { get; }


    public ResilientService(ILogger<ResilientService> logger, IOptions<ResilientOptions> resilientOptions)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        RetryPolicy = Policy.Handle<Exception>().WaitAndRetryAsync(
          resilientOptions.Value.RetryCount,
          attempt =>
          {
              _logger.LogInformation($"{nameof(ResilientService)} -> {attempt} attempt. Transient error occured..");
              return TimeSpan.FromSeconds(Math.Pow(2, attempt));
          });

        CircutBreakerPolicy = Policy.Handle<Exception>().CircuitBreakerAsync(
            resilientOptions.Value.AllowedExceptionsBeforeBreaking,
            TimeSpan.FromSeconds(resilientOptions.Value.CircutOpenTimeInSec),
              (ex, t) => { _logger.LogError($"{nameof(ResilientService)} -> Circuit broken!"); },
              () => { _logger.LogInformation($"{nameof(ResilientService)} -> Circuit reset!"); });

        ResilientPolicy = CircutBreakerPolicy.WrapAsync(RetryPolicy);
    }
}
