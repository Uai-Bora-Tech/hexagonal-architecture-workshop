using Application.Abstractions.Contracts;
using Application.Ports.Handles;
using Application.Ports.Repositories;

namespace Application.Abstractions.Handles;

public abstract class CommandHandler<TRequest> : ICommandHandler<TRequest>
    where TRequest : IRequest
{
    protected readonly ILibraryRepository _repository;

	protected CommandHandler(ILibraryRepository repository)
        => _repository = repository;

    public abstract Task Handle(TRequest request, CancellationToken cancellationToken);
}