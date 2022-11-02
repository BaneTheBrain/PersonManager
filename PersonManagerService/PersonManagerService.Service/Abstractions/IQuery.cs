using MediatR;

namespace PersonManagerService.Domain.Abstractions;

public interface IQuery<out TResult> : IRequest<TResult>
{
}
