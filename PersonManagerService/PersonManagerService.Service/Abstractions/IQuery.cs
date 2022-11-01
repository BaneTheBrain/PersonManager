using MediatR;

namespace PersonManagerService.Application.Abstractions;

public interface IQuery<out TResult> : IRequest<TResult>
{
}
