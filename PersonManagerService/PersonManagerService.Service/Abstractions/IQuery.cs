using MediatR;

namespace PersonManagerService.Domain.Abstractions;

public interface IQuery<TResult> : IRequest<TResult>
{
}
