using Application.Abstractions.Contracts;

namespace Application.Ports.Handles;

public interface ICommandHandler<TRequest>
    where TRequest : IRequest
{
    Task Handle(TRequest request, CancellationToken cancellationToken);
}
