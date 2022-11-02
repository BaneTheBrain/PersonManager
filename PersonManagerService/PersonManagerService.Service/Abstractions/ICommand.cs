using MediatR;

namespace PersonManagerService.Domain.Abstractions;

public interface ICommand<T> : IRequest<T>
{
}
