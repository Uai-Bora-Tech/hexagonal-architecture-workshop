using Application.Abstractions.Contracts;
using Application.Ports.Handles;
using Application.Ports.Repositories;

namespace Application.Abstractions.Handles;

public abstract class QueryHandler<TRequest, TResponse> : IQueryHandler<TRequest, TResponse>
    where TRequest : IQuery
{
    protected readonly ILibraryRepository _repository;

    protected QueryHandler(ILibraryRepository repository)
        => _repository = repository;

    public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
}
