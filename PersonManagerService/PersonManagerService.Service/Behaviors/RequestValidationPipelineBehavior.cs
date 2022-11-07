using FluentValidation;
using MediatR;
using PersonManagerService.Domain.Abstractions;

namespace PersonManagerService.Application.Behaviors;

public class RequestValidationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICommand<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public RequestValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if(!_validators.Any())
        {
            return await next();
        }

        var errors = _validators
            .Select(x => x.Validate(request))
            .SelectMany(vResult => vResult.Errors)
            .Where(err => err is not null)
            .Distinct();

        if(errors.Any())
        {
            throw new ValidationException("Request validation exception.", errors);
        }

        return await next();
    }
}
