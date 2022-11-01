using MediatR;

namespace PersonManagerService.Application.Abstractions;

public interface ICommand<T> : IRequest<T>
{
}
